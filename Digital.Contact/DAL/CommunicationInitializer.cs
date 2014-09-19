using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Digital.Contact.Models;

namespace Digital.Contact.DAL
{
    public class CommunicationInitializer : DropCreateDatabaseIfModelChanges<CommunicationContext>
    {
        protected override void Seed(CommunicationContext context)
        {
            var Ideas1 = new List<IdeaModel>{
                new IdeaModel{Name="idea1"},
                new IdeaModel{Name="idea2"}
            };

            var Ideas2 = new List<IdeaModel>{
                new IdeaModel{Name="idea3"},
                new IdeaModel{Name="idea4"}
            };
            var Ideas3 = new List<IdeaModel>{
                new IdeaModel{Name="idea5"},
                new IdeaModel{Name="idea6"}
            };
            //var Sills = new List<SkillsModel>
            //{
            //    new SkillsModel{Name=".net"},
            //    new SkillsModel{Name="java"},
            //    new SkillsModel{Name="php"},
            //    new SkillsModel{Name="vb"},
            //    new SkillsModel{Name="sql"},
            //    new SkillsModel{Name="mysql"},
            //    new SkillsModel{Name="oracle"},
            //};
            var GoodAtWhat = new List<GoodAtWhatModel>
            {
                new GoodAtWhatModel{SkillsModel= new SkillsModel{Name=".net"}},
                 new GoodAtWhatModel{SkillsModel= new SkillsModel{Name="java"}},
                  new GoodAtWhatModel{SkillsModel= new SkillsModel{Name="php"}},
                  new GoodAtWhatModel{SkillsModel= new SkillsModel{Name="vb"}},
                  new GoodAtWhatModel{SkillsModel= new SkillsModel{Name="sql"}},
                  new GoodAtWhatModel{SkillsModel= new SkillsModel{Name="mysql"}},
                  new GoodAtWhatModel{SkillsModel= new SkillsModel{Name="oracle"}},
            };
            var Users = new List<UsersModel>
            {
               
                 new UsersModel{Name="1",RegisterDate=DateTime.Now,Passwords="4eNgC+ewzLk=",LoginIP="",LastLoginTime=DateTime.Now,Status=1,IdeaModelList=Ideas2,UsersInfoModel=new UsersInfoModel{DisplayPicture="../DigitalStyle/images/photos/profile-1.png",QQ="49718751",Email="49718751@QQ.com",NickName="路西法",Sex=0,TrueName="龙俊",Zip="430062",BeGoodAtIntroduction="好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍",CityID=1,ProvinceID=1,Tel="15071410434",GoodAtWhatModels=GoodAtWhat}},
            };

            var Menus = new List<MenuModel> { 
                new MenuModel{ID=1 ,MenuName="用户中心",Style="fa fa-user", OtherName="",ParentId=1,Url="../Users/Index"},
                new MenuModel{ID=2 ,MenuName="个人资料",Style="fa fa-edit",OtherName="",ParentId=1,Url="../Users/Index"},
                 new MenuModel{ID=3 ,MenuName="安全设置",Style="fa fa-lock",OtherName="",ParentId=1,Url=""},
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
          
            Menus.ForEach(c => context.MenuModel.Add(c));
            Ideas2.ForEach(c => context.IdeaModels.Add(c));
            Users.ForEach(c => context.UsersModels.Add(c));
            context.SaveChanges();

        }
    }
}
