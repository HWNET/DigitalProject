using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Digital.Contact.Models
{
    public class CasesModel
    {
        [Key]
        public int CasesID { get; set; }
        public string CasesName { get; set; } //案例名称
        public string CasesAbstract { get; set; } //案例概述
        public string CasesThumbnail { get; set; } //案例缩略图
        public DateTime CasesDate { get; set; } //案例日期
        public int CasesCategoryID { get; set; } //案例所属分类
        public string CasesOrderBy { get; set; } //排序值
        public string CasesLabels { get; set; } //标签
        public string CasesDetails { get; set; } //详细描述

        public int UpdateStatus { get; set; } //0 表示不更新 1表示新增加 2 表示更新 3表示删除的 
    }
}
