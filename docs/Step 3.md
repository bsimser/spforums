### Installing the Web Part - Step 3

The SharePoint Forums Web Part is made up of two .NET assemblies. One is the user interface and contains the Web Part itself, the other contains the backend logic and services code.

In order for SharePoint to trust the assemblies and let them execute, entries have to be added to the web.config file in the SafeControls section. This lets SharePoint know explicitly what files are trusted and what ones are not.

The web.config file is a simple text file and can be edited with Notepad. It is located in the folder where the virtual directory for the web site runs. This is normally c:\inetpub\wwwroot but your administrator may have setup things differently and this may not be the location of the file.

You can find the location of your web.config file with the Internet Information Services snap-in. The Internet Information Services snap-in is an administration tool for IIS that has been integrated with other administrative functions of Windows Server. To launch the Internet Information Services snap-in:

# Click Start, point to Programs, point to Administrative Tools, and click Computer Management. 
# Under the Server Applications and Services node, expand Internet Information Services. 

To locate where your web.config file is:
# Launch the IIS snap-in as described above
# Expand the Web Sites item in the tree
# Locate the web site running your SharePoint installation (normally Default Web Site)
# Right-click on it and select Properties
# Select the Home Directory tab

The Local path contains the physical location of where the web.config file will be.

Inside the web.config file they'll be a section called SafeControls with entries similar to this:

{{
<SafeControls>
  <SafeControl 
    Assembly="System.Web, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=.." 
    Namespace="System.Web.UI.WebControls" TypeName="*" Safe="True" />
  <SafeControl 
    Assembly="System.Web, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=..." 
    Namespace="System.Web.UI.HtmlControls" TypeName="*" Safe="True" />
  <SafeControl 
    Assembly="Microsoft.SharePoint, Version=11.0.0.0, Culture=neutral, PublicKeyToken=..." 
    Namespace="Microsoft.SharePoint" TypeName="*" Safe="True" />
  ...
</SafeControls>
}}

To register a Web Part as a safe control for use in Windows SharePoint Services, in the SafeControls block, add a SafeControl element that specifies the Web Part in the web.config file.

Add the following entries for the SharePoint Forums Web Parts to this section:

{{
<SafeControl 
  Assembly="BilSimser.SharePoint.WebParts.Forums, Version=1.0.0.0, Culture=neutral, PublicKeyToken=e516dadc23877c32" 
  Namespace="BilSimser.SharePoint.WebParts.Forums.Controls" TypeName="*" Safe="True" />
<SafeControl 
  Assembly="BilSimser.SharePoint.WebParts.Forums, Version=1.0.0.0, Culture=neutral, PublicKeyToken=e516dadc23877c32" 
  Namespace="BilSimser.SharePoint.WebParts.Forums" TypeName="*" Safe="True" />
<SafeControl 
  Assembly="BilSimser.SharePoint.WebParts.Forums, Version=1.0.0.0, Culture=neutral, PublicKeyToken=e516dadc23877c32" 
  Namespace="BilSimser.SharePoint.WebParts.Forums.Controls.Common" TypeName="*" Safe="True" />
<SafeControl 
  Assembly="BilSimser.SharePoint.WebParts.Forums, Version=1.0.0.0, Culture=neutral, PublicKeyToken=e516dadc23877c32" 
  Namespace="BilSimser.SharePoint.WebParts.Forums.Controls.Base" TypeName="*" Safe="True" />
}}

Now the SharePoint Forums Web Parts are registered as safe in your SharePoint installation.