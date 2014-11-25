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
            var Users = new List<UsersModel>();
            //var UserModel1 = new UsersModel { Name = "1", RegisterDate = DateTime.Now, Passwords = "4eNgC+ewzLk=", LoginIP = "", LastLoginTime = DateTime.Now, Status = 1, IdeaModelList = Ideas2, UsersInfoModel = new UsersInfoModel { DisplayPicture = "../DigitalStyle/images/photos/profile-1.png", QQ = "49718751", Email = "49718751@QQ.com", NickName = "路西法", Sex = 0, TrueName = "龙俊", Zip = "430062", BeGoodAtIntroduction = "好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍", CityID = 1, ProvinceID = 1, Tel = "15071410434", GoodAtWhatModels = GoodAtWhat } };
            for (int i = 1; i < 2; i++)
            {
                Users.Add(new UsersModel { Name = i.ToString(), RegisterDate = DateTime.Now, Passwords = "4eNgC+ewzLk=", LoginIP = "", LastLoginTime = DateTime.Now, Status = 1, IdeaModelList = Ideas2, UsersInfoModel = new UsersInfoModel { DisplayPicture = "../DigitalStyle/images/photos/profile-1.png", QQ = "49718751", Email = "49718751@QQ.com", NickName = "路西法", Sex = 0, TrueName = "龙俊", Zip = "430062", BeGoodAtIntroduction = "好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍好多好多好多好介绍", CityID = 1, ProvinceID = 1, Tel = "15071410434", GoodAtWhatModels = GoodAtWhat } });
            }
            Ideas2.ForEach(c => context.IdeaModels.Add(c));
            Users.ForEach(c => context.UsersModels.Add(c));
            context.SaveChanges();

        }
    }
}
