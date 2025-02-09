mergeInto(LibraryManager.library, {

    LoadFromLocalStorage: function (key) {
        key = UTF8ToString(key);
        if (typeof key === "string") {
            console.log('Loading from localStorage with key:', key);
            return localStorage.getItem(key);
        }
        return null;
    },

    SaveToLocalStorage: function (key, jsonData) {
        key = UTF8ToString(key);
        jsonData = UTF8ToString(jsonData);

        console.log('SaveToLocalStorage called with:', key, jsonData);
        if (typeof key === "string" && typeof jsonData === "string") {
            localStorage.setItem(key, jsonData);
            console.log('Saved to localStorage with key:', key);
        } else {
            console.error('Invalid arguments for SaveToLocalStorage:', key, jsonData);
        }
    },

    MyLog: function (value) {
        console.log('Log:', UTF8ToString(value));
    }
});