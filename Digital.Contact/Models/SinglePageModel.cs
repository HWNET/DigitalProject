using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Digital.Contact.Models
{
    public class SinglePageModel
    {
        [Key]
        public int PageID { get; set; }
        public string PageTitle { get; set; } //页面标题
        public string PageKeyWords { get; set; } //关键字
        public string PageDescription { get; set; } //页面描述,限100字以内
        public string PageRelationFlag { get; set; } //页面关联标示
        public string PageBody { get; set; } //页面主体

        public int CompanyID { get; set; } //所属企业ID
        public int UpdateStatus { get; set; } //0 表示不更新 1表示新增加 2 表示更新 3表示删除的 
        public DateTime ModifiedTime { get; set; }
    }
}
