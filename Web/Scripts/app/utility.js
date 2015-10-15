
var Utility = function() {

    var self = {
        showError: showError,
        showElement: showElement,
        hideElement: hideElement,
        toCoordinate: toCoordinate
    };

    return self;

    function hideElement(element) {
        $(element).addClass('hidden');
    }

    function showElement(element) {
        $(element).removeClass('hidden');        
    }

    function showError(message) {
        alert(message);
    }

    function toCoordinate(data) {
        return {
            description: data.Description,
            coords: {
                latitude: data.Latitude,
                longitude: data.Longitude
            }
        };
    }
}
