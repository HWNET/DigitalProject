using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Digital.Contact.Models
{
    public class CompanyModel
    {
        [Key]
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string Website { get; set; }
        /// <summary>
        /// 企业用户类型 1 方案商  2 成品商  3  供应商
        /// </summary>
        public int CompanyUserTypeID { get; set; }
        public int CompanyUserLevelID { get; set; } //企业 总账户 : 0 即企业Admin, 子账户 : 1 即只能在所属部门进行操作
        public bool IsHasModules { get; set; }

        public int ParentCompanyID { get; set; } //企业 总账户 : ParentCompanyID=0  子账户 : ParentCompanyID=CompanyID 
        public int DepartmentID { get; set; } //子账户所属部门ID
        public bool IsHeader { get; set; } //子账户是否部门负责人
        public int HeaderTitle { get; set; } //部门负责人头衔 ： 1 , 2 , 3 
        public bool IsDisabled { get; set; }//子账户 是否停用

        public string SendAddress { get; set; } //???部门子账户 发货地址 , 最多保存 5 个有效地址；允许设置一个默认地址
        public string ReceiveAddress { get; set; } //???部门子账户 收货地址

        public virtual ICollection<DepartmentModel> DepartmentModels { get; set; }
    }
}
