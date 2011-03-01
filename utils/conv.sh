#!/bin/bash  

#################################################################################################
# script v0.1
# Author: Krzysztof Piech (chrisx29a@gmail.com)							
# Required packages:										
# 	imagemagick (Debian/Ubuntu: $ sudo apt-get install imagemagick)				
#												
# Installation:											
#	1. Put script in decent place. (for example: /home/xbmc/conv.sh)
#	2. Optiona: Adjust convert argument for best results. ($ man convert)
#	2. Make it executable. ($ chmod +x /home/xbmc/conv.sh)					
#	3. Add crontab job ($ crontab -e  (for example: */10 * * * * /home/xbmc/conv.sh ))	
#################################################################################################

THUMBNAILS_DIR='/home/xbmc/.xbmc/userdata/Thumbnails'
CONVERT_ARGS='-resize 40%'

if [ $# -gt 0 ];
 then
	THUMBNAILS_DIR=$1
 else 
	echo "Using default thumbnails directory: $THUMBNAILS_DIR"
fi

function doconv {
for file in $1/*
 do
   if [ -d $file ];
      then
 	doconv $file	
   elif [ -e $file ];
        then 
 	jpg_filepath=${file/.tbn/.jpg}
 	if [ ! -f $jpg_filepath ];
 	   then
 		convert $CONVERT_ARGS $file $jpg_filepath
 	fi
    fi
 done
}

for f in $THUMBNAILS_DIR
 do
   doconv $f
 done




