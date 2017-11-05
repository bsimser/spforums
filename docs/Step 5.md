### Installing the Web Part - Step 5

Once all the configuration changes have been made, it's best to restart IIS. This will clear any cached files or policies and reset things for use with the SharePoint Forums Web Part.

To restart IIS

# In the Internet Information Services snap-in, select the Computer icon in the contents pane and click the Action button. 
# Click the Action button and select Restart IIS. 
# From the drop-down menu, select Restart Internet Services, Stop Internet Services, Start Internet Services, or Restart computer name. 

Microsoft also provides a command-line version of the IIS snap-in restarting feature: Iisreset.exe. See the following command-line usage and parameters.

iisreset computername
  
/RESTART Stop and then restart all Internet services. 
/START  Start all Internet services. 
/STOP Stop all Internet services. 
/REBOOT  Reboot the computer. 
/REBOOTONERROR   Reboot the computer if an error occurs when starting, stopping, or restarting Internet services. 
/NOFORCE Do not forcefully terminate Internet services if attempting to stop them gracefully fails. 
/TIMEOUT:val Specify the timeout value (in seconds) to wait for a successful stop of Internet services. On expiration of this timeout the computer can be rebooted if the /REBOOTONERROR parameter is specified. The default value is 20s for restart, 60s for stop, and 0s for reboot. 
/STATUS Display the status of all Internet services. 
/ENABLE  Enable restarting of Internet Services on the local system. 
/DISABLE Disable restarting of Internet Services on the local system.