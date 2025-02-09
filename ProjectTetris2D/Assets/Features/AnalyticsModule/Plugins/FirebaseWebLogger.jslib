mergeInto(LibraryManager.library, {
    
    // for initialize firebase
    CheckFirebaseReady: function () {
        if (typeof firebase !== 'undefined' && firebase.analytics) {
            return 1; // true
        } else {
            return 0; // false
        }
    },

    // for send data to Firebase
    LogFirebaseEvent: function (eventNamePtr, paramsPtr) {
        const eventName = UTF8ToString(eventNamePtr);
        const params = JSON.parse(UTF8ToString(paramsPtr));
        firebase.analytics().logEvent(eventName, params);
    }
});