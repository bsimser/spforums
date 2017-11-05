### Installing the Web Part
These are quick installation instructions for the SharePoint Forums Web Part. Links in each step orovide more detail for installing.

* **[Step 1](Step-1)**
	* Install BilSimser.SharePoint.Common to the GAC

* **[Step 2](Step-2)**
	* Copy "BilSimser.SharePoint.WebParts.Forums.dll" and "BilSimser.SharePoint.WebParts.Forums.Core.dll" to your SharePoint website bin directory (if it's the default, it's c:\inetpub\wwwroot\bin).

* **[Step 3](Step-3)**
	* Add the following SafeControl entries to your web.config file:
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

* **[Step 4](Step-4)**
	* Add the following to a custom policy file AFTER the FirstMatchCodeGroup and BEFORE the ASP.NET UnionCodeGroup. _Note that the PublicKeyBlob needs to be a continuous line in the file. It is broken up here into separate lines for clarity only_:
{{
<CodeGroup 
	class="UnionCodeGroup" 
	version="1" 
	PermissionSetName="FullTrust">
<IMembershipCondition 
	version="1" 
	class="StrongNameMembershipCondition" 
	PublicKeyBlob="0024000004800000940000000602000000240000525341310004000001000100F15CA89A8
                       0D45C052CC5003DDAD661CEA98168E5B12A7BEC2A8B455D1E7D043C9248BC192A16B02B4D
                       1CCF41738C31797CFFED01C70EE6247222243FA3B10706368EDE73C57BAF586582F83CB93
                       91DA711DFF5B8169A9AD6169D6023B5C6572136233AC331010CE4C808143B2E2AB18FE59A
                       872340DB76F71180623789336DAB">
</IMembershipCondition>
</CodeGroup>
}}

* **[Step 5](Step-5)**
	* Reset Internet Information Services (IIS) on the SharePoint server. Alternately, you can recycle the Application Pool.

* **[Step 6](Step-6)**
	* Import the SharePointForums.dwp onto a WSS site or SPS area page where you are an Administrator.

* **[Step 7](Step-7)**
	* Configure and have fun!