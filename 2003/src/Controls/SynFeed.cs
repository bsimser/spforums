#region Using Directives

using System;
using System.Web.UI;
using BilSimser.SharePoint.WebParts.Forums.Controls.Base;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Collections;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;
using Rss;

#endregion

namespace BilSimser.SharePoint.WebParts.Forums.Controls
{
	public class SynFeed : BaseForumControl
	{
		protected override void Render(HtmlTextWriter writer)
		{
			RssChannel channel = new RssChannel();

			TopicCollection topics = RepositoryRegistry.TopicRepository.FindByForumId(forumID);
			foreach (Topic topic in topics)
			{
				RssItem item = new RssItem();
				item.Title = topic.Name;
				item.Description = topic.Name;
				item.PubDate = DateTime.Now.ToUniversalTime();
				item.Author = topic.Author.Name;
				item.Link = new Uri(ForumApplication.Instance.GetLink(SharePointForumControls.ViewMessages, "topic={0}", topic.Id));
				channel.Items.Add(item);
			}

			channel.Title = ForumApplication.Instance.Title;
			channel.Description = ForumApplication.Instance.Title;
			channel.LastBuildDate = channel.Items.LatestPubDate();
			channel.Link = new Uri(ForumApplication.Instance.BasePath);

			RssFeed feed = new RssFeed();
			feed.Channels.Add(channel);
			
			Page.Response.Clear();
			Page.Response.ContentType = "text/xml";
			feed.Write(Page.Response.OutputStream);
			Page.Response.End();
		}

	}
}
