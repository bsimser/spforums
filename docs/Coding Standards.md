### Coding Standards

The source code for the SharePoint Forums Web Part follows some basic standards with regards to formmatting, naming, and organization. This page describes those standards.

**Regions**
All code is organized by region. There are 7 standard regions:
* Using Directives
* Fields
* Constructors
* Properties
* Public Methods
* Protected Methods
* Private Methods
Code in each source file will be in one of these regions.

**Accessors**
Access to private member variables is only allowed through public or protected properties. Any business rules go into the property code so no variable should be accessed directly (even in a constructor).

**Naming Conventions**
Private methods and member variables (including local variables) are to be written in camlCase. Public methods and properties use PascalCase. Examples:
	* private getUserId();
	* private int id;
	* public GetAllUsers();
	* public string Name { get { return name; } }

**Initializing Variables**
Any native types, collections, etc. should be initialized in their declaration rather than in constructors. This prevents any issues of variables not initialized in multiple constructors.

Do this:
{{
public class Forum
{
  private Hashtable userHashTable = new Hashtable();

  public Forum()
  {
  }
}
}}

Not this:
{{
public class Forum
{
  private Hashtable userHashTable;

  public Forum()
  {
    userHashTable = new Hashtable();
  }
}
}}