using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Digital.Contact.Models
{
    [Table("News", Schema = "dbo")]
    public class NewsModel
    {
        [Key]
        public int NewsID { get; set; }

        //[Required(ErrorMessage = "必填")]
        //[StringLength(256, MinimumLength = 1, ErrorMessage = "{1}到{0}个字")]
        //[Display(Name = "文章标题")]
        public string NewsTitle { get; set; } //文章标题

        //[StringLength(256, MinimumLength = 1, ErrorMessage = "{1}到{0}个字")]
        //[Display(Name = "文章摘要")]
        public string NewsAbstract { get; set; } //文章摘要

        //[Required(ErrorMessage = "必填")]
        //[StringLength(256, MinimumLength = 1, ErrorMessage = "{1}到{0}个字")]
        //[Display(Name = "文章缩略图")]
        public string NewsThumbnail { get; set; } //文章缩略图

        //[Required]
        //[Display(Name = "案例类别")]
        public int NewsCategoryID { get; set; } //所属类别ID
        [NotMapped]
        public virtual NewsCategoryModel NewsCategoryModel { get; set; }

        //[Required]
        //[Display(Name = "排序值")]
        public int NewsOrderID { get; set; } //文章排序值

        //[Required]
        //[StringLength(256, MinimumLength = 1, ErrorMessage = "{1}到{0}个字")]
        //[Display(Name = "关键字")]
        public string NewsKeywords { get; set; }//关键字

        public string NewsLabels { get; set; } //标签

        //[DataType(DataType.Text)]
        //[Display(Name = "文章内容")]
        public string NewsBody { get; set; } //文章内容
        
        public int ViewsCount { get; set; } //文章 被浏览数
        public DateTime ReleaseTime { get; set; } //发布日期

        public virtual ICollection<NewsCommentModel> NewsCommentModels { get; set; }
    }
}
