using System;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;
using NUnit.Framework;

namespace BilSimser.SharePoint.WebParts.Forums.UnitTests
{
	[TestFixture]
	public class CategoryFixture
	{
		[Test]
		[Ignore("Have to implement lazy load of repository via interfaces for this to work")]
		public void UserShouldHaveAccess()
		{
			ForumUser user = new ForumUser();
			Category category = new Category("Test");
			Assert.AreEqual(true, category.HasAccess(user, Permission.Rights.Add));
		}
	}
}
