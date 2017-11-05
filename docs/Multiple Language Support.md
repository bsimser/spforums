### Multiple Language Support

The SharePoint Forums Web Part supports any language that Windows knows about. All it needs is a simple XML file to handle the translations.

**Language File**
The file is named using the following format:
{{
LCID.lng.xml
}}
Where _LCID_ is a locale identifier (LCID) for the language supported. SharePoint Forums ships with the 1033.lng.xml file (English) which will be used if it cannot locate a resource in the desired language.

**File Location**
The language files are located in the following directory (v1.1):
{{
C:\Program Files\Common Files\Microsoft Shared\web server extensions\wpresources\BilSimser.SharePoint.WebParts.Forums\1.1.0.0__e516dadc23877c32
}}

Future versions will have a different folder based on the version number.

**Creating a New Language File**
To create a language file for the SharePoint Forums Web Part, just copy the 1033.lng.xml file to the language of your choice. Here is a list of the numbers to use and the language they refer to:

1025 Arabic 
2052 Chinese - Simplified 
1028 Chinese - Traditional 
1029 Czech 
1030 Danish 
1043 Dutch 
1033 English 
1035 Finnish 
1036 French 
1031 German 
1032 Greek 
1037 Hebrew 
1038 Hungarian 
1040 Italian 
1041 Japanese 
1042 Korean 
1044 Norwegian 
1045 Polish 
2070 Portuguese 
1046 Portuguese - Brazilian 
1049 Russian 
1034 Spanish 
1053 Swedish 
1054 Thai 
1055 Turkish 
1058 Ukranian

_(note: if your language is not listed let me know and I'll add it to the list)_

So, for example, to create the German language file you would copy 1033.lng.xml to 1031.lng.xml and begin editing the tranlations.

**Editing the Translations**
The language file is a simple XML file that looks like this:
{{
<?xml version="1.0" encoding="utf-8" ?> 
<!-- Filename: 1033.lng.xml -->
<!-- Description: xml resource file for English translations -->
<!-- Author: Bil Simser (bsimser@shaw.ca) -->
<strings>
	<string id="Text.Quote">Quote</string>
	<string id="Text.Edit">Edit</string>
	<string id="Text.Posted">Posted</string>
	<string id="Text.NewTopic">New Topic</string>
	<string id="Text.DeleteTopic">Delete Topic</string>
	<string id="Text.Topics">Topics</string>
	<string id="Text.Replies">Replies</string>
	<string id="Text.Author">Author</string>
	<string id="Text.Views">Views</string>
	<string id="Text.LastPost">Last Post</string>
	<string id="Text.RSS">RSS</string>
	<string id="Text.Reply">Reply</string>
</strings>
}}

Once you have a copy of the english language file copied to your own language, simple go in and replace the text used in each tag.

**Example**
Step 1: Copy 1033.lng.xml to 1031.lng.xml (German)
Step 2: Replace the contents of the string id "Text.LastPost" from "Last Post" to "Letzter Pfosten"
Step 3: Save the file
Step 4: Browse to where the forums are installed (with your browser set to German) and the translations will be displayed.

**Right to Left Languages**
There are several languages that read right to left instead of left to right (like Arabic). These not only require the translations to be done but also for some other modifications to the Web Part to support this. This support will be added in a future release and this documentation will be updated accordingly.

**Contributing to the Release**
We need translators! So if you're up to the challenge, grab the 1033.lng.xml file from the location listed above and start translating.

**Taking Credit**
Please remember to change the following items in your submission:
{{
<!-- Filename: NNNN.lng.xml -->
<!-- Description: xml resource file for XXXX translations -->
<!-- Author: Your Name (yourname@yourdomain.com) -->
}}

Where NNNN is the number of the LCID you're translating and XXXX is the name of the language.

When completed, please email a copy of the file (remember to include your name/email in the comments of the file for credit!) to bsimser@shaw.ca.

Your translations will be included in the next planned release of the software.