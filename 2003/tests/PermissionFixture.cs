using System;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;
using NUnit.Framework;

namespace BilSimser.SharePoint.WebParts.Forums.UnitTests
{
	[TestFixture]
	public class PermissionFixture
	{
		private Permission testPermissionObject;

		[SetUp]
		public void SetUp()
		{
			testPermissionObject = new Permission();
		}

		[Test]
		public void AllRightsAreFalseOnCreation()
		{
			for(int i=0; i<(int)Permission.Rights.LastRight; i++)
			{
				Permission.Rights right = (Permission.Rights)i;
				Assert.AreEqual(false, testPermissionObject.HasPermission(right));
			}
		}

		[Test]
		[ExpectedException(typeof(ApplicationException))]
		public void CreateWithEmptyString()
		{
			Permission permissionFromString = new Permission("");
			Assert.Fail(String.Format("Permission create from empty string has permissions of {0}", permissionFromString.ToString()));
		}

		[Test]
		[ExpectedException(typeof(ApplicationException))]
		public void CreateWithNullValue()
		{
			Permission permissionFromString = new Permission(null);
			Assert.Fail(String.Format("Permission create from null has permissions of {0}", permissionFromString.ToString()));
		}

		[Test]
		[ExpectedException(typeof(ApplicationException))]
		public void CreateWithInvalidLengthString()
		{
			Permission permissionFromString = new Permission("1");
			Assert.Fail(String.Format("Permission create with short string has permissions of {0}", permissionFromString.ToString()));
		}

		[Test]
		[ExpectedException(typeof(ApplicationException))]
		public void CreateWithInvalidStringButCorrectLength()
		{
			string invalidPermString = "";
			invalidPermString = invalidPermString.PadRight((int)Permission.Rights.LastRight, 'X');
			Permission permissionFromString = new Permission(invalidPermString);
			Assert.Fail(String.Format("Permission create with invalid string of {0} has permissions of {1}", invalidPermString, permissionFromString.ToString()));
		}

		[Test]
		public void SetAllRightsTrue()
		{
			testPermissionObject.SetAllRights(true);
			for(int i=0; i<(int)Permission.Rights.LastRight; i++)
			{
				Permission.Rights right = (Permission.Rights)i;
				Assert.AreEqual(true, testPermissionObject.HasPermission(right));
			}
		}

		[Test]
		public void SetAllRightsFalse()
		{
			testPermissionObject.SetAllRights(false);
			for(int i=0; i<(int)Permission.Rights.LastRight; i++)
			{
				Permission.Rights right = (Permission.Rights)i;
				Assert.AreEqual(false, testPermissionObject.HasPermission(right));
			}
		}

		[Test]
		public void PermissionToStringIsCorrectLength()
		{
			Assert.AreEqual(Convert.ToInt32(Permission.Rights.LastRight), testPermissionObject.ToString().Length);
		}

		[Test]
		public void CreatePermissionFromString()
		{
			testPermissionObject.SetPermission(Permission.Rights.Read, true);
			string s = testPermissionObject.ToString();
			Permission permissionFromString = new Permission(s);
			Assert.AreEqual(true, permissionFromString.HasPermission(Permission.Rights.Read));
		}

		[Test]
		public void CreatePermissionFromStringAndCheckTwoRights()
		{
			testPermissionObject.SetPermission(Permission.Rights.Read, true);
			testPermissionObject.SetPermission(Permission.Rights.Moderate, true);
			string s = testPermissionObject.ToString();
			Permission permissionFromString = new Permission(s);
			Assert.AreEqual(true, permissionFromString.HasPermission(Permission.Rights.Read));
			Assert.AreEqual(true, permissionFromString.HasPermission(Permission.Rights.Moderate));
		}
	}
}
