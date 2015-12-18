### Missing Thumbnails ###

If you are not seeing any thumbnails at all then the problem is usually the port number...

The JPGs are supplied by the Xbmc webserver directly, either port 8080 or port 80. The Eden version of xbmc uses port 80 but in the past it used 8080.

You can check the JPG port number in the Xbmc Services>Webserver settings page. (See attached image)

In Crestron SIMPL project locate the XBMC module in the project. In the detail view, at the bottom of this module you will see 5 parameters. Set the httpport to whatever port is on your Xbmc webserver setting page.

![http://www.neilcarthy.com/img/WebserverSettings.png](http://www.neilcarthy.com/img/WebserverSettings.png)


### Thumbnails are only partly loading ###

This is a known issue on XPanel and Touchpanels for the current version of xbmc-crestron and the Xbmc Frodo release. It is not a problem on iPad. I don't know what causes this problem but I think it might be one of these:
  1. In Frodo the thumbnails are larger than Eden, often 500x500 or larger meaning almost every image has to be rescaled and I don't think the Crestron processor can keep up
  1. The thumnail uris can be very long... approaching or even exceeding 255 characters, which is the limit for Dynamic Graphics in Crestron systems.

### After updating to Frodo video is very slow or stuttering ###
If you experience video problems after upgrading to Frodo from Eden then
it's probably caused by your HD Audio driver. You may need to re-install
your HDMI Audio drivers and make changes in Xbmc at
Settings -> System -> Audio output

Troubleshooting here: http://forum.xbmc.org/showthread.php?tid=146911
AudioEngine info here: http://wiki.xbmc.org/index.php?title=AudioEngine


### Problems compiling XbmcMain.usp ###

If you have problems compiling any of the modules for the MC3 like "Getting Compile Error "Could Not Add (file name).lpz" then try these steps:

  * Use "Recompile All" in the Build Menu, not F12.
  * On Windows 7 I always run SIMPL+ as an administrator. (Right-click the icon and select "Run as administrator".
  * If neither of these work try rebooting the PC


### This version only works with the Frodo release ###
If you need a version that works with Eden then use version 0.7.1 in the Downloads tab.

You can also use the eden branch in the source code. Use svn to check it out.