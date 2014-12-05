using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Digital.Contact.Models
{
    [Table("NewsCategory", Schema = "dbo")]
    public class NewsCategoryModel
    {
        [Key]
        public int NewsCategoryID { get; set; }

        [Required(ErrorMessage = "必填")]
        [StringLength(256, MinimumLength = 1, ErrorMessage = "{1}到{0}个字")]
        [Display(Name = "播报类别")]
        public string NewsCategoryName { get; set; } //播报类别 名称

        [Required(ErrorMessage = "必填")]
        [StringLength(256, MinimumLength = 1, ErrorMessage = "{1}到{0}个字")]
        [Display(Name = "类别图片")]
        public string NewsCategoryPicture { get; set; } //播报类别 图片

        [DataType(DataType.Text)]
        [Display(Name = "类别内容")]
        public string NewsCategoryContent { get; set; } //播报类别 内容

        [Required]
        [Display(Name = "顶级类别")]
        public int NewsCategoryParentID { get; set; } //播报类别 父子级 , 顶级类别 : NewsCategoryParentID=0

        [Required]
        [Display(Name = "排序值")]
        public int NewsCategoryOrderID { get; set; } //排序值

        [Required]
        public int CompanyID { get; set; } //所属企业ID
        [NotMapped]
        public virtual CompanyModel CompanyModel { get; set; }

        public int UpdateStatus { get; set; } //0 表示不更新 1表示新增加 2 表示更新 3表示删除的 
        public virtual ICollection<NewsModel> NewsModels { get; set; }
    }
}
