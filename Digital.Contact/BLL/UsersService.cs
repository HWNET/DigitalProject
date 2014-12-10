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
    public class UsersService
    {


        public UsersService()
        {

        }



        #region Users
        public bool Login(string UserName, string Password)
        {
            using (var db = new CommunicationContext())
            {
                var User = db.UsersModels.Where(o => o.Name == UserName && o.Passwords == Password).FirstOrDefault();
                if (User != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        //public List<SkillsModel> GetSkillList()
        //{
        //    using (var db = new CommunicationContext())
        //    {
        //        return db.SkillsModel.ToList();
        //    }
        //}

        public bool UpdateGoodAt(GoodAtWhatModel GoodAtList)
        {
            using (var db = new CommunicationContext())
            {
                try
                {
                    //var OldGoodAtList= db.GoodAtWhatModel.Where(o => o.UsersInfoID == UsersInfoModel.UsersInfoID).ToList();
                    //var RemoveList= OldGoodAtList.Where(o => GoodAtList.Any(j => j.GoodAtWhatID != o.GoodAtWhatID)).ToList();
                    //foreach(var Remove in RemoveList)
                    //{
                    //    db.GoodAtWhatModel.Remove(Remove);
                    //}
                    //var AddList = GoodAtList.Where(o => OldGoodAtList.Any(j => j.GoodAtWhatID != o.GoodAtWhatID)).ToList();
                    //foreach (var Add in AddList)
                    //{
                    //    db.GoodAtWhatModel.Add(Add);
                    //}
                    //var RemoveList = GoodAtList.Where(o => o.UpdateStatus == 3).ToList();
                    //var AddList = GoodAtList.Where(o => o.UpdateStatus == 1).ToList();
                    if (GoodAtList.UpdateStatus == 3)
                    {
                        var RemoveEntity = db.GoodAtWhatModel.Where(O => O.GoodAtWhatID == GoodAtList.GoodAtWhatID).FirstOrDefault();
                        db.GoodAtWhatModel.Remove(RemoveEntity);
                    }
                    if (GoodAtList.UpdateStatus == 1)
                    {
                        GoodAtList.UpdateStatus = 0;
                        db.GoodAtWhatModel.Add(GoodAtList);
                    }

                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

        }

        //public bool UpdateGoodAt(string SkillStr, int UsersInfoID)
        //{
        //    using (var db = new CommunicationContext())
        //    {
        //        try
        //        {
        //            string[] SkillIds = SkillStr.Split(',');
        //            var SkillList= db.SkillsModel.ToList();
        //            foreach (var SkillModel in SkillList)
        //            {
        //               bool IsInclude=SkillIds.Any(e => e.ToString() == SkillModel.SkillId.ToString());
        //               var GoodModel = db.GoodAtWhatModel.Where(o => o.UsersInfoID == UsersInfoID && o.SkillId == SkillModel.SkillId ).FirstOrDefault();
        //               if (GoodModel != null && !IsInclude)
        //               {
        //                   db.GoodAtWhatModel.Remove(GoodModel);
        //               }
        //               else
        //               {

        //                   if (GoodModel == null && IsInclude)
        //                   {
        //                       db.GoodAtWhatModel.Add(new GoodAtWhatModel { SkillId = SkillModel.SkillId, UsersInfoID = UsersInfoID });
        //                   }
        //               }
        //            }
        //            db.SaveChanges();
        //            return true;
        //        }
        //        catch (Exception ex)
        //        {
        //            return false;
        //        }
        //    }
        //}

        public UsersModel FindByName(string UserName)
        {
            using (var db = new CommunicationContext())
            {
                try
                {
                    var Users = db.UsersModels.Include(o => o.UsersInfoModel).Include("UsersInfoModel.GoodAtWhatModels").Include("UsersInfoModel.GoodAtWhatModels.SkillsModel").Where(o => o.Name == UserName).SingleOrDefault();

                    if (Users != null)
                    {
                        return Users;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (DbEntityValidationException dbEx)
                {
                    return null;
                }
            }
        }


        public List<UsersModel> GetAllUserList()
        {
            using (var db = new CommunicationContext())
            {
                try
                {
                    //Include("UsersInfoModel.GoodAtWhatModels.SkillsModel")
                    var UserList = db.UsersModels.Include(o => o.UsersInfoModel).Include("UsersInfoModel.GoodAtWhatModels").ToList();
                    return UserList;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }



        public IList<UsersModel> PageList(int pageIndex, int pageSize, out int TotalCount, out int PageCount)
        {
            using (var db = new CommunicationContext())
            {
                var Modelist = db.UsersModels;
                TotalCount = Modelist.Count();
                PageCount = (int)Math.Round((double)TotalCount / pageSize);
                return Modelist.OrderBy(o => o.ID).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public IList<UsersModel> PageList<S>(int pageIndex, int pageSize, out int TotalCount, out int PageCount,
            Func<UsersModel, bool> whereLambda, bool isAsc, Func<UsersModel, S> orderByLambda)
        {
            using (var db = new CommunicationContext())
            {
                IEnumerable<UsersModel> tempData = null;
                if (whereLambda != null)
                {
                    tempData = db.Set<UsersModel>().Where<UsersModel>(whereLambda);
                }
                else
                {
                    tempData = db.Set<UsersModel>();
                }
                TotalCount = tempData.Count();
                PageCount = (int)Math.Round((double)TotalCount / pageSize);
                if (isAsc)
                {
                    tempData = tempData.OrderBy<UsersModel, S>(orderByLambda).
                          Skip<UsersModel>(pageSize * (pageIndex - 1)).
                          Take<UsersModel>(pageSize).AsQueryable();
                }
                else
                {
                    tempData = tempData.OrderByDescending<UsersModel, S>(orderByLambda).
                         Skip<UsersModel>(pageSize * (pageIndex - 1)).
                         Take<UsersModel>(pageSize).AsQueryable();
                }
                return tempData.AsQueryable().ToList();
            }
        }

        public UsersModel Find(int? Id)
        {
            using (var db = new CommunicationContext())
            {
                try
                {
                    return db.UsersModels.Find(Id);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    return new UsersModel();
                }
            }
        }


        public UsersModel Edit(UsersModel usersmodel)
        {
            using (var db = new CommunicationContext())
            {
                try
                {
                    if (usersmodel != null && usersmodel.ID != 0)
                    {
                        db.Entry(usersmodel).State = EntityState.Modified;
                        db.SaveChanges();
                        return usersmodel;
                    }
                    else
                    {
                        usersmodel = db.UsersModels.Add(usersmodel);
                        db.SaveChanges();
                        return usersmodel;
                    }
                }
                catch (DbEntityValidationException dbEx)
                {
                    Logger.Error(dbEx.InnerException.ToString());
                    return null;
                }
            }
        }

        public bool Delete(int Id)
        {
            using (var db = new CommunicationContext())
            {
                try
                {
                    UsersModel usersmodel = db.UsersModels.Find(Id);
                    db.UsersModels.Remove(usersmodel);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    return false;
                }
            }
        }
        #endregion

        #region WaterUser
        public WaterMarkModel WaterEdit(WaterMarkModel WaterMark)
        {
            using (var db = new CommunicationContext())
            {
                try
                {
                    if (WaterMark != null && WaterMark.UserId != 0)
                    {
                        var Temp = db.WaterMarkModels.Where(o => o.UserId == WaterMark.UserId).FirstOrDefault();
                        Temp.WaterPostion = WaterMark.WaterPostion;
                        Temp.IsWebsite = WaterMark.IsWebsite;
                        Temp.IsCompanyName = WaterMark.IsCompanyName;
                        db.Entry(WaterMark).State = EntityState.Modified;
                        db.SaveChanges();
                        return WaterMark;
                    }
                    else
                    {
                        WaterMark = db.WaterMarkModels.Add(WaterMark);
                        db.SaveChanges();
                        return WaterMark;
                    }
                }
                catch (DbEntityValidationException dbEx)
                {
                    Logger.Error(dbEx.InnerException.ToString());
                    return null;
                }
            }
        }

        public WaterMarkModel WaterFind(int Userid)
        {
            using (var db = new CommunicationContext())
            {
                try
                {
                    return db.WaterMarkModels.Where(o => o.UserId == Userid).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    return new WaterMarkModel();
                }
            }
        }
        #endregion


    }
}
