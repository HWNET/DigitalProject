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
        public string CasesCategoryName { get; set; } //案例类别 名称
        public string CasesCategoryPicture { get; set; } //案例类别 图片
        public string CasesCategoryContent { get; set; } //案例类别 内容
        public int CasesCategoryParentID { get; set; } //案例类别 父子级 , 顶级类别 : CasesCategoryParentID=0
        public int CasesCategoryOrderID { get; set; } //排序值

        public int CompanyID { get; set; } //所属企业ID
        public int UpdateStatus { get; set; } //0 表示不更新 1表示新增加 2 表示更新 3表示删除的 
        public virtual ICollection<CasesModel> CasesModels { get; set; } //企业案例
    }
}
