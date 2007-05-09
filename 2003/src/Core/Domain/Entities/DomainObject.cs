namespace BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities
{
	public class DomainObject
	{
		private int id;
		private string name;

		public int Id
		{
			get { return id; }
			set { id = value; }
		}
		
		public string Name
		{
			get { return name; }
			set { name = value; }
		}
		
		public override string ToString()
		{
			return Name;
		}
	}
}