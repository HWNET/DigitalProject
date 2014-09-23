using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Digital.Contact.Models
{
    public class NewsModel
    {
        [Key]
        public int NewsID { get; set; }
        public string NewsTitle { get; set; }
        public string NewsThumb { get; set; }
        public string NewsBody { get; set; }
        public string Keywords { get; set; }//关联 新闻关键字
        public string Tags { get; set; }
        public int ViewsCount { get; set; }
        public DateTime ReleaseTime { get; set; }

        public virtual ICollection<NewsCommentModel> NewsCommentModels { get; set; }
    }
}
