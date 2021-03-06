﻿using System;
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
    public class CompanyService
    {
        #region IsModelExist
        private bool IsModelExist(CompanyModel Model)
        {
            using (var db = new CommunicationContext())
            {
                var item = db.CompanyModels.Where(o => o.CompanyID == Model.CompanyID).FirstOrDefault();
                if (item == null)
                {
                    return false;
                }
                return true;
            }
        }
        #endregion

        #region CompanyInsert
        public CompanyModel CompanyInsert(CompanyModel Model)
        {
            using (var db = new CommunicationContext())
            {

                    if (!IsModelExist(Model))
                    {
                        Model = db.CompanyModels.Add(Model);
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

        #region CompanyUpdate
        public CompanyModel CompanyUpdate(CompanyModel Model)
        {
            using(var db=new CommunicationContext())
            {

                    if (IsModelExist(Model))
                    {
                        db.Entry(Model).State = EntityState.Modified;
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

        #region CompanyQueryById
        public CompanyModel CompanyQueryById(int CompanyId)
        {
            using (var db = new CommunicationContext())
            {

                    return db.CompanyModels.Find(CompanyId);

            }
        }
        #endregion

        #region CompanyQueryByName
        public CompanyModel CompanyQueryByName(string CompanyName)
        {
            using(var db=new CommunicationContext())
            {

                    var model = db.CompanyModels.Where(o => o.CompanyName == CompanyName).SingleOrDefault();
                    return model;

            }
        }
        #endregion

        #region CompanyDeleteById
        public bool CompanyDeleteById(int CompanyId)
        {
            using(var db=new CommunicationContext())
            {

                    var model = db.CompanyModels.Find(CompanyId);
                    if (model!=null)
                    {
                        db.CompanyModels.Remove(model);
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

        #region CompanyDisposeByNo
        public bool CompanyDisposeByNo(string CompanyRegisteredNO)
        {
            using (var db = new CommunicationContext())
            {

                    var model = db.CompanyModels.Where(o=>o.CompanyRegisteredNO==CompanyRegisteredNO).SingleOrDefault();
                    if (model != null)
                    {
                        db.CompanyModels.Remove(model);
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

        #region CompanyQueryList
        public List<CompanyModel> CompanyQueryList()
        {
            using (var db = new CommunicationContext())
            {

                    return db.CompanyModels.ToList();

            }
        }
        #endregion
    }
}
