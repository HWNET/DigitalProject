using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/*
 * Created By Rob Xu
 * Creartd Date: 2015.01.18
 * Updated Date:
 * Ver:1.0.0.0
 */
namespace Digital.Contact.Models
{
    [Table("ProductDetail", Schema = "dbo")]
    public class ProductDetail
    {
        [Key]
     public int ProductId {get;set;}
        /// <summary>
        /// 产品标题
        /// </summary>
     public string ProductName { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
     public int OederNumber{get;set;}
        /// <summary>
        /// 关键字
        /// </summary>
     public string ProductKeyWord { get; set; }
        /// <summary>
        /// 列表描述
        /// </summary>
     public string ListContent { get; set; }
        /// <summary>
        /// 产品概述
        /// </summary>
     public string AbstractContent { get; set; }
        /// <summary>
        /// 样品测试
        /// </summary>
     public string TemplateTest { get; set; } 
        /// <summary>
        /// 价格体系
        /// </summary>
     public string Money { get; set; }
        /// <summary>
        /// 价格体系查看权限
        /// </summary>
     public string ShowMoenyRole { get; set; }
        /// <summary>
        /// 产品缩略图
        /// </summary>
     public string ImageUrl { get; set; }
        /// <summary>
        /// 有效期开始时间
        /// </summary>
     public DateTime StartDate {get;set;}
        /// <summary>
        /// 有效期结束时间
        /// </summary>
     public DateTime EndDate {get;set;}
        /// <summary>
        /// 所属分类
        /// </summary>
     public string IncludetYype { get; set; }
        /// <summary>
        /// 产品详细介绍
        /// </summary>
     public string ProductContent { get; set; }
        /// <summary>
        /// 产品图集
        /// </summary>
     public string ImageListUrl { get; set; }
        /// <summary>
        /// 商品尺寸
        /// </summary>
     public string ProductSize { get; set; }
        /// <summary>
        /// 工业特性
        /// </summary>
     public string IndustryAttribute { get; set; }
        /// <summary>
        /// 接口描述
        /// </summary>
     public string InterFaceDescribe { get; set; }
        /// <summary>
        /// 重量
        /// </summary>
     public string Weight { get; set; }
        /// <summary>
        /// 尺寸
        /// </summary>
     public string Size { get; set; }
        /// <summary>
        /// 认证
        /// </summary>
     public string Accreditation { get; set; }
        /// <summary>
        /// 测试
        /// </summary>
     public string Test { get; set; }
        /// <summary>
        /// 产品规格书
        /// </summary>
     public string FileUrl { get; set; }
        /// <summary>
        /// 产品相关文章
        /// </summary>
     public string AboutDocument { get; set; }
        /// <summary>
        /// 关联案例
        /// </summary>
     public string CaseDocument { get; set; }
        /// <summary>
        /// 下载文件名
        /// </summary>
     public string DowlandFileName { get; set; }
        /// <summary>
        /// 下载地址
        /// </summary>
     public string DowlandFileUrl { get; set; }
        /// <summary>
        /// 下载文件名2
        /// </summary>
     public string DowlandFileNameTwo { get; set; }
     /// <summary>
     /// 下载地址2
     /// </summary>
     public string DowlandFileUrlTwo { get; set; }

        /// <summary>
        /// 公司ID
        /// </summary>
     public string CompanyId { get; set; } 
    }
}
