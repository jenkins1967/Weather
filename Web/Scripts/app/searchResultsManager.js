
var SearchResultsManager = function (element, templateLocation, locationSelectedCallback) {
    var templateUri = templateLocation;
    var locationSelected = locationSelectedCallback;
    var searchResultsTemplate;
    var picker = $(element);
    var utility = new Utility();

    //wire up close button
    $(".closeButton", picker).on('click', hide);

    //wire up the selectors using delegated events
    var selector = $('button.locationButton');
    $('body').on('click', 'button.locationButton', handleLocationSelected);

    var self = {
        show: show,
        hide: hide,
        load:load
    }

    return self;

    function handleLocationSelected(event) {
        //package up location
        var container = $(event.currentTarget);
        var location = {
            description: container.attr("data-description"),
            coords: {
                latitude: container.attr("data-latitude"),
                longitude: container.attr("data-longitude")
            }
        };

        self.hide();

        //callback
        locationSelected(location);
    }

    function show() {
        utility.showElement(picker);
    }

    function hide() {
        utility.hideElement(picker);
    }

    function load(data) {
        //load
        var locations = [];
        $.each(data, function(index, value) {
            locations.push({
                latitude: value.Latitude,
                longitude: value.Longitude,
                description: value.Description
            });
        });

        var context = {
            locations: locations
        };

        var container = $(".panel-body", picker).first();
        getSearchResultsTemplate()
                .done(function (template) {
                    var html = template(context);
                    container.html(html);
                    self.show();
        });
        
    }

    function getSearchResultsTemplate() {

        var deferred = $.Deferred();

        if (searchResultsTemplate != null) {
            deferred.resolve(searchResultsTemplate);
        } else {
            var options = {
                async: true,
                cache: true,
                dataType: 'html'
            };

            $.ajax(templateUri, options)
                .done(function (data) {
                    searchResultsTemplate = Handlebars.compile(data);
                    deferred.resolve(searchResultsTemplate);
                });
        }

        return deferred.promise();
    }
}