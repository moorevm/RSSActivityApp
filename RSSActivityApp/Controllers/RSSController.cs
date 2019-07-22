using RSSActivityApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace RSSActivityApp.Controllers
{
    public class RSSController : Controller
    {
        // GET: RSS
        public ActionResult Index()
        {
            ViewBag.Title = "Check RSS Inactivity";
            return View();
        }

        [HttpGet]
        public ActionResult GetRssFeeds(double numOfDays)
        {
            RSSData model = new RSSData();
            //Get number of days of inactivity
            model.NumOfDays = numOfDays;
            //Get rss feeds that were inactive for at least x number of days
            model.RssFeeds = SearchFeeds(GetRssData(), numOfDays);
            return PartialView("Partial/GetRssFeeds", model);
        }

        public Dictionary<string,string> SearchFeeds(Dictionary<string,string> feeds, double numOfDays)
        {
            Dictionary<string,string> results = new Dictionary<string, string>();
            foreach (KeyValuePair<string,string> item in feeds)
            {
                //Check for activity in the last [value given by user(numOfDays)] days
                if (GetRssPubDate(item.Value, numOfDays))
                {
                    results.Add(item.Key, item.Value);
                }
            }

            return results;
        }

        public Dictionary<string, string> GetRssData()
        {
            return new Dictionary<string, string>()
            {
                { "ESPN Oklahoma", "https://www.espn.com/espn/rss/recruiting/oklahoma/news" },
                { "Diane Rehm Show", "https://dianerehm.org/rss/npr/dr_podcast.xml" },
                { "ESPN Florida", "https://www.espn.com/espn/rss/recruiting/florida/news" },
                { "BBC World", "http://feeds.bbci.co.uk/news/world/rss.xml" },
                { "BBC Business", "http://feeds.bbci.co.uk/news/business/rss.xml" },
                { "Joe Rogan", "http://podcasts.joerogan.net/feed" },
                { "ESPN Top Headlines", "https://www.espn.com/espn/rss/news" },
                { "BBC Health", "http://feeds.bbci.co.uk/news/health/rss.xml" },
                { "BBC Technology", "http://feeds.bbci.co.uk/news/technology/rss.xml" }
            };
        }

        public bool GetRssPubDate(string url, double numOfDays)
        {
            //XmlReaderSettings settings = new XmlReaderSettings();
            //settings.DtdProcessing = DtdProcessing.Parse;
            //settings.MaxCharactersFromEntities = 1024;
            var r = XmlReader.Create(url);//, settings);
            var albums = SyndicationFeed.Load(r);
            string albumRSS = "";
            SyndicationFeed photos;
            r.Close();
            foreach (SyndicationItem album in albums.Items)
            {
                DateTime today = DateTime.Now;
                DateTime pubDate = album.PublishDate.DateTime;

                //if there's activity in the last [numOfDays] days return false
                if (today.Date.AddDays(-numOfDays) < pubDate.Date)
                {
                    return false;
                }

                //// album.links[0].URI points to this album page on spaces.live.com
                //// album.Summary (not shown) is an HTML block with thumbnails of the album pics
                ////cell.Text = string.Format("<a href='{0}'>{1}</a>", album.Links[0].Uri, album.Title.Text);
                //albumRSS = GetAlbumRSS(album);
                //r = XmlReader.Create(albumRSS);
                //photos = SyndicationFeed.Load(r);
                //r.Close();
                //foreach (SyndicationItem photo in photos.Items)
                //{
                //    // photo.Summary is an HTML block with a thumbnail of the pic
                //    //cell.Text = string.Format("{0}", photo.Summary.Text);
                //}


            }

            return true;
        }
        //
        // helper to extract the feed to one album from the albums feed
        //
        private string GetAlbumRSS(SyndicationItem album)
        {



            string url = "";
            foreach (SyndicationElementExtension ext in album.ElementExtensions)
                if (ext.OuterName == "itemRSS") url = ext.GetObject<string>();
            return (url);


        }
        
    }
}