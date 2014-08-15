using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Digital.Contact.Models
{
    public class TempColumnModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "必填")]
        [Display(Name = "是否为主键")]
        public bool IsPrimaryKey { get; set; }
        [Required(ErrorMessage = "必填")]
        [Display(Name = "显示字段名称")]
        public string ColumnDisplayName { get; set; }
        [Required(ErrorMessage = "必填")]
        [Display(Name = "字段数据库名称")]
        public string ColumnName { get; set; }
        [Required(ErrorMessage = "必填")]
        [Display(Name = "数据类型")]
        public string Type { get; set; }
        [Required(ErrorMessage = "必填")]
        [Display(Name = "数据长度")]
        public string ColumnLength { get; set; }
        [Required(ErrorMessage = "必填")]
        [Display(Name = "是否多选")]
        public bool IsMultiSelected { get; set; }
        [Required(ErrorMessage = "必填")]
        [Display(Name = "是否单选")]
        public bool IsOnlyOneCheck { get; set; }
        [Required(ErrorMessage = "必填")]
        [Display(Name = "是否关联属性")]
        public bool IsRelation { get; set; }
        [Required(ErrorMessage = "必填")]
        [Display(Name = "关联属性")]
        public virtual ICollection<RelationTableModels> RelationTableModelslList { get; set; }

        public virtual TemplateModel TemplateModel { get; set; }

    }
}
