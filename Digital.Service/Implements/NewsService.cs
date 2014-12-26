using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Digital.Contact.Models;
using Digital.Service.Interfaces;
using Digital.Contact.BLL;
using Digital.Common.Logging;

namespace Digital.Service.Implements
{
    public partial class Service : INewsService
    {
        #region NewsInsert
        public NewsModel NewsInsert(NewsModel Model)
        {
            try
            {
                //Model.UpdateStatus = 1;
                NewsService NewsService = new NewsService();
                //Insert DB
                var Result = NewsService.NewsInsert(Model);
                //Insert Cache
                //GenericList.CacheModelObj.CompanyModellist.Add(Model);
                return Result;
            }
            catch (Exception ex)
            {
                logger.WriteInfo("News", MessageLevel.Level2, ex.ToString());
                return null;
            }

        }
        #endregion

        #region NewsUpdate
        public NewsModel NewsUpdate(NewsModel Model)
        {
            try
            {
                var NewsModel = NewsQueryById(Model.NewsID);

                if (NewsModel != null)
                {
                    #region Instance Model

                    #endregion
                    #region UI Models

                    #endregion

                    //0 表示不更新 1表示新增加 2 表示更新 3表示删除的 
                    //NewsModel.UpdateStatus = 2;

                    //directly update to DB
                    NewsService NewsService = new NewsService();
                    NewsService.NewsUpdate(Model);
                    return Model;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                logger.WriteInfo("News", MessageLevel.Level2, ex.ToString());
                return null;
            }

        }
        #endregion

        #region NewsQueryById
        public NewsModel NewsQueryById(int NewsId)
        {
            try
            {
                //get model from DB
                NewsService NewsService = new NewsService();
                var NewsModel = NewsService.NewsQueryById(NewsId);
                return NewsModel;
            }
            catch (Exception ex)
            {
                logger.WriteInfo("News", MessageLevel.Level2, ex.ToString());
                return null;
            }

        }
        #endregion

        #region NewsQueryByTitle
        public NewsModel NewsQueryByTitle(string NewsTitle)
        {
            try
            {
                //get model from DB
                NewsService NewsService = new NewsService();
                var NewsModel = NewsService.NewsQueryByTitle(NewsTitle);
                return NewsModel;
            }
            catch (Exception ex)
            {
                logger.WriteInfo("News", MessageLevel.Level2, ex.ToString());
                return null;
            }

        }
        #endregion

        #region NewsDeleteById
        public bool NewsDeleteById(int NewsId)
        {
            try
            {
                //get model from DB
                NewsService NewsService = new NewsService();
                var Result = NewsService.NewsDeleteById(NewsId);
                return Result;
            }
            catch (Exception ex)
            {
                logger.WriteInfo("News", MessageLevel.Level2, ex.ToString());
                return false;
            }

        }
        #endregion

        #region NewsDeleteByCategory
        public bool NewsDeleteByCategory(int NewsCategoryId)
        {
            try
            {
                //get model from DB
                NewsService NewsService = new NewsService();
                var Result = NewsService.NewsDeleteByCategory(NewsCategoryId);
                return Result;
            }
            catch (Exception ex)
            {
                logger.WriteInfo("News", MessageLevel.Level2, ex.ToString());
                return false;
            }

        }
        #endregion

        #region NewsDeleteByCompany
        public bool NewsDeleteByCompany(int CompanyId)
        {
            try
            {
                NewsService NewsService = new NewsService();
                var NewsList = NewsService.NewsQueryListByCompany(CompanyId);
                //0 表示不更新 1表示新增加 2 表示更新 3表示删除的 
                //CategoryModel.UpdateStatus = 3;

                if (NewsList != null)
                {
                    var Result = NewsService.NewsDeleteByCompany(CompanyId);
                    return Result;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                logger.WriteInfo("News", MessageLevel.Level2, ex.ToString());
                return false;
            }

        }
        #endregion

        #region NewsQueryList
        public List<NewsModel> NewsQueryList()
        {
            try
            {
                NewsService NewsService = new NewsService();
                var NewsList = NewsService.NewsQueryList();
                return NewsList;
            }
            catch (Exception ex)
            {
                logger.WriteInfo("News", MessageLevel.Level2, ex.ToString());
                return null;
            }

        }
        #endregion

        #region NewsQueryListByCategory
        public List<NewsModel> NewsQueryListByCategory(int NewsCategoryId)
        {
            try
            {
                NewsService NewsService = new NewsService();
                var NewsList = NewsService.NewsQueryListByCategory(NewsCategoryId);
                return NewsList;
            }
            catch (Exception ex)
            {
                logger.WriteInfo("News", MessageLevel.Level2, ex.ToString());
                return null;
            }

        }
        #endregion

        #region NewsQueryListByCompany
        public List<NewsModel> NewsQueryListByCompany(int CompanyId)
        {
            try
            {
                NewsService NewsService = new NewsService();
                var NewsList = NewsService.NewsQueryListByCompany(CompanyId);
                return NewsList;
            }
            catch (Exception ex)
            {
                logger.WriteInfo("News", MessageLevel.Level2, ex.ToString());
                return null;
            }

        }
        #endregion
    }
}
