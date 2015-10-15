var WeatherService = function (options) {

    var self = {
        getWeather:getWeather
    }

    function getWeather(location) {
        var uri = options.coordinateSearchUri + "?Latitude=" + location.coords.latitude + "&Longitude=" + location.coords.longitude;
        return get(uri);
    }

    function get(uri) {
        var encodedUri = encodeURI(uri);
        return $.getJSON(encodedUri);
    }

    return self;
}