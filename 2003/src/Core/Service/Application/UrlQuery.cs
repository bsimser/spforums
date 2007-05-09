#region Using Directives

using System;
using System.Collections.Specialized;
using System.Web;

#endregion

namespace BilSimser.SharePoint.WebParts.Forums.Utility
{
	/// <summary>
	/// Helper class to parse and extract values from a
	/// url with query strings in it.
	/// </summary>
	/// <example>
	/// <code>
	/// // create a query object using the current url
	/// MyQuery = new UrlQuery();
	/// // add another parameter (or replace existing one)
	/// MyQuery["myparam"] = "myval";
	/// Trace.Write(MyQuery["myparam"]); // returns 'myval'
	/// // remove parameter
	/// MyQuery["myparam"] = null; // or string.Empty
	/// Trace.Write(MyQuery["myparam"]); // returns ''
	/// </code>
	/// </example>
	/// <remarks>
	/// TODO move to BilSimser.SharePoint.Common library
	/// </remarks>
	public class UrlQuery
	{
		#region Fields

		private NameValueCollection queryString;
		private string url;

		#endregion

		#region Constructors

		/// <summary>
		/// Base on other page
		/// </summary>
		/// <param name="value">The url of the page to reference, i.e.: '/path/to/folder/page.aspx?param1=1¶m2=2'</param>
		public UrlQuery(string value)
		{
			int q = value.IndexOf('?');
			if (q != -1)
			{
				this.url = value.Substring(0, q);
				this.queryString = NameValueCollection(value);
			}
			else
			{
				this.url = value;
			}
		}

		/// <summary>
		/// Based on current page
		/// </summary>
		public UrlQuery()
		{
			this.url = HttpContext.Current.Request.Url.AbsolutePath;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Get the QueryString for the page
		/// </summary>
		public NameValueCollection QueryString
		{
			get
			{
				if (this.queryString != null)
				{
					return this.queryString;
				}
				else
				{
					this.queryString = new NameValueCollection(HttpContext.Current.Request.QueryString);
					return this.queryString;
				}
			}
		}

		/// <summary>
		/// The AbsoluteUri
		/// </summary>
		public string AbsoluteUri
		{
			get { return this.Url + this.Get(); }
		}

		/// <summary>
		/// Returns the virtual folder the page is in
		/// </summary>
		/// <value>/path/to/folder/</value>
		public string VirtualFolder
		{
			get { return this.Url.Substring(0, Url.LastIndexOf("/") + 1); }
		}

		/// <summary>
		/// The Url of the page, without QueryString
		/// </summary>
		/// <value>/path/to/folder/page.aspx</value>
		public string Url
		{
			get { return this.url; }
		}

		/// <summary>
		/// Get and set Url parameters
		/// </summary>
		public string this[string param]
		{
			get { return this.Get(param); }
			set { this.Set(param, value); }
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Set QueryString parameter
		/// </summary>
		/// <param name="param">Parameter to set</param>
		/// <param name="value">Value of parameter</param>
		public void Set(string param, string value)
		{
			if (param != string.Empty)
			{
				if (value == string.Empty || value == null)
				{
					this.QueryString.Remove(param);
				}
				else
				{
					this.QueryString[param] = value;
				}
			}
		}

		/// <summary>
		/// Convert QueryString string to NameValueCollection
		/// </summary>
		public static NameValueCollection NameValueCollection(string qs)
		{
			NameValueCollection nvc = new NameValueCollection();
			//strip string data before the question mark
			qs = qs.IndexOf('?') > 0 ? qs.Remove(0, qs.IndexOf('?') + 1) : qs;
			Array sqarr = qs.Split("&".ToCharArray());
			for (int i = 0; i < sqarr.Length; i++)
			{
				string[] pairs = sqarr.GetValue(i).ToString().Split("=".ToCharArray());
				nvc.Add(pairs[0], pairs[1]);
			}
			return nvc;
		}

		/// <summary>
		/// Copies a form paramater to the QueryString
		/// </summary>
		/// <param name="param">Form Parameter</param>
		public void FormToQuery(string param)
		{
			this.Set(param, HttpContext.Current.Request.Form[param]);
		}

		/// <summary>
		/// Get parameter from QueryString
		/// </summary>
		/// <param name="param">Parameter to get</param>
		/// <returns>Parameter Value</returns>
		public string Get(string param)
		{
			return this.QueryString[param];
		}

		/// <summary>
		/// Get the QueryString
		/// </summary>
		/// <returns>String in the format ?param1=1&2=2</returns>
		public string Get()
		{
			string query = "";
			if (this.QueryString.Count != 0)
			{
				query = "?";
				for (int i = 0; i <= this.QueryString.Count - 1; i++)
				{
					if (i != 0)
					{
						query += "&";
					}
					query += this.QueryString.GetKey(i) + "=" + this.QueryString.Get(i);
				}
			}
			return query;
		}

		#endregion
	}
}