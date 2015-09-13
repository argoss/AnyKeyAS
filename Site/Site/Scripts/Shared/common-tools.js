function getOrCreateAngularModule(name, requires) {
    var mod = null;
    try {
        mod = angular.module(name);
    }
    catch (e) {
        mod = angular.module(name, requires);
        return mod;
    }
    for (var index = 0; index < requires.length; index++) {
        var item = requires[index];
        if (mod.requires.indexOf(item) < 0) {
            mod.requires.push(item);
        }
    }
    return mod;
}
// This file contains common utilities which can be used in our *.ts files.
function errStringFromHttpResponse(result) {
    /// <summary>Composes error string from the server response.</summary>
    /// <param name="result" type="string">Result of http request.</param>
    /// <returns type="string">Error string</returns>	
    if (!angular.isObject(result) || !('status' in result) || result.status != 500)
        return "";
    if (angular.isString(result.data))
        return "<p>" + result.data + "</p>";
    if (angular.isArray((result.data))) {
        var errString = "";
        for (var i in result.data) {
            var val = result.data[i];
            if (angular.isObject(val) && ('Errors' in val)) {
                errString += "<p>" + val.Errors + "</p>";
            }
            if (angular.isString(val)) {
                errString += "<p>" + val + "</p>";
            }
        }
        return errString;
    }
    if (angular.isObject(result.data) && ('ExceptionMessage' in result.data)) {
        return "<p>" + result.data.ExceptionMessage + "</p>";
    }
    return "";
}
function notifySuccess(msg) {
    /// <summary>Shows notification about success. Requires html element with class 'notifications' on html page.</summary>
    /// <param name="msg" type="string">Message to show</param>
    $(".notifications").notify({ type: "success-blackgloss", message: { html: "<strong>Success!</strong> " + msg } }).show();
}
function notifyError(msg, result) {
    /// <summary>Shows notification about error. Requires html element with class 'notifications' on html page.</summary>
    /// <param name="msg" type="string">Message to show</param>
    /// <param name="result" type="any">Optional parameter. Result of http request</param>
    $(".notifications").notify({ type: "danger-blackgloss", message: { html: "<strong>Error!</strong> " + msg + (result ? errStringFromHttpResponse(result) : "") } }).show();
}
function date2IsoStr(date) {
    if (date == null)
        return null;
    if (typeof (date) == 'string') {
        date = new Date(date);
    }
    return date.toISOString().substring(0, 10);
}