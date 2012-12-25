This version only works with the Frodo release of Xbmc


If you experience video problems after upgrading to Frodo from Eden then
it's probably caused by your HD Audio driver. You may need to re-install
your HDMI Audio drivers and make changes in Xbmc at 
Settings -> System -> Audio output

Troubleshooting here: http://forum.xbmc.org/showthread.php?tid=146911

AudioEngine info here: http://wiki.xbmc.org/index.php?title=AudioEngine


Frodo uses larger thumbnail images than Eden and XPanel and most TouchPanels
will struggle to show the images because they have to be rescaled. Images
are fine on iPad.


If list items are missing from XPanel (e.g. you don't see your Albums listed)
that is because there has been some changes in how Browse information is
sent to the Serial arrays (Name$, Genre$ etc.):

- Album name is now sent to SeasonOrAlbumName instead of MOVIE/EPISODE_NAME
- Artist name is now sent to SERIES/ARTIST_NAME instead of MOVIE/EPISODE_NAME
- For TV shows FirstAired is now sent to YEAR/AIRDATE instead of Tagline
- For Tv Shows the banner thumbnail is now sent to Banner instead of Thumbnail
  Banner is a new serial array specifically for these images
