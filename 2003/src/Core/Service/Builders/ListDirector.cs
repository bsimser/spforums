namespace BilSimser.SharePoint.WebParts.Forums.Core.Service.Builders
{
	/// <summary>
	/// Summary description for ListDirector.
	/// </summary>
	public class ListDirector
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ListDirector"/> class.
		/// </summary>
		public ListDirector()
		{
		}

		/// <summary>
		/// Constructs the list.
		/// </summary>
		/// <param name="builder">The builder.</param>
		public void ConstructList(ListBuilder builder)
		{
			builder.CreateList();
			builder.AddFields();
			if (builder.CreateDefaultValues)
				builder.AddSampleData();
			builder.HideList();
		}
	}
}