
/*options: {
resultCallback: -,
postalSearchUri: -,
addressSearchUri: -

*/

var Search = function (options) {
    var config = options;

    var self = {
        search: startSearch,
        errorCallback: function (error){}
    };
    
    return self;
   
    function startSearch(searchParam) {
        
        if (isAllNumbers(searchParam)) {
            searchForPostalCode(searchParam);
        } else {
            searchForAddress(searchParam);
        }
    }

    function searchForPostalCode(searchParam) {        
        var uri = config.postalSearchUri + "/" + searchParam;
        search(uri);
    }

    function searchForAddress(searchParam) {
        var uri = config.addressSearchUri + "/" + searchParam;
        search(uri);
    }

    function isAllNumbers(searchParam) {
        var allNumbersRegex = /^\d+$/;

        return allNumbersRegex.test(searchParam);
    }

    function search(uri) {
        var encodedUri = encodeURI(uri);
        $.getJSON(encodedUri)
            .done(searchSuccess)
            .fail(searchFailed);
    }

    function searchSuccess(data, status, xhr) {        
        config.resultCallback(data);
    }
    function searchFailed(data, status, errorThrown) {
        alert("Search failed.");
    }

}