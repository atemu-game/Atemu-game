mergeInto(LibraryManager.library, {
  HandleConnectButton: function () {
    try {
      window.dispatchReactUnityEvent("HandleConnectButton");
    } catch (e) {
      console.warn("Failed to dispatch event");
    }
  },
  HandleClearSessionButton: function () {
    try {
      window.dispatchReactUnityEvent("HandleClearSessionButton");
    } catch (e) {
      console.warn("Failed to dispatch event");
    }
  },
  HandleRequestSign: function (message) {
    try {
      var convertedMessage = UTF8ToString(message);
      window.dispatchReactUnityEvent("HandleRequestSign", convertedMessage);
    } catch (e) {
      console.warn("Failed to dispatch event");
    }
  },
  HandleCopyLink: function (message) {
    try {
      var convertedMessage = UTF8ToString(message);
      window.dispatchReactUnityEvent("HandleCopyLink", convertedMessage);
    } catch (e) {
      console.warn("Failed to dispatch event");
    }
  },
});
