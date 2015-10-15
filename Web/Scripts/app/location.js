
var UserLocation = function(locationChangedCallback) {
    var changeCallback = locationChangedCallback;
    var self = {
        currentLocation: null,
        locationChanged: function() {},
        isInUnitedStates: function() {},
        update: updatePosition,
        geoLocationSupported: null
    };

    initialize();
    return self;

    function initialize() {
        self.geoLocationSupported = geoLocationSupport();
    }

    function updatePosition() {
        if (geoLocationSupport()) {
            var options = {
                enabledHighAccuracy: false,
                timeout: 5000, //3 seconds
                maximumAge: 0 //no cache
            };
            navigator.geolocation.getCurrentPosition(locationDetected, locationDetectionError, options);
        } else {
            locationDetectionError("Geolocation is not supported.");
        }
    }

    function locationDetected(location) {
        self.currentLocation = location;
        changeCallback(self.currentLocation, null);
    }

    function locationDetectionError(error) {
        var message = '';
        if (error.code !== null) {
            switch (error.code) {
            case error.PERMISSION_DENIED:
                message = "Geolocation request denied by user.";
                break;
            case error.POSITION_UNAVAILABLE:
                message = "Location information is unavailable.";
                break;
            case error.TIMEOUT:
                message = "The request to get user location timed out.";
                break;
            case error.UNKNOWN_ERROR:
                message = "An unknown error occurred.";
                break;
            }
        } else {
            message = error;
        }       

        changeCallback(self.currentLocation, message);
    }

    function geoLocationSupport() {
        return Modernizr.geolocation;
    }
}