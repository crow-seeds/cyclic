var fullscreen = {
    GoFullscreen: function()
    {
		if(UnityLoader.SystemInfo.mobile){
			var viewFullScreen = document.getElementById('#canvas');
 
			var ActivateFullscreen = function()
			{
				if (viewFullScreen.requestFullscreen) /* API spec */
				{  
					viewFullScreen.requestFullscreen();
				}
				else if (viewFullScreen.mozRequestFullScreen) /* Firefox */
				{
					viewFullScreen.mozRequestFullScreen();
				}
				else if (viewFullScreen.webkitRequestFullscreen) /* Chrome, Safari and Opera */
				{  
					viewFullScreen.webkitRequestFullscreen();
				}
				else if (viewFullScreen.msRequestFullscreen) /* IE/Edge */
				{  
					viewFullScreen.msRequestFullscreen();
				}
				viewFullScreen.removeEventListener('touchend', ActivateFullscreen);
			}
			viewFullScreen.addEventListener('touchend', ActivateFullscreen, false);
		}else{
			var viewFullScreen = document.getElementById('#canvas');
 
			var ActivateFullscreen = function()
			{
				if (viewFullScreen.requestFullscreen) /* API spec */
				{  
					viewFullScreen.requestFullscreen();
				}
				else if (viewFullScreen.mozRequestFullScreen) /* Firefox */
				{
					viewFullScreen.mozRequestFullScreen();
				}
				else if (viewFullScreen.webkitRequestFullscreen) /* Chrome, Safari and Opera */
				{  
					viewFullScreen.webkitRequestFullscreen();
				}
				else if (viewFullScreen.msRequestFullscreen) /* IE/Edge */
				{  
					viewFullScreen.msRequestFullscreen();
				}
				viewFullScreen.removeEventListener('click', ActivateFullscreen);
			}
			viewFullScreen.addEventListener('click', ActivateFullscreen, false);
		}
    }
};
mergeInto(LibraryManager.library, fullscreen);