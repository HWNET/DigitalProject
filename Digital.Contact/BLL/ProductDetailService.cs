using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Digital.Common.Logging;
using Digital.Contact.DAL;
using Digital.Contact.Models;
using System.Data.Entity;
/*
 * Created By Rob Xu
 * Creartd Date: 2015.01.18
 * Updated Date:
 * Ver:1.0.0.0
 */
namespace Digital.Contact.BLL
{
    public class ProductDetailService
    {
        #region IsModelExist
        private bool IsModelExist(ProductDetailModel Model)
        {
            using (var db = new CommunicationContext())
            {
                var item = db.ProductDetail.Find(Model.ProductId);
                if (item == null)
                {
                    return false;
                }
                return true;
            }
        }
        #endregion

        #region ProductDetailInsert
        public ProductDetailModel ProductDetailInsert(ProductDetailModel Model)
        {
            using (var db = new CommunicationContext())
            {

                if (!IsModelExist(Model))
                {
                    Model = db.ProductDetail.Add(Model);
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

        #region ProductDetailUpdate
        public ProductDetailModel ProductDetailUpdate(ProductDetailModel Model)
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
                    return new ProductDetailModel();
                }

            }
        }
        #endregion

        #region ProductDetailQueryById
        public ProductDetailModel ProductDetailQueryById(int ProductDetailId)
        {
            using (var db = new CommunicationContext())
            {

                return db.ProductDetail.Find(ProductDetailId);

            }
        }
        #endregion

        #region ProductDetailQueryByTitle
        public ProductDetailModel ProductDetailQueryByTitle(string ProductName)
        {
            using (var db = new CommunicationContext())
            {

                var model = db.ProductDetail.Where(o => o.ProductName == ProductName).SingleOrDefault();
                return model;

            }
        }
        #endregion

        #region ProductDetailDeleteById
        public bool ProductDetailDeleteById(int ProductId)
        {
            using (var db = new CommunicationContext())
            {

                var model = db.ProductDetail.Find(ProductId);
                if (model != null)
                {
                    db.ProductDetail.Remove(model);
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


        #region ProductDetailDeleteByCompany
        public bool ProductDetailDeleteByCompany(int CompanyId)
        {
            using (var db = new CommunicationContext())
            {
                var ProductDetailList = db.ProductDetail.Where(o => o.CompanyId == CompanyId.ToString());
                if (ProductDetailList != null)
                {
                    db.ProductDetail.RemoveRange(ProductDetailList);
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

        #region ProductDetailQueryList
        public List<ProductDetailModel> ProductDetailQueryList()
        {
            using (var db = new CommunicationContext())
            {

                return db.ProductDetail.ToList();

            }
        }
        #endregion

        #region ProductDetailQueryListByCompany
        public List<ProductDetailModel> ProductDetailQueryListByCompany(int CompanyId)
        {
            using (var db = new CommunicationContext())
            {
                return db.ProductDetail.Where(o => o.CompanyId == CompanyId.ToString()).ToList();
            }
        }
        #endregion
    }
}
