This VT Pro-e project requires at least version 4.2 of Crestron Visiontools Pro-e

A note about thumbnails:
XBMC stores image files (thumbnails, fanart etc.) with a .tbn extension. Xpanel and TPS-6X touchpanels do not recognise these files as image files. The solution to this problem that I employed was to make a copy of the thumbnail file and save the new version with the extension .jpg. I then set the "UseJpgExtension" parameter on XBMC.umc to "1d".

I wrote a simple .NET console program that did the copying. A version can be found in the utils directory. Run it from a Cmd.exe window. It requires .NET 2.0 

You no longer need to use the latest nightly version of XBMC for best results: The Eden release has all the functionality needed.