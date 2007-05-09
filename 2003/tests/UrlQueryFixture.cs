using System;
using NUnit.Framework;

namespace BilSimser.SharePoint.WebParts.Forums.UnitTests
{
	/// <summary>
	/// Summary description for UrlQueryFixture.
	/// </summary>
	[TestFixture]
	public class UrlQueryFixture
	{
		[Test]
		public void CreateEmptyQuery()
		{
			UrlQuery query = new UrlQuery("http://localhost/default.aspx");
			Assert.AreNotEqual(null, query);
			Assert.AreEqual("http://localhost/default.aspx", query.Url);
		}

		[Test]
		public void AddAdditionalParameter()
		{
			UrlQuery query = new UrlQuery("http://localhost/default.aspx?id=1");
			Assert.AreEqual(1, query.QueryString.Count);
			query["myparam"] = "myval";
			Assert.AreEqual("myval", query["myparam"]);
			Assert.AreEqual(2, query.QueryString.Count);
		}

		[Test]
		public void GetIdParameter()
		{
			UrlQuery query = new UrlQuery("http://localhost/default.aspx?id=1");
			Assert.AreEqual(1, query.QueryString.Count);
			Assert.AreEqual(1, Convert.ToInt32(query["id"]));
		}

		[Test]
		public void RemoveParameter()
		{
			UrlQuery query = new UrlQuery("http://localhost/default.aspx?id=1");
			Assert.AreEqual(1, query.QueryString.Count);
			query["id"] = null;
			Assert.AreEqual(null, query["id"]);
			Assert.AreEqual(0, query.QueryString.Count);
		}
	}
}