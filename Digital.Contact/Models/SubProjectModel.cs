using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Digital.Contact.Models
{
    public class SubProjectModel
    {
        [Key]
        public int SubProjectID { get; set; }
        public int SupplierID { get; set; }//子项目 承包方ID
        public int ProgressID { get; set; }//子项目 进度表ID
        public string SubProjectTitle { get; set; } //子项目标题
        public string Requirements { get; set; }
        public string Details { get; set; }
        public DateTime ExpectedStartTime { get; set; } //希望开始日期
        public DateTime ExpectedEndTime { get; set; } //希望完成日期

        public bool IsHasSubProject { get; set; } //是否有外包出去的子项目
        public bool IsVisibleNext { get; set; }//是否让直属下级公开流程
        public bool IsVisiblePrevious { get; set; }//是否公开流程给直属上级
        public DateTime CreatedTime { get; set; } //项目发布时间
        public int Status { get; set; } // 项目状态

        public int SubProjectParentID { get; set; }
        public virtual SubProjectModel SubProjectParentModel { get; set; } //父级子项目
    }
}
