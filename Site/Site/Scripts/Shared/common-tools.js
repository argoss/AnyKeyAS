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