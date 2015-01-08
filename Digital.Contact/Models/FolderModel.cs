using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Digital.Contact.Models
{
    [Table("CompanyFolder", Schema = "dbo")]
    public class FolderModel
    {
        [Key]
        public int FolderID { get; set; }
        public string FolderName { get; set; } // 目录名称
        public string FolderNameCode { get; set; } // 目录名称编码

        public int CompanyID { get; set; } //所属企业ID
        [NotMapped]
        public virtual CompanyModel CompanyModel { get; set; }
    }
}
