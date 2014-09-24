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
                new MenuModel{ID=1 ,MenuName="用户中心",Style="fa fa-user", OtherName="",ParentId=1,Url="../Users/Index"},
                new MenuModel{ID=2 ,MenuName="个人资料",Style="fa fa-edit",OtherName="",ParentId=1,Url="../Users/Index"},
                 new MenuModel{ID=3 ,MenuName="安全设置",Style="fa fa-lock",OtherName="",ParentId=1,Url="../Users/UserSafe"},
                  new MenuModel{ID=4 ,MenuName="收货地址",Style="fa fa-truck",OtherName="",ParentId=1,Url=""},
                   new MenuModel{ID=5 ,MenuName="威客交易",Style="fa fa-money",OtherName="",ParentId=5,Url=""},
                    new MenuModel{ID=6 ,MenuName="我的项目",Style="fa fa-cube",OtherName="",ParentId=5,Url=""},
                     new MenuModel{ID=7 ,MenuName="中标项目",Style="fa fa-cubes",OtherName="",ParentId=5,Url=""},
                      new MenuModel{ID=8 ,MenuName="创意管理",Style="fa fa-lightbulb-o",OtherName="",ParentId=8,Url=""},
                       new MenuModel{ID=9 ,MenuName="我的创意",Style="fa fa-lightbulb-o",OtherName="",ParentId=8,Url=""},
                        new MenuModel{ID=10 ,MenuName="参与讨论",Style="fa fa-comment-o",OtherName="",ParentId=8,Url=""},
                         new MenuModel{ID=11 ,MenuName="企业信息",Style="glyphicon glyphicon-info-sign",OtherName="",ParentId=11,Url=""},
                          new MenuModel{ID=12 ,MenuName="基础信息",Style="glyphicon glyphicon-info-sign",OtherName="",ParentId=11,Url=""},
                           new MenuModel{ID=13 ,MenuName="财务部",Style="fa fa-money",OtherName="",ParentId=13,Url=""},
                            new MenuModel{ID=14 ,MenuName="合同管理",Style="fa fa-file-text-o",OtherName="",ParentId=13,Url=""},
                             new MenuModel{ID=15 ,MenuName="资产管理",Style="fa fa-folder",OtherName="",ParentId=13,Url=""},
                              new MenuModel{ID=16 ,MenuName="业务部",Style="glyphicon glyphicon-earphone",OtherName="",ParentId=16,Url=""},
                               new MenuModel{ID=17 ,MenuName="发布产品",Style="fa fa-send",OtherName="",ParentId=16,Url=""},
                                new MenuModel{ID=18 ,MenuName="销售产品",Style="fa fa-star",OtherName="",ParentId=16,Url=""},
                                 new MenuModel{ID=19 ,MenuName="未上架产品",Style="fa fa-star-o",OtherName="",ParentId=16,Url=""},
                                  new MenuModel{ID=20 ,MenuName="商品分类",Style="fa fa-sort-amount-desc",OtherName="",ParentId=16,Url=""},
                                   new MenuModel{ID=21 ,MenuName="项目部",Style="fa fa-building",OtherName="我的项目",ParentId=21,Url=""},
                                    new MenuModel{ID=22 ,MenuName="发布中项目",Style="fa fa-mail-forward",OtherName="",ParentId=21,Url=""},
                                    new MenuModel{ID=23 ,MenuName="收到评估",Style="fa fa-mail-reply",OtherName="",ParentId=21,Url=""},
                                    new MenuModel{ID=24 ,MenuName="合作中项目",Style="fa fa-thumbs-o-up",OtherName="",ParentId=21,Url=""},
                                    new MenuModel{ID=25 ,MenuName="已立项项目",Style="fa fa-thumb-tack",OtherName="",ParentId=21,Url=""},
                                    new MenuModel{ID=26 ,MenuName="进度表",Style="fa fa-tasks",OtherName="",ParentId=21,Url=""}
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
