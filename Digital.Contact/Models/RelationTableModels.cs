using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Digital.Contact.Models
{
    public class RelationTableModels
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "属性")]
        public string ColumnName { get; set; }
        [Display(Name = "属性值")]
        public string ColumnValue { get; set; }

        public virtual TempColumnModel TempColumnModel { get; set; }
    }
}
