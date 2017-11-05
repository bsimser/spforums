### Installing the Web Part - Step 1

The SharePoint Forums Web Part uses some common routines. Typical routines for accessing SharePoint lists, doing impersonation, etc. These are housed in an assembly named "BilSimser.SharePoint.Common.dll". 

As this assembly is meant to be used by other projects (including your own), in order to manage versions of this assembly (as new features are added to it) it needs to reside in the Global Assembly Cache (aka the GAC).

You can either do it manually by simply copying and pasting the assembly into the GAC, which is located at:

{{
c:\winnt\assembly
}}

or you can also use the utility **gacutil.exe** that is installed with the .NET framework. On the server from a command prompt, in the same directory where you unzipped BilSimser.SharePoint.Common.dll to, run gacutil.exe:

{{
C:\download>gacutil /i BilSimser.SharePoint.Common.dll

Microsoft (R) .NET Global Assembly Cache Utility.
Version 1.0.2914.16
Copyright (C) Microsoft Corp. 1998-2001. All rights reserved.

Assembly successfully added to the cache

C:\download>
}}

Here the /i option is for installation. And for removing an assembly from the GAC, you can use the same utility as follows.

{{
gacutil /u BilSimser.SharePoint.Common.dll 
}}

The /u option here is for uninstalling or removing an assembly from the cache.

The assembly can now be used from other assemblies on the server, regardless of their physical location.