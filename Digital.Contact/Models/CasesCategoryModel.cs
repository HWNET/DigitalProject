using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Digital.Contact.Models
{
    [Table("CasesCategory",Schema="dbo")]
    public class CasesCategoryModel
    {
        [Key]
        public int CasesCategoryID { get; set; }

        [Required(ErrorMessage = "必填")]
        [StringLength(256, MinimumLength = 1, ErrorMessage = "{1}到{0}个字")]
        [Display(Name = "类别名称")]
        public string CasesCategoryName { get; set; } //案例类别 名称

        [Required(ErrorMessage = "必填")]
        [StringLength(256, MinimumLength = 1, ErrorMessage = "{1}到{0}个字")]
        [Display(Name = "类别图片")]
        public string CasesCategoryPicture { get; set; } //案例类别 图片

        [DataType(DataType.Text)]
        [Display(Name = "类别内容")]
        public string CasesCategoryContent { get; set; } //案例类别 内容

        [Required]
        [Display(Name = "顶级类别")]
        public int CasesCategoryParentID { get; set; } //案例类别 父子级 , 顶级类别 : CasesCategoryParentID=0
        
        [Required]
        [Display(Name = "排序值")]
        public int CasesCategoryOrderID { get; set; } //排序值

        [Required]
        public int CompanyID { get; set; } //所属企业ID
        [NotMapped]
        public virtual CompanyModel CompanyModel { get; set; }

        public int UpdateStatus { get; set; } //0 表示不更新 1表示新增加 2 表示更新 3表示删除的 
        public virtual ICollection<CasesModel> CasesModels { get; set; } //企业案例
    }
}
