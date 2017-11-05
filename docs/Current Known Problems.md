### Current Known Problems

This is a list of known problems with the Web Part and any workarounds or fixes. Also links to work items that are assigned to the problem will be listed here.

* _Saving properties on SPS_
	* The system uses Web Part properties to save information (like the forum name) and retrieve these later. When the web part is placed on a SPS area, saving produces an error saying "Access Denied saving Web Part properties..." (even if you have admin rights). This is being investigated for a future fix.

* _Security errors when installing (v1.0 only)_
	* This is probably the most common problem and happens to most people. A few things to watch out for:
		* Use a domain (not SYSTEM or NETWORK_SERVICE) for the Application Pool account.
		* Do not enable anonymous users on the system (it currently does not support it)
		* Ensure the SafeControl entries are added to the proper web.config file

* _Deploying on SPS areas (v1.1)_
	* There is a problem (related to saving properties in SPS installations) that is causing the use of the web part on SPS areas to be problematic. Several properties are now saved back to the web part and unfortunately they're not working correctly on SPS setups. The workaround is to create a WSS site or Web Part Page in a WSS site to host it and expose this (through a the Page Viewer Web Part) on your portal. As with the above issue, we are looking at what the problem is saving properties on SPS and will correct it as soon as possible.

* _Uninstalling Web Part does not remove hidden lists_
	* If you uninstall the Web Part through the Add/Remove Programs list (v1.1 only) or remove it from a Web Part Page, it does not remove the lists. There are several hidden lists the Web Part uses (and recreates if missing) and these lists are not removed. Unfortunately it's nearly impossible to detect when someone removes the Web Part from a page so we cannot remove the lists automatically. You will have to remove them manually using FrontPage, SharePoint Explorer, or another tool.