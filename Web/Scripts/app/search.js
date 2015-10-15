
/*options: {
resultCallback: -,
postalSearchUri: -,
addressSearchUri: -

*/

var Search = function (options) {
    var config = options;

    var self = {
        searchAddress: startAddressSearch,
        searchLocation: searchForCoordinate,
        errorCallback: function (error){}
    };
    
    return self;
   
    function startAddressSearch(searchParam) {
        
        if (isAllNumbers(searchParam)) {
            return searchForPostalCode(searchParam);
        }

        return searchForAddress(searchParam);
        
    }

    function searchForPostalCode(searchParam) {        
        var uri = config.postalSearchUri + "/" + searchParam;
        return search(uri);
    }

    function searchForAddress(searchParam) {
        var uri = config.addressSearchUri + "/" + searchParam;
        return search(uri);
    }

    function searchForCoordinate(location) {
        var uri = config.coordinateSearchUri + "?Latitude=" + location.coords.latitude + "&Longitude=" + location.coords.longitude;
        return search(uri);
    }

    function isAllNumbers(searchParam) {
        var allNumbersRegex = /^\d+$/;

        return allNumbersRegex.test(searchParam);
    }

    function search(uri) {
        var encodedUri = encodeURI(uri);
        return $.getJSON(encodedUri);
            
    }
    
}