using System;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;
using NUnit.Framework;

namespace BilSimser.SharePoint.WebParts.Forums.UnitTests
{
	[TestFixture]
	public class DomainObjectFixture
	{
		[Test]
		public void ToStringMethodShouldBeSameAsNameProperty()
		{
			DomainObject obj = new DomainObject();
			obj.Name = "Test";
			Assert.AreEqual("Test", obj.ToString());
		}
	}
}
