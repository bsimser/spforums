### Installing the Web Part - Step 2

The SharePoint Forums Web Part is made up of two .NET assemblies. These are:
# BilSimser.SharePoint.Forums.dll
# BilSimser.SharePoint.Forums.Core.dll

One is the user interface and contains the Web Part itself, the other contains the backend logic and services code.

These files have to be copied to the root folder of the virtual server where SharePoint is installed to. This is normally c:\inetpub\wwwroot\bin but your administrator may have setup things differently and this may not be the location of the file.

You can find the location of your virtal server root directory with the Internet Information Services snap-in. The Internet Information Services snap-in is an administration tool for IIS that has been integrated with other administrative functions of Windows Server. To launch the Internet Information Services snap-in:

# Click Start, point to Programs, point to Administrative Tools, and click Computer Management. 
# Under the Server Applications and Services node, expand Internet Information Services. 

To locate where your virtual server root directory is:

# Launch the IIS snap-in as described above
# Expand the Web Sites item in the tree
# Locate the web site running your SharePoint installation (normally Default Web Site)
# Right-click on it and select Properties
# Select the Home Directory tab

The Local path contains the physical location of the directory. Copy the files above to the "bin" directory under this location. If the "bin" folder doesn't exist, create it.

The SharePoint Forums Web Part files are now installed.