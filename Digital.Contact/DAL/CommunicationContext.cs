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
    //[DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class CommunicationContext:DbContext
    {
        public CommunicationContext() : base("CommunicationContext") { this.Configuration.ProxyCreationEnabled = false; }

        public DbSet<Digital.Contact.Models.IdeaModel> IdeaModels { get; set; }

        public DbSet<Digital.Contact.Models.UsersModel> UsersModels { get; set; }

        public DbSet<Digital.Contact.Models.TemplateModel> TemplateModels { get; set; }

        public DbSet<Digital.Contact.Models.TempColumnModel> TempColumnModels { get; set; }

        public DbSet<Digital.Contact.Models.RelationTableModels> RelationTableModels { get; set; }

        public DbSet<Digital.Contact.Models.UsersInfoModel> UsersInfoModel { get; set; }

        public DbSet<Digital.Contact.Models.GoodAtWhatModel> GoodAtWhatModel { get; set; }

        public DbSet<Digital.Contact.Models.CompanyModel> CompanyModels { get; set; }
        public DbSet<Digital.Contact.Models.CasesCategoryModel> CasesCategoryModels { get; set; }
        public DbSet<Digital.Contact.Models.CasesModel> CasesModels { get; set; }
        public DbSet<Digital.Contact.Models.NewsCategoryModel> NewsCategoryModels { get; set; }
        public DbSet<Digital.Contact.Models.NewsModel> NewsModels { get; set; }
        public DbSet<Digital.Contact.Models.PatentModel> PatentModels { get; set; }
        public DbSet<Digital.Contact.Models.SinglePageModel> SinglePageModels { get; set; }

        public DbSet<Digital.Contact.Models.CertificateModel> CertificateModels { get; set; }
        public DbSet<Digital.Contact.Models.FolderModel> FolderModels { get; set; }

        public DbSet<Digital.Contact.Models.WaterMarkModel> WaterMarkModels { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<DbContext>());  
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}
