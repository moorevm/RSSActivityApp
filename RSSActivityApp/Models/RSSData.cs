using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RSSActivityApp.Models
{
    public class RSSData
    {
        [Required(ErrorMessage = "Number of Days is required")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Number of Days must be numeric")]
        public double NumOfDays { get; set; }

        public Dictionary<string,string> RssFeeds { get; set; }
    }
}