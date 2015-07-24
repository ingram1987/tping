# tping
tping, or TrackPing, is a utility that will track ping times and export them into a CSV file for further analysis. Typical usage is for monitoring a network device or internet connection for latency or drops. Currently tping will ping the specified hostname or ip once every 2 seconds, 43200 times, which comes out to around 24 hours. This is so you can leave tping running for a day, and then analyze the results. The command can be stopped at any time by pressing ctrl + c.

Usage:
tping.exe [hostname|ip]

For example:

tping.exe 4.2.2.1

Host, Response Time, Status, Time

4.2.2.1, 24, Success, 22:37:50.4599477

4.2.2.1, 27, Success, 22:37:52.4946834

4.2.2.1, 27, Success, 22:37:54.5233546

4.2.2.1, 26, Success, 22:37:56.5547100

4.2.2.1, 31, Success, 22:37:58.6094936

4.2.2.1, 29, Success, 22:38:00.6271065

4.2.2.1, 26, Success, 22:38:02.6584623

4.2.2.1, 26, Success, 22:38:04.6868608

To export to a CSV file, simply send the output to a CSV file like this:

tping.exe 4.2.2.1 > C:\tping-results.csv

Once tping is done running, you can analyze the CSV file to find out what time of the day you are experiencing latency or drops.

Future versions of tping will include a ping amount parameter, and will perform basic analysis of the ping times gathered.
