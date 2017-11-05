### Frequently Asked Questions (FAQ)
This is a basic Frequently Asked Questions (FAQ) page which attempts to answer some of the more commonly asked questions about the SharePoint Forums Web Part.

* **Where are all the messages stored?**
	* Everything in the Web Part is stored in a series of lists that are created if they don't exist when a user visits the page. The lists are hidden after creation so you normally won't see them, but opening the site in FrontPage will show them. These are standard SharePoint lists built from the Custom list template. It's recommended that you do _not_ edit these directly normally, as this may throw parts of the system out of sync.

* **I cannot see the Admin options after I installed the Web Part?**
	* In order to see the Admin options, you must be either a) the first user in the system (user #1) or b) a site administrator where you deployed the web part. If you don't see the option but think you should you can edit the users list directly by going to http://_servername_/Lists/spforums_users/AllItems.aspx. Find your entry and edit the item, setting the IsAdmin flag to true.

* **I downloaded the Web Part but how do I install it?**
	* Complete installation instructions, with examples for configuration, are available here on the Wiki in the section [Installing the Web Part](Installing-the-Web-Part)

* **I've followed everything about security but it _still_ doesn't work?**
	* Check the user id that is running your Application Pool that's attached to the SharePoint installation. If it's a system account, it may not have enough privledges to operate. It should be a domain account, or if there's no domain available then a local admin account.

* **I placed a second copy of the Web Part on the page but cannot remove it now?**
	* The Web Part only supports on instance on a portal area or WSS site. If you do put two copies on a page, javascript errors will prevent you from removing the web part normally. You need to put the page into maintenance mode by adding "?contents=1" to the URL and removing the extra web part that way.

* **How do I add support for my language?**
	* As of version 1.1, the Web Part supports multiple languages but requires translation files in order to present that language. Full instructions on creating and contributing language translations can be found here: [Multiple Language Support](Multiple-Language-Support)

* **Can I run the forums on SPS and WSS?**
	* Generally yes, however there are some known problems running the forums web part on an SPS area compared to a WSS site. It can be run from within a web part page in a document library but again, there are some issues with redirection that are not completely working. If you have any specific setups that are causing you problems, please post them in the forums here on CodePlex and we'll try to help. In the end, it's best to run it on a WSS site deployed to the default.aspx page (the main page) as this setup works in all situations.

* **How do I change the date format in my Forums?**
	* As of version 1.2, you can completely customize the date format by using the Admin -> Configuration screen. This lets you enter a date format string which is used anywhere in the system where a date/time is shown. Full instructions on the date format can be found in here: [Configuring the System](Configuring-the-System).