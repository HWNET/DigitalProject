using Digital.Contact.DAL;
using Digital.Contact.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digital.Contact.BLL
{
    public class TemplateService : IBaseService<TemplateModel>
    {
        const string CREATE_TABLE_START = @"create table {0} (";
        const string CREATE_TABLE_END = @");";

        public TemplateService()
        {

        }


        public void MakeTable(TemplateModel TemplateModel, List<TempColumnModel> columns)
        {
            var sb = new StringBuilder();
            sb.Append(string.Format(CREATE_TABLE_START, TemplateModel.TableName));
            foreach (var column in columns)
            {
                if (column.IsPrimaryKey)
                    sb.Append(string.Format("[{0}] {1} not null,", column.ColumnName, column.Type));
                else
                    sb.Append(string.Format("[{0}] {1},", column.ColumnName, column.Type));
            }
            sb.Append(CREATE_TABLE_END);
            var sql = sb.ToString();
            var ConnectionStrings = System.Configuration.ConfigurationManager.ConnectionStrings["CommunicationContext"].ToString();
            using (SqlConnection c = new SqlConnection(ConnectionStrings))
            {
                c.Open();
                using (SqlTransaction tx = c.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = c.CreateCommand())
                        {
                            cmd.Transaction = tx;
                            cmd.CommandType = System.Data.CommandType.Text;
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                            //cmd.CommandText = string.Format(@"CREATE NONCLUSTERED INDEX [SourceTime] ON [dbo].[{0}] ([SourceTime] DESC) ON [PRIMARY];", table_name);
                            //cmd.ExecuteNonQuery();
                            tx.Commit();
                        }
                    }
                    catch (Exception exc)
                    {
                        tx.Rollback();
                        throw exc;
                    }
                }
            }

        }

        public IList<TemplateModel> PageList(int pageIndex, int pageSize, out int TotalCount, out int PageCount)
        {
            using (var db = new CommunicationContext())
            {
                var Modelist = db.TemplateModels;
                TotalCount = Modelist.Count();
                PageCount = (int)Math.Round((double)TotalCount / pageSize);
                return Modelist.OrderBy(o => o.TemplateID).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public IList<TemplateModel> PageList<S>(int pageIndex, int pageSize, out int TotalCount, out int PageCount,
            Func<TemplateModel, bool> whereLambda, bool isAsc, Func<TemplateModel, S> orderByLambda)
        {
            using (var db = new CommunicationContext())
            {
                IEnumerable<TemplateModel> tempData = null;
                if (whereLambda != null)
                {
                    tempData = db.Set<TemplateModel>().Where<TemplateModel>(whereLambda);
                }
                else
                {
                    tempData = db.Set<TemplateModel>();
                }
                TotalCount = tempData.Count();
                PageCount = (int)Math.Round((double)TotalCount / pageSize);
                if (isAsc)
                {
                    tempData = tempData.OrderBy<TemplateModel, S>(orderByLambda).
                          Skip<TemplateModel>(pageSize * (pageIndex - 1)).
                          Take<TemplateModel>(pageSize).AsQueryable();
                }
                else
                {
                    tempData = tempData.OrderByDescending<TemplateModel, S>(orderByLambda).
                         Skip<TemplateModel>(pageSize * (pageIndex - 1)).
                         Take<TemplateModel>(pageSize).AsQueryable();
                }
                return tempData.AsQueryable().ToList();
            }
        }

        public TemplateModel Find(int? Id)
        {
            using (var db = new CommunicationContext())
            {
                try
                {
                    return db.TemplateModels.Find(Id);
                }
                catch (Exception ex)
                {
                    //log
                    return new TemplateModel();
                }
            }
        }


        public bool Edit(TemplateModel templateModels)
        {
            using (var db = new CommunicationContext())
            {
                if (templateModels != null && templateModels.TemplateID != 0)
                {
                    db.Entry(templateModels).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    db.TemplateModels.Add(templateModels);
                    db.SaveChanges();
                    return true;
                }
            }
        }

        public bool Delete(int Id)
        {
            using (var db = new CommunicationContext())
            {
                try
                {
                    TemplateModel templateModels = db.TemplateModels.Find(Id);
                    db.TemplateModels.Remove(templateModels);
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    //log
                    return false;
                }
            }
        }


    }
}
