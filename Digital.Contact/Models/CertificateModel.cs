using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Digital.Contact.Models
{
    [Table("CompanyCertificate", Schema = "dbo")]
    public class CertificateModel
    {
        [Key]
        public int CertificateID { get; set; }
        public string CertificateName { get; set; } // 资质名称
        public string CertificateThumbnail { get; set; } // 资质图片
        public string CertificateIntros { get; set; } // 资质描述

        public int CompanyID { get; set; } //所属企业ID
        [NotMapped]
        public virtual CompanyModel CompanyModel { get; set; }
    }
}
