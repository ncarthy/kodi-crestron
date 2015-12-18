# Preliminaries #
  * Download and install the latest (currently 12.3) version of Xbmc. You can obtain it from http://www.xbmc.org/download/
  * LoadMediaOntoXbmc
  * Switch on Xbmc HTTP server and event server:
    * From the Xbmc main menu navigate to System > Services > Webserver
    * Select "Allow control of XBMC via HTTP". The port number must match the Http Port number in the Parameter section of the XBMC module in your SIMPL program, otherwise you won't see Thumbnail images.
    * Go down to Remote Control
    * Select both "Allow programs on this system to control XBMC" and "Allow programs on other systems to control XBMC"
  * If you suspect that a firewall is preventing you from communicating to Xbmc then open ports 8080, 9090 and 9777 on the Xbmc machine. Maybe first try disabling the firewall to see if that helps.
  * The VT Pro-e project requires at least version 5.0 of [VisionTools](http://www.crestron.com)


# Compiling #
What follows are instructions for getting the demo XPanel project working.
  * Download and extract the source code zip file from the Downloads tab
  * Open the xbmc-test.smw project file in SIMPL windows
    * This project is set up for an AV2 processor. Change if required.
  * Locate the XBMC module in the project. In the detail view, at the bottom of this module you will see 5 parameters:
    * `Item_Count`: is the number of items in a browse list that will be shown at one time. Minimum 1, maximum 10. Leave it at 6 for the XPanel project to function correctly.
    * `Name`: The name of the controller, as seen by Xbmc. Can be any string.
    * `Address`: The IP address of the Xbmc
    * `httpport`: The port that Xbmc's HTTP server is listening on (usually 8080 but might be 80... check what port was set in "Preliminaries" above )
    * `tcpport`: The port that Xbmc's TCP server is listening on (usually 9090). This server does not have any authentication capability at present. If port 9090 conflicts with another device on you network it can be changed via XBMC's advancedsettings.xml file. See the XBMC wiki for mor information.
  * Compile the project. If you get errors please email me.
  * Upload to your Crestron processor
  * Open and compile the VT Pro-e XPanel project.
    * Be sure to set the IP address of your Crestron processor in the project properties (Ctrl-P)
    * Also, ensure the IP ID matches that for the XPanel in your SIMPL project
  * Run the XPanel and begin testing. If you notice errors in the Toolbox debug view please email me a screenshot.

# Troubleshooting #
  * Ensure you have [correctly loaded your media libraries](LoadMediaOntoXbmc.md) onto the Xbmc application
  * Use the Frodo (v12.0) release of xbmc and the latest version of this project
  * For debug output uncomment the debug constant at the top of the SIMPL+ files. This show what JSON commands are being sent to XBMC.
  * More tips at [Troubleshooting](Troubleshooting.md)