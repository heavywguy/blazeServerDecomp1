To use this blaze you will need a redirector.
Credits to misaka_mikoto_01 on the Battlefield Alpha-Beta Preservation discord server.
Additional: Building this on VS won't create the "conf" folder, as well as the bf3lan.sql (this blaze WILL need a mysql host to work).
After building, create a folder called "conf" in the directory the blaze.exe is in, then, create 2 .txt files inside the folder "conf", one named subnet, the other, conf.
in the conf.txt paste this:

#Main Settings

LogLevel=1
NATType=0
TimeCheckInterval=400000
WriteTimeout=4000
ReadTimeout=4000
UseClientMetrics=0
ServerListeningPort=42128
ClientListeningPort=42129
dbhost=127.0.0.1
dbname=bf3lan
dbuser=root
dbpass=root
dbConnectionPort=3306
UseLocalPlayerAlgorithm=1
online_access=1
online_access2=1

In subnet.txt: 
0.0.0.0:0.0.0.0:
