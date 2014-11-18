var NegotiumUtilities = window.NegotiumUtilities || {};

NegotiumUtilities.getQueryStringParameter = function (paramToRetrieve) {
    var params = document.URL.split("?")[1].split("&");
    var strParams = "";
    for (var i = 0; i < params.length; i = i + 1) {
        var singleParam = params[i].split("=");
        if (singleParam[0] == paramToRetrieve)
            return singleParam[1];
    }
}

NegotiumUtilities.GetSharePointSecurityParams = function ()
{
    var re = /#$/;
    var params = {
        SPHostUrl: decodeURIComponent(NegotiumUtilities.getQueryStringParameter("SPHostUrl")).replace(re, ""),
        SPLanguage: decodeURIComponent(NegotiumUtilities.getQueryStringParameter("SPLanguage")).replace(re, ""),
        SPClientTag: decodeURIComponent(NegotiumUtilities.getQueryStringParameter("SPClientTag")).replace(re, ""),
        SPProductNumber: decodeURIComponent(NegotiumUtilities.getQueryStringParameter("SPProductNumber")).replace(re, "")
    };
    return $.param(params);
}