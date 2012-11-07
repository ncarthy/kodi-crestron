1. A note about thumbnails:
XBMC stores image files (thumbnails, fanart etc.) with a .tbn extension. Xpanel and TPS-6X touchpanels do not recognise these files as image files. 

The solution to this problem was to append "?image.jpg" to the end of the image urls. This is not required for Android/iPad/iPhone but it doesn't affect their operation either. 

When the 'UseJpgExtension' parameter on XBMC v2.umc is set to "1d" then the "?image.jpg" string is appended. Otherwise not/

2. General Advice
Use the Eden release of XBMC for best results.