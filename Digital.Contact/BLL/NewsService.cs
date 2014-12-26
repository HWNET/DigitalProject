using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Digital.Common.Logging;
using Digital.Contact.DAL;
using Digital.Contact.Models;
using System.Data.Entity;

namespace Digital.Contact.BLL
{
    public class NewsService
    {
        #region IsModelExist
        private bool IsModelExist(NewsModel Model)
        {
            using (var db = new CommunicationContext())
            {
                var item = db.NewsModels.Find(Model.NewsID);
                if (item == null)
                {
                    return false;
                }
                return true;
            }
        }
        #endregion

        #region NewsInsert
        public NewsModel NewsInsert(NewsModel Model)
        {
            using (var db = new CommunicationContext())
            {

                    if (!IsModelExist(Model))
                    {
                        Model = db.NewsModels.Add(Model);
                        db.SaveChanges();
                        return Model;
                    }
                    else
                    {
                        return null;
                    }

            }
        }
        #endregion

        #region NewsUpdate
        public NewsModel NewsUpdate(NewsModel Model)
        {
            using (var db = new CommunicationContext())
            {

                    if (IsModelExist(Model))
                    {
                        db.Entry(Model).State = EntityState.Modified;
                        db.SaveChanges();
                        return Model;
                    }
                    else
                    {
                        return new NewsModel();
                    }

            }
        }
        #endregion

        #region NewsQueryById
        public NewsModel NewsQueryById(int NewsId)
        {
            using (var db = new CommunicationContext())
            {

                    return db.NewsModels.Find(NewsId);

            }
        }
        #endregion

        #region NewsQueryByTitle
        public NewsModel NewsQueryByTitle(string NewsTitle)
        {
            using (var db = new CommunicationContext())
            {

                    var model = db.NewsModels.Where(o => o.NewsTitle == NewsTitle).SingleOrDefault();
                    return model;

            }
        }
        #endregion

        #region NewsDeleteById
        public bool NewsDeleteById(int NewsId)
        {
            using (var db = new CommunicationContext())
            {

                    var model = db.NewsModels.Find(NewsId);
                    if (model != null)
                    {
                        db.NewsModels.Remove(model);
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }

            }
        }
        #endregion

        #region NewsDeleteByCategory
        public bool NewsDeleteByCategory(int NewsCategoryId)
        {
            using (var db = new CommunicationContext())
            {

                    var model = db.NewsModels.Where(o => o.NewsCategoryID == NewsCategoryId);
                    if (model != null)
                    {
                        db.NewsModels.RemoveRange(model);
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }

            }
        }
        #endregion

        #region NewsDeleteByCompany
        public bool NewsDeleteByCompany(int CompanyId)
        {
            using (var db = new CommunicationContext())
            {

                    var CategoryList = db.NewsCategoryModels.Where(o => o.CompanyID == CompanyId).ToList();
                    List<NewsModel> NewsList = new List<NewsModel>();
                    CategoryList.ForEach(c => NewsList.Add(db.NewsModels.Where(o => o.NewsCategoryID == c.NewsCategoryID).SingleOrDefault()));
                    if (NewsList != null)
                    {
                        db.NewsModels.RemoveRange((IEnumerable<NewsModel>)NewsList);
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
            }
        }
        #endregion

        #region NewsQueryList
        public List<NewsModel> NewsQueryList()
        {
            using (var db = new CommunicationContext())
            {

                    return db.NewsModels.ToList();

            }
        }
        #endregion

        #region NewsQueryListByCategory
        public List<NewsModel> NewsQueryListByCategory(int NewsCategoryId)
        {
            using (var db = new CommunicationContext())
            {

                    var NewsList = db.NewsModels.Where(o => o.NewsCategoryID == NewsCategoryId).ToList();
                    return NewsList;

            }
        }
        #endregion

        #region NewsQueryListByCompany
        public List<NewsModel> NewsQueryListByCompany(int CompanyId)
        {
            using (var db = new CommunicationContext())
            {

                    var CategoryList = db.NewsCategoryModels.Where(o => o.CompanyID == CompanyId).ToList();
                    List<NewsModel> NewsList = new List<NewsModel>();
                    CategoryList.ForEach(c => NewsList.AddRange(db.NewsModels.Where(o => o.NewsCategoryID == c.NewsCategoryID)));

                    //foreach (var mode in NewsList)
                    //{
                    //    var category = CategoryList.Where(o => o.NewsCategoryID == mode.NewsCategoryID).SingleOrDefault();
                    //    mode.NewsCategoryModel = category;
                    //}

                    return NewsList;

            }
        }
        #endregion
    }
}
