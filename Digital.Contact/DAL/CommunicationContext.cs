using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using MySql.Data.Entity;

namespace Digital.Contact.DAL
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class CommunicationContext:DbContext
    {
        public CommunicationContext():base("CommunicationContext"){}

        public DbSet<Digital.Contact.Models.IdeaModel> IdeaModels { get; set; }

        public DbSet<Digital.Contact.Models.UsersModel> UsersModels { get; set; }

        public DbSet<Digital.Contact.Models.TemplateModel> TemplateModels { get; set; }

        public DbSet<Digital.Contact.Models.TempColumnModel> TempColumnModels { get; set; }

        public DbSet<Digital.Contact.Models.RelationTableModels> RelationTableModels { get; set; }

        public DbSet<Digital.Contact.Models.SkillsModel> SkillsModel { get; set; }

        public DbSet<Digital.Contact.Models.UsersInfoModel> UsersInfoModel { get; set; }

        public DbSet<Digital.Contact.Models.GoodAtWhatModel> GoodAtWhatModel { get; set; }

        public DbSet<Digital.Contact.Models.CompanyModel> CompanyModel { get; set; }

        public DbSet<Digital.Contact.Models.MenuModel> MenuModel { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<DbContext>());  
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}
