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
    public class CasesService
    {
        public bool CasesInsert(CasesModel Model)
        {
            throw new NotImplementedException();
        }

        public CasesModel CasesUpdate(CasesModel Model)
        {
            throw new NotImplementedException();
        }

        public CasesModel CasesQueryById(int CasesId)
        {
            throw new NotImplementedException();
        }

        public CasesModel CasesQueryByName(string CasesName)
        {
            throw new NotImplementedException();
        }

        public bool CasesDeleteById(int CasesId)
        {
            throw new NotImplementedException();
        }

        public bool CasesDeleteByCategory(int CasesCategoryId)
        {
            throw new NotImplementedException();
        }

        public bool CasesDeleteByCompany(int CompanyId)
        {
            throw new NotImplementedException();
        }

        public List<CasesModel> CasesQueryList()
        {
            throw new NotImplementedException();
        }

        public List<CasesModel> CasesQueryListByCategory(int CasesCategoryId)
        {
            throw new NotImplementedException();
        }

        public List<CasesModel> CasesQueryListByCompany(int CompanyId)
        {
            throw new NotImplementedException();
        }
    }
}
