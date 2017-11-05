### Installing the Web Part - Step 4

The SharePoint Forums Web Parts perform all reads and writes to/from the underlying lists by impersonation. They impersonate the Application Pool user automatically so you don't have to grant edit permissions for all your users.

In order to do this, a custom security policy has to be defined and trust level setup for the Forums to do their job.

By default, SharePoint has two policies setup in the web.config file. These are "WSS_Minimal" and "WSS_Medium". It's best to create your own policy by copying one of these (WSS_Medium) to a custom one and modifying it. We'll do this with the following steps:

# Locate the WSS_Medium.config file in the location specified in the policyFile property
# Copy this file to wss_customtrust.config in the same location

The default location is:
C:\Program Files\Common Files\Microsoft Shared\Web Server Extensions\60\config

In this policy file you will need to add the following section, right after the FirstMatchCodeGroup: 

{{
<CodeGroup 
	class="UnionCodeGroup" 
	version="1" 
	PermissionSetName="FullTrust">
	<IMembershipCondition 
		version="1" 
		class="StrongNameMembershipCondition" 
		PublicKeyBlob="0024000004800000940000000602000000240
		000525341310004000001000100F15CA89A80D
		45C052CC5003DDAD661CEA98168E5B12A7BEC2
		A8B455D1E7D043C9248BC192A16B02B4D1CCF4
		1738C31797CFFED01C70EE6247222243FA3B10
		706368EDE73C57BAF586582F83CB9391DA711D
		FF5B8169A9AD6169D6023B5C6572136233AC33
		1010CE4C808143B2E2AB18FE59A872340DB76F
		71180623789336DAB">
	</IMembershipCondition>
</CodeGroup>
}}

_NOTE: The PublicKeyBlog needs to be a single line in the config file, but it's broken up here into multiple lines for clarity._

By adding this section to the policy file you will assign full trust permissions to all the code that is signed with that specific public key blob. 

**IMPORTANT** This entry **must** be _after_ the FirstMatchCodeGroup and _before_ the UnionCodeGroup with the PermissionSetName of ASP.NET in order to work correctly.

Before:
{{
<CodeGroup 
	class="FirstMatchCodeGroup" 
	version="1"
	PermissionSetName="Nothing">
	<IMembershipCondition 
		class="AllMembershipCondition"
		version="1"
	/>

<CodeGroup 
	class="UnionCodeGroup"
	version="1"
	PermissionSetName="ASP.Net">
	<IMembershipCondition 
		class="UrlMembershipCondition"
		version="1"
		Url="$AppDirUrl$/*"
	/>
}}

After:
{{
<CodeGroup 
	class="FirstMatchCodeGroup" 
	version="1"
	PermissionSetName="Nothing">
	<IMembershipCondition 
		class="AllMembershipCondition"
		version="1"
	/>

<CodeGroup 
	class="UnionCodeGroup" 
	version="1" 
	PermissionSetName="FullTrust">
	<IMembershipCondition 
		version="1" 
		class="StrongNameMembershipCondition" 
		PublicKeyBlob="0024000004800000940000000602000000240
		000525341310004000001000100F15CA89A80D
		45C052CC5003DDAD661CEA98168E5B12A7BEC2
		A8B455D1E7D043C9248BC192A16B02B4D1CCF4
		1738C31797CFFED01C70EE6247222243FA3B10
		706368EDE73C57BAF586582F83CB9391DA711D
		FF5B8169A9AD6169D6023B5C6572136233AC33
		1010CE4C808143B2E2AB18FE59A872340DB76F
		71180623789336DAB">
	</IMembershipCondition>
</CodeGroup>

<CodeGroup 
	class="UnionCodeGroup"
	version="1"
	PermissionSetName="ASP.Net">
	<IMembershipCondition 
		class="UrlMembershipCondition"
		version="1"
		Url="$AppDirUrl$/*"
	/>
}}

Now we'll create a new trust level entry in the securityPolicy section in web.config to define it. 

Find this section in web.config:
{{
<trustLevel name="WSS_Minimal" policyFile="C:\Program Files\Common Files\Microsoft Shared\Web Server Extensions\60\config\wss_minimaltrust.config" /> 
}}

And add an entry after it like this:
{{
<trustLevel name="WSS_Custom" policyFile="C:\Program Files\Common Files\Microsoft Shared\Web Server Extensions\60\config\wss_customtrust.config" /> 
}}

Now we need to tell web.config (and SharePoint) that we want to use this new custom policy file.

Find this section in web.config:
{{
<trust level="WSS_Minimal" originUrl="" />
}}

And change it to this:
{{
<trust level="WSS_Custom" originUrl="" />
}}

Note: Your trust level might be set to Full or something else.

Now the SharePoint Forums are ready to be added to a web page.