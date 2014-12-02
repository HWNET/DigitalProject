using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Digital.Contact.Models
{
    [Table("Patents", Schema = "dbo")]
    public class PatentModel
    {
        [Key]
        public int PatentID { get; set; }
        public string PatentNumber { get; set; } //专利号
        public string PatentName { get; set; } //专利名称
        public string PatentAbstract { get; set; } //专利概述
        public string PatentCerificate { get; set; } //专利证书
        public string PatentDate { get; set; } //专利日期
        public string PatentTechnologyDomain { get; set; } //技术领域
        public string PatentDevelopmentStatus { get; set; } //开发状态
        public int PatentOrderID { get; set; } //排序值
        public string PatentLabels { get; set; } //标签
        public string PatentIntro { get; set; } //专利简介

        public bool IsDisabled { get; set; } //是否停用
        public bool IsTransferred { get; set; } //是否转让

        public int CompanyID { get; set; } //所属企业ID
        public int UpdateStatus { get; set; } //0 表示不更新 1表示新增加 2 表示更新 3表示删除的 

    }
}
