using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Digital.Contact.Models
{
    public class ProjectModel
    {
        [Key]
        public int ProjectID { get; set; }
        public int UserID { get; set; } //发布者ID : 1企业用户  2个人用户  3政府用户
        public int SupplierID { get; set; }//总承包方ID
        public int ProgressID { get; set; }//总进度表ID

        public string ProjectName { get; set; }
        public string Requirements { get; set; } //项目需求概述
        public string Details { get; set; } //项目详情
        //???设置隐藏项目详情
        //隐藏后，发布方该信息可以对固定的人或者人群开放浏览

        public DateTime ExpectedEndTime { get; set; } //希望完成日期
        public decimal EstimatedMoney { get; set; } //预估开发资金-即开发资金预算
        public int EstimatedQuantity { get; set; } //预计采购数量-（开发完成量产后）
        public decimal ExpectedPrice { get; set; } //希望单价
        public bool IsHideExpectedPrice { get; set; } //是否隐藏发布方预期的量产单价
        public int ExpectedLeadInterval { get; set; } //???预期供货周期
        public string Reward { get; set; } //???项目开发金奖励
        public bool IsHasSubProject { get; set; } //是否有外包出去的子项目
        public DateTime CreatedTime { get; set; } //项目发布时间

        public int Status { get; set; } // 项目状态 : 1 已发布, 2 已承接 , 3 研发中, 4 等待评估 , 5 已评估 

        //外包 子项目
        public virtual ICollection<SubProjectCategoryModel> SubProjectCategoryModels { get; set; }
    }
}
