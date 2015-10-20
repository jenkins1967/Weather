var WeatherDisplayer = function (containerElement, options) {
    var container = containerElement;
    var config = options;
    var todayTemplate;
    var utility = new Utility();

    var self = {
        load: load,
        show: show,
        hide: hide
    }

    return self;

    function load(data) {        
        var context = {
            today: {
                dailyMaximum: findItemContainingThisHour(data.Temperatures.DailyMaximum).Value,
                conditionSummary: getConditionsText(data),
                wind:getTodaysWindDisplay(data.Wind),
                humidity:'&nbsp;',
                dewPoint: findItemStartingThisHour(data.Temperatures.DewPoint).Value
            }            
        }

        getTodayTemplate()
            .done(function (template) {
                var html = template(context);
                container.html(html);
                self.show();
            });

    }

    function getTodayTemplate() {

        var deferred = $.Deferred();

        if (todayTemplate != null) {
            deferred.resolve(todayTemplate);
        } else {
            var options = {
                async: true,
                cache: true,
                dataType: 'html'
            };

            $.ajax(config.todayTemplateUri, options)
                .done(function (data) {
                    todayTemplate = Handlebars.compile(data);
                    deferred.resolve(todayTemplate);
                });
        }

        return deferred.promise();
    }

    function getConditionsText(data) {
        var clouds = findItemStartingThisHour(data.CloudCover.Percent);
        return percentToConditions(clouds.Value);
    }

    function getTodaysWindDisplay(windData) {
        var todaysWindGusts = findItemContainingThisHour(windData.Gusts).Value;
        var todaysWindSpeed = findItemContainingThisHour(windData.Speed).Value;
        var todaysWindDirection = findItemContainingThisHour(windData.Direction).Value;
        return degToCompass(todaysWindDirection) + ' ' + (Math.round(todaysWindSpeed * Constants.MPHPerKnot)) + ' mph (Gusts ' + Math.round(todaysWindGusts * Constants.MPHPerKnot) + ')';
    }

    function percentToConditions(num) {
        var conditions = ['Mostly Cloudy', 'Partly Cloudy', 'Partly Sunny', 'Mostly Sunny'];
        if (num === 0) return 'Clear';
        if (num === 100) return 'Cloudy';
        var pos = Math.ceil(100 / num) - 1;
        return conditions[pos];
    }

    function degToCompass(num) {
        var val = Math.floor((num / 22.5) + 0.5);
        var arr = ["N", "NNE", "NE", "ENE", "E", "ESE", "SE", "SSE", "S", "SSW", "SW", "WSW", "W", "WNW", "NW", "NNW"];
        return arr[(val % 16)];
    }

    function findItemContainingThisHour(items) {
        var now = new Date();
        for (var index = 0; index < items.length; index++) {
            var currentItem = items[index];
            var start = new Date(currentItem.Start);
            var end = new Date(currentItem.End);
            if ((start < now && end > now) || start > now) {
                return currentItem;
            }
        }
    }

    function findItemStartingThisHour(items) {
        var now = new Date();
        var prevItem = null;
        for (var index = 0; index < items.length; index++) {
            var currentItem = items[index];
            if (prevItem == null) {
                prevItem = currentItem;
            }

            var currentStart = new Date(currentItem.Start);
            var prevStart = new Date(prevItem.Start);
            if (prevStart < now && currentStart > now) {
                return prevItem;
            }
            if (currentStart > now) {
                return currentItem;
            }
            prevItem = currentItem;
        }
        return null;
    }

    function show() {
        utility.showElement(container);
    }

    function hide() {
        utility.hideElement(container);
    }
}