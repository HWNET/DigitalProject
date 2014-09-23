﻿using System;
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
        public bool IsHasModules { get; set; }
        //企业 子账户
        public int ParentCompanyID { get; set; }
        //是否停用
        public bool IsDisabled { get; set; }

        public virtual ICollection<DepartmentModel> DepartmentModels { get; set; }
    }
}