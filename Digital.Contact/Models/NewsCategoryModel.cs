using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Digital.Contact.Models
{
    public class NewsCategoryModel
    {
        [Key]
        public int NewsCategoryID { get; set; }
        public string NewsCategoryName { get; set; } //播报类别 名称
        public string NewsCategoryPicture { get; set; } //播报类别 图片
        public string NewsCategoryContent { get; set; } //播报类别 内容
        public int NewsCategoryParentID { get; set; } //播报类别 父子级 , 顶级类别 : NewsCategoryParentID=0
        public int NewsCategoryOrderID { get; set; } //排序值

        public int CompanyID { get; set; } //所属企业ID
        public int UpdateStatus { get; set; } //0 表示不更新 1表示新增加 2 表示更新 3表示删除的 

        public virtual ICollection<NewsModel> NewsModels { get; set; }
    }
}
