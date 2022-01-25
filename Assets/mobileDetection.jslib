var mobileDetection = {
   IsMobile: function()
   {
      return UnityLoader.SystemInfo.mobile;
   }
};  
mergeInto(LibraryManager.library, mobileDetection);