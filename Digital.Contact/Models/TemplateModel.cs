using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Digital.Contact.Models
{
    public class TemplateModel
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "必填")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "{1}到{0}个字")]
        [Display(Name = "行业名称")]
        public string Name { get; set; }
        [Display(Name = "数据库表名")]
        public string TableName { get; set; }
        [Display(Name = "是否创建")]
        public bool IsCreate { get; set; }

        public virtual ICollection<TempColumnModel> ColumnModelList { get; set; }
    }
}
