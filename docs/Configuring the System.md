### Configuring the System

Currently the only options are to change the name of the system and the date format. To do this:

# Select **Configuration** from the Administration Control Panel
# Enter the name desired for your forum in the edit box
# Enter the date/time format (see below) for your forum
# Click on Submit to save your changes, Cancel to abort

The name will be displayed in the Breadcrumb trail and on other parts of the system (including RSS feeds). Dates will be displayed in the format specified anywhere they appear on the Forums.

**Date/Time formatting**

The SharePoint Forums Web Part (version 1.2 and higher) provides the ability to set the default date/time format when displaying dates in the system. This is used on the last post of a topic, individual messages, and user profiles. Basically anywhere a date/time stamp is displayed you have control over the format.

The format is based on how your server is set so "d" on a US setting is Month/Day/Year whereas on a Canadian setting it's Day/Month/Year. The following values will produce the output shown for August 7, 2006 at 11:30:00 AM:

d - 8/7/2006
D - Monday, August 07, 2006
f - Monday, August 07, 2006 11:30 AM
F - Monday, August 07, 2006 11:30:00 AM
g - 8/7/2006 11:30 AM
G - 8/7/2006 11:30:00 AM
m,M - August 07
r,R - Mon, 07 Aug 2006 11:30:00 GMT
s - 2006-08-07T11:30:00
t - 11:30 AM
T - 11:30:00 AM
u - 2006-08-07 11:30:00Z
U - Monday, August 07, 2006 5:30:00 PM
y - August, 2006

You can also create a custom pattern using the following formatters. This means that if you don't like the format above, you can just create your own.

d - The day of the month. Single-digit days will not have a leading zero. 
dd - The day of the month. Single-digit days will have a leading zero. 
ddd - The abbreviated name of the day of the week, as defined in AbbreviatedDayNames. 
dddd - The full name of the day of the week, as defined in DayNames. 
M - The numeric month. Single-digit months will not have a leading zero. 
MM - The numeric month. Single-digit months will have a leading zero. 
MMM - The abbreviated name of the month, as defined in AbbreviatedMonthNames. 
MMMM - The full name of the month, as defined in MonthNames. 
y - The year without the century. If the year without the century is less than 10, the year is displayed with no leading zero. 
yy - The year without the century. If the year without the century is less than 10, the year is displayed with a leading zero. 
yyyy - The year in four digits, including the century. 
gg - The period or era. This pattern is ignored if the date to be formatted does not have an associated period or era string. 
h - The hour in a 12-hour clock. Single-digit hours will not have a leading zero. 
hh - The hour in a 12-hour clock. Single-digit hours will have a leading zero. 
H - The hour in a 24-hour clock. Single-digit hours will not have a leading zero. 
HH - The hour in a 24-hour clock. Single-digit hours will have a leading zero. 
m - The minute. Single-digit minutes will not have a leading zero. 
mm - The minute. Single-digit minutes will have a leading zero. 
s - The second. Single-digit seconds will not have a leading zero. 
ss - The second. Single-digit seconds will have a leading zero. 
f - The fraction of a second in single-digit precision. The remaining digits are truncated. 
ff - The fraction of a second in double-digit precision. The remaining digits are truncated. 
fff - The fraction of a second in three-digit precision. The remaining digits are truncated. 
ffff - The fraction of a second in four-digit precision. The remaining digits are truncated. 
fffff - The fraction of a second in five-digit precision. The remaining digits are truncated. 
ffffff - The fraction of a second in six-digit precision. The remaining digits are truncated. 
fffffff - The fraction of a second in seven-digit precision. The remaining digits are truncated. 
t - The first character in the AM/PM designator defined in AMDesignator or PMDesignator, if any. 
tt - The AM/PM designator defined in AMDesignator or PMDesignator, if any. 
z - The time zone offset ("+" or "-" followed by the hour only). Single-digit hours will not have a leading zero. For example, Pacific Standard Time is "-8". 
zz - The time zone offset ("+" or "-" followed by the hour only). Single-digit hours will have a leading zero. For example, Pacific Standard Time is "-08". 
zzz - The full time zone offset ("+" or "-" followed by the hour and minutes). Single-digit hours and minutes will have leading zeros. For example, Pacific Standard Time is "-08:00". 
: - The default time separator defined in TimeSeparator. 
/ - The default date separator defined in DateSeparator. 
% c  - Where c is a format pattern if used alone. The "%" character can be omitted if the format pattern is combined with literal characters or other format patterns. 
\ c  - Where c is any character. Displays the character literally. To display the backslash character, use "\\". 

Note: these are all standard .NET formaters for DateTimeInfo, but documented here for users of the Web Part who are not .NET developers.