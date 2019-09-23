<pre>Track Ping
Options:                                                                      
   -c         Set the ping count (Default: 2000)                              
   -d         Set the Database name (Default: "date.sqlite")                  
   -h         Shows this help text                                            
   -i         Specify the IP address (accepts multiple -i                     
              options for pinging multiple IP addresses)                      
   -s         Seconds between Pings (Default: 5)                              



Use Case:
Ping IP addresses and log the results to an SQLite Database.
Analyze the results to search for network latency and dropped packets.

Examining Results:
Use SQLiteBrowser "https://sqlitebrowser.org" to view, filter, and analyze the ping results.
Open Database, Click "Browse Data", Select the table, view/filter/analyze/export the data.

Examples:
TrackPing.exe -i 10.0.0.1
     --Pings 10.0.0.1 for the default number of pings at the default interval
TrackPing.exe -i 10.0.0.1 -i 10.0.0.2
     --Pings 10.0.0.1 & 10.0.0.2 for the default number of pings at the default interval
TrackPing.exe -i 10.0.0.1 -c 10 -s 2
     --Pings 10.0.0.1 for the 10 pings at a 2 second interval
TrackPing.exe -i 10.0.0.1 -d MyDatabase.sqlite
     --Pings Pings 10.0.0.1 for the default number of pings at the default interval, and sets the database name
</pre>
