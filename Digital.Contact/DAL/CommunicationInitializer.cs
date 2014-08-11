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

            var Users = new List<UsersModel>
            {
                new UsersModel{Name="xjb",RegisterDate=DateTime.Now,IdeaModelList=Ideas1,Passwords="1"},
                new UsersModel{Name="yyq",RegisterDate=DateTime.Now,IdeaModelList=Ideas2,Passwords="1"},
                 new UsersModel{Name="1",RegisterDate=DateTime.Now,IdeaModelList=Ideas3,Passwords="1"},
            };

            Ideas1.ForEach(c => context.IdeaModels.Add(c));
            Ideas2.ForEach(c => context.IdeaModels.Add(c));
            Ideas3.ForEach(c => context.IdeaModels.Add(c));
            Users.ForEach(c => context.UsersModels.Add(c));
            context.SaveChanges();

        }
    }
}
