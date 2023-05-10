This is a runner template for developers to use and make your own game.

one predefined level is made in demo level and you can make your own random scenes by using the prefabs in the prefabs folder.
there is a level menu in the mainmenu, you can use those level buttons for your own levels and disable the rest 
To use that menu add your levels to build settings and note the bulid number of the scene and ad this build number to the buttons' "Levelbtnno" field under LevelsBtns.cs in Inspector window.
Lock image is the image to display if the level is locked or not, you can ad your UI component or sprite in lockImg source image in inspector window.

if you play for the firse time it will show the play guide lines which are not displayed second time using player prefs in the GameManager.cs that has key "firsttime"

audio state is alse stored in playerprefs.