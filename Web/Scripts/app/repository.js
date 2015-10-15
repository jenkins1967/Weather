var Repository = function () {
    var keyGeolocate = "autoGeoLocate";

    var self = {
        getGeoLocate: getGeoLocate,
        setGeoLocate: setGeoLocate
    }

    return self;

    function getGeoLocate() {
        return Boolean(localStorage.getItem(keyGeolocate));
    }

    function setGeoLocate(status) {
        var value = status === true;
        localStorage.setItem(keyGeolocate, value);
    }
}