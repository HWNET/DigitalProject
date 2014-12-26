using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Digital.Contact.Models
{
    [Table("Cases", Schema = "dbo")]
    public class CasesModel
    {
        [Key]
        public int CasesID { get; set; }
        
        //[Required(ErrorMessage = "必填")]
        //[StringLength(256, MinimumLength = 1, ErrorMessage = "{1}到{0}个字")]
        //[Display(Name = "案例名称")]
        public string CasesName { get; set; } //案例名称


        public int CompanyID { get; set; } //所属企业ID
         [NotMapped]
        public virtual CompanyModel CompanyModel { get; set; }

        //[StringLength(256, MinimumLength = 1, ErrorMessage = "{1}到{0}个字")]
        //[Display(Name = "案例概述")]
        public string CasesAbstract { get; set; } //案例概述

        //[Required(ErrorMessage = "必填")]
        //[StringLength(256, MinimumLength = 1, ErrorMessage = "{1}到{0}个字")]
        //[Display(Name = "案例缩略图")]
        public string CasesThumbnail { get; set; } //案例缩略图

        //[Required]
        //[Display(Name = "案例日期")]
        public DateTime CasesDate { get; set; } //案例日期

        //[Required]
        //[Display(Name = "案例类别")]
        public int CasesCategoryID { get; set; } //案例所属分类
        [NotMapped]
        public virtual CasesCategoryModel CasesCategoryModel { get; set; }

        //[Required]
        //[Display(Name = "排序值")]
        public string CasesOrderBy { get; set; } //排序值

        public string CasesLabels { get; set; } //标签
        public string CasesDetails { get; set; } //详细描述

        public int UpdateStatus { get; set; } //0 表示不更新 1表示新增加 2 表示更新 3表示删除的 
    }
}
