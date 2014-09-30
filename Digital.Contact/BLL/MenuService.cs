using Digital.Common.Logging;
using Digital.Contact.DAL;
using Digital.Contact.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Digital.Contact.BLL
{
    public class MenuService
    {
        

        public List<MenuModel> MenuList()
        {
            //using (var db = new CommunicationContext())
            //{
            //    return db.MenuModel.ToList();
            //}
            #region
            var Menus = new List<MenuModel> { 
                //personal user menu ,ParentId value begin from 1 to 99
                new MenuModel{ID=1 ,MenuName="用户中心",Style="fa fa-user", OtherName="",ParentId=1,Url="../Users/Index"},
                new MenuModel{ID=2 ,MenuName="个人资料",Style="fa fa-edit",OtherName="",ParentId=1,Url="../Users/Index"},
                new MenuModel{ID=3 ,MenuName="安全设置",Style="fa fa-lock",OtherName="",ParentId=1,Url="../Users/UserSafe"},
                new MenuModel{ID=4 ,MenuName="隐私设置",Style="fa fa-lock",OtherName="",ParentId=1,Url="../Users/UserSafe"},
                new MenuModel{ID=5 ,MenuName="收货地址",Style="fa fa-truck",OtherName="",ParentId=1,Url=""},
                new MenuModel{ID=6 ,MenuName="提醒设置",Style="fa fa-truck",OtherName="",ParentId=1,Url=""},

                new MenuModel{ID=10 ,MenuName="我是威客",Style="fa fa-slideshare",OtherName="",ParentId=10,Url=""},
                new MenuModel{ID=11 ,MenuName="我的项目",Style="fa fa-cube",OtherName="",ParentId=10,Url=""},
                new MenuModel{ID=12 ,MenuName="中标项目",Style="fa fa-cubes",OtherName="",ParentId=10,Url=""},
                new MenuModel{ID=13 ,MenuName="我的文件柜",Style="fa fa-cubes",OtherName="",ParentId=10,Url=""},

                new MenuModel{ID=20 ,MenuName="我是雇主",Style="fa fa-tasks",OtherName="",ParentId=20,Url=""},
                new MenuModel{ID=21 ,MenuName="发布需求",Style="fa fa-cube",OtherName="",ParentId=20,Url=""},
                new MenuModel{ID=22 ,MenuName="已发布需求",Style="fa fa-cubes",OtherName="",ParentId=20,Url=""},

                new MenuModel{ID=30 ,MenuName="创意管理",Style="fa fa-lightbulb-o",OtherName="",ParentId=30,Url=""},
                new MenuModel{ID=31 ,MenuName="我发布的种子创意",Style="fa fa-lightbulb-o",OtherName="",ParentId=30,Url=""},
                new MenuModel{ID=32 ,MenuName="我参与的创意",Style="fa fa-comment-o",OtherName="",ParentId=30,Url=""},
                new MenuModel{ID=33 ,MenuName="封存的创意",Style="fa fa-comment-o",OtherName="",ParentId=30,Url=""},

                new MenuModel{ID=40 ,MenuName="就业机会",Style="fa  fa-briefcase",OtherName="",ParentId=40,Url=""},
                new MenuModel{ID=41 ,MenuName="HR来信",Style="fa fa-lightbulb-o",OtherName="",ParentId=40,Url=""},
                new MenuModel{ID=42 ,MenuName="寻找职位",Style="fa fa-comment-o",OtherName="",ParentId=40,Url=""},

                new MenuModel{ID=50 ,MenuName="资产管理",Style="fa fa-credit-card",OtherName="",ParentId=50,Url=""},
                new MenuModel{ID=51 ,MenuName="收支明细",Style="fa fa-lightbulb-o",OtherName="",ParentId=50,Url=""},
                new MenuModel{ID=52 ,MenuName="充值",Style="fa fa-comment-o",OtherName="",ParentId=50,Url=""},
                new MenuModel{ID=53 ,MenuName="提现",Style="fa fa-comment-o",OtherName="",ParentId=50,Url=""},

                new MenuModel{ID=60 ,MenuName="增值应用",Style="fa fa-cloud-download",OtherName="",ParentId=60,Url=""},
                new MenuModel{ID=61 ,MenuName="我的创意",Style="fa fa-lightbulb-o",OtherName="",ParentId=60,Url=""},
                new MenuModel{ID=62 ,MenuName="参与讨论",Style="fa fa-comment-o",OtherName="",ParentId=60,Url=""},


                //company user menu  ,ParentId value begin from 100
                new MenuModel{ID=100 ,MenuName="账户中心",Style="glyphicon glyphicon-info-sign",OtherName="",ParentId=100,Url=""},
                new MenuModel{ID=101 ,MenuName="账户信息",Style="glyphicon glyphicon-info-sign",OtherName="",ParentId=100,Url=""},
                new MenuModel{ID=102 ,MenuName="子账户管理",Style="glyphicon glyphicon-info-sign",OtherName="",ParentId=100,Url=""},
                new MenuModel{ID=103 ,MenuName="账户安全",Style="glyphicon glyphicon-info-sign",OtherName="",ParentId=100,Url=""},
                new MenuModel{ID=104 ,MenuName="交易资料",Style="glyphicon glyphicon-info-sign",OtherName="",ParentId=100,Url=""},

                new MenuModel{ID=150 ,MenuName="企业信息中心",Style="glyphicon  glyphicon-user",OtherName="",ParentId=150,Url=""},
                new MenuModel{ID=151 ,MenuName="基础信息",Style="glyphicon glyphicon-info-sign",OtherName="",ParentId=150,Url=""},
                new MenuModel{ID=152 ,MenuName="企业信誉",Style="glyphicon glyphicon-info-sign",OtherName="",ParentId=150,Url=""},
                new MenuModel{ID=153 ,MenuName="资质介绍",Style="glyphicon glyphicon-info-sign",OtherName="",ParentId=150,Url=""},
                new MenuModel{ID=154 ,MenuName="企业文件柜",Style="glyphicon glyphicon-info-sign",OtherName="",ParentId=150,Url=""},
                new MenuModel{ID=155 ,MenuName="企业播报",Style="glyphicon glyphicon-info-sign",OtherName="",ParentId=150,Url=""},

                new MenuModel{ID=200 ,MenuName="财务部",Style="fa fa-money",OtherName="",ParentId=200,Url=""},
                new MenuModel{ID=201 ,MenuName="账户管理",Style="fa fa-folder",OtherName="",ParentId=200,Url=""},
                new MenuModel{ID=202 ,MenuName="自助支付",Style="fa fa-file-text-o",OtherName="",ParentId=200,Url=""},
                new MenuModel{ID=203 ,MenuName="合同管理",Style="fa fa-folder",OtherName="",ParentId=200,Url=""},
                new MenuModel{ID=204 ,MenuName="项目交易明细",Style="fa fa-folder",OtherName="",ParentId=200,Url=""},
                new MenuModel{ID=205 ,MenuName="发票管理",Style="fa fa-folder",OtherName="",ParentId=200,Url=""},
                new MenuModel{ID=206 ,MenuName="生成报税凭据",Style="fa fa-folder",OtherName="",ParentId=200,Url=""},
                
                new MenuModel{ID=250 ,MenuName="项目部",Style="fa fa-building",OtherName="我的项目",ParentId=250,Url=""},
                new MenuModel{ID=251 ,MenuName="发布项目",Style="fa fa-mail-forward",OtherName="",ParentId=250,Url=""},
                new MenuModel{ID=251 ,MenuName="已发布项目",Style="fa fa-mail-forward",OtherName="",ParentId=250,Url=""},
                new MenuModel{ID=252 ,MenuName="收到评估",Style="fa fa-mail-reply",OtherName="",ParentId=250,Url=""},
                new MenuModel{ID=253 ,MenuName="合作中的项目",Style="fa fa-thumbs-o-up",OtherName="",ParentId=250,Url=""},
                new MenuModel{ID=254 ,MenuName="供应商选型",Style="fa fa-tasks",OtherName="",ParentId=250,Url=""},
                new MenuModel{ID=255 ,MenuName="众筹项目管理",Style="fa fa-thumb-tack",OtherName="",ParentId=250,Url=""},

                new MenuModel{ID=300 ,MenuName="业务部",Style="glyphicon glyphicon-phone-alt",OtherName="",ParentId=300,Url=""},
                new MenuModel{ID=301 ,MenuName="寻找定制需求",Style="fa fa-send",OtherName="",ParentId=300,Url=""},
                new MenuModel{ID=302 ,MenuName="发送项目评估书",Style="fa fa-sort-amount-desc",OtherName="",ParentId=300,Url=""},
                new MenuModel{ID=303 ,MenuName="发送报价单",Style="fa fa-sort-amount-desc",OtherName="",ParentId=300,Url=""},
                new MenuModel{ID=304 ,MenuName="询盘管理",Style="fa fa-send",OtherName="",ParentId=300,Url=""},
                new MenuModel{ID=305 ,MenuName="发布产品",Style="fa fa-send",OtherName="",ParentId=300,Url=""},
                new MenuModel{ID=307 ,MenuName="产品分类管理",Style="fa fa-star",OtherName="",ParentId=300,Url=""},
                new MenuModel{ID=308 ,MenuName="已发布产品管理",Style="fa fa-star-o",OtherName="",ParentId=300,Url=""},
                new MenuModel{ID=309 ,MenuName="意向客户",Style="fa fa-sort-amount-desc",OtherName="",ParentId=300,Url=""},
                new MenuModel{ID=310 ,MenuName="渠道管理",Style="fa fa-sort-amount-desc",OtherName="",ParentId=300,Url=""},
                new MenuModel{ID=311 ,MenuName="在线客服",Style="fa fa-sort-amount-desc",OtherName="",ParentId=300,Url=""},

                new MenuModel{ID=350 ,MenuName="市场部",Style="glyphicon fa-bullhorn",OtherName="",ParentId=350,Url=""},
                new MenuModel{ID=351 ,MenuName="企业店铺管理",Style="fa fa-send",OtherName="",ParentId=350,Url=""},
                new MenuModel{ID=352 ,MenuName="投放广告",Style="fa fa-star",OtherName="",ParentId=350,Url=""},
                new MenuModel{ID=353 ,MenuName="广告管理",Style="fa fa-star-o",OtherName="",ParentId=350,Url=""},
                new MenuModel{ID=354 ,MenuName="排名服务",Style="fa fa-sort-amount-desc",OtherName="",ParentId=350,Url=""},
                new MenuModel{ID=355 ,MenuName="增值应用",Style="fa fa-sort-amount-desc",OtherName="",ParentId=350,Url=""},

                new MenuModel{ID=400 ,MenuName="采购部",Style="glyphicon fa-shopping-cart",OtherName="",ParentId=400,Url=""},
                new MenuModel{ID=401 ,MenuName="发送采购订单",Style="fa fa-send",OtherName="",ParentId=400,Url=""},
                new MenuModel{ID=402 ,MenuName="挑选供应商",Style="fa fa-star",OtherName="",ParentId=400,Url=""},
                new MenuModel{ID=403 ,MenuName="供应商列表",Style="fa fa-star",OtherName="",ParentId=400,Url=""},
                new MenuModel{ID=404 ,MenuName="供应商分类",Style="fa fa-star",OtherName="",ParentId=400,Url=""},
                new MenuModel{ID=405 ,MenuName="已收到的报价单",Style="fa fa-star-o",OtherName="",ParentId=400,Url=""},
                new MenuModel{ID=406 ,MenuName="需求管理",Style="fa fa-sort-amount-desc",OtherName="",ParentId=400,Url=""},

                new MenuModel{ID=450 ,MenuName="人事部",Style="glyphicon fa-users",OtherName="",ParentId=450,Url=""},
                new MenuModel{ID=451 ,MenuName="发布招贤榜",Style="fa fa-send",OtherName="",ParentId=450,Url=""},
                new MenuModel{ID=452 ,MenuName="已收到的简历",Style="fa fa-star",OtherName="",ParentId=450,Url=""},
                new MenuModel{ID=453 ,MenuName="委托寻找高端人才",Style="fa fa-star-o",OtherName="",ParentId=450,Url=""},
                new MenuModel{ID=454 ,MenuName="聘请临时专家",Style="fa fa-sort-amount-desc",OtherName="",ParentId=450,Url=""},
                new MenuModel{ID=455 ,MenuName="聘请临时项目团队",Style="fa fa-sort-amount-desc",OtherName="",ParentId=450,Url=""},
                
                new MenuModel{ID=500 ,MenuName="隐私设置",Style="glyphicon fa-unlock-alt",OtherName="",ParentId=500,Url=""},
                new MenuModel{ID=501 ,MenuName="发布产品",Style="fa fa-send",OtherName="",ParentId=500,Url=""},
                new MenuModel{ID=502 ,MenuName="销售产品",Style="fa fa-star",OtherName="",ParentId=500,Url=""},
                new MenuModel{ID=503 ,MenuName="未上架产品",Style="fa fa-star-o",OtherName="",ParentId=500,Url=""},
                new MenuModel{ID=504 ,MenuName="商品分类",Style="fa fa-sort-amount-desc",OtherName="",ParentId=500,Url=""},

                new MenuModel{ID=550 ,MenuName="消息及通知",Style="glyphicon fa-envelope",OtherName="",ParentId=550,Url=""},
                new MenuModel{ID=551 ,MenuName="项目浏览权限",Style="fa fa-send",OtherName="",ParentId=550,Url=""},
                new MenuModel{ID=552 ,MenuName="企业信息查看权限",Style="fa fa-star",OtherName="",ParentId=550,Url=""},
                new MenuModel{ID=553 ,MenuName="其它",Style="fa fa-star-o",OtherName="",ParentId=550,Url=""},

                new MenuModel{ID=600 ,MenuName="举报及维权",Style="glyphicon fa-bell",OtherName="",ParentId=600,Url=""},
                new MenuModel{ID=601 ,MenuName="我收到的举报",Style="fa fa-send",OtherName="",ParentId=600,Url=""},
                new MenuModel{ID=602 ,MenuName="我发起的举报",Style="fa fa-star",OtherName="",ParentId=600,Url=""},

                new MenuModel{ID=650 ,MenuName="帮助中心",Style="glyphicon fa-question-circle",OtherName="",ParentId=650,Url=""},
                new MenuModel{ID=651 ,MenuName="平台介绍",Style="fa fa-send",OtherName="",ParentId=650,Url=""},
                new MenuModel{ID=652 ,MenuName="常见问题",Style="fa fa-star",OtherName="",ParentId=650,Url=""}


            };
            #endregion
            return Menus;
        }

        public MenuModel GetMenuModel(int MenuId)
        {

            return MenuList().Where(o => o.ID == MenuId).FirstOrDefault();
            
        }
    }
}
