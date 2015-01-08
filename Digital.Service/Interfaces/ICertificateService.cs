using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Digital.Contact.Models;
using System.ServiceModel;

namespace Digital.Service.Interfaces
{
    [ServiceContract(Name = "ICertificateService", SessionMode = SessionMode.Required)]
    public interface ICertificateService
    {
        [OperationContract]
        CertificateModel CertificateInsert(CertificateModel Model);
        [OperationContract]
        CertificateModel CertificateUpdate(CertificateModel Model);
        [OperationContract]
        CertificateModel CertificateQueryById(int CertificateId);
        [OperationContract]
        CertificateModel CertificateQueryByName(string CertificateName);
        [OperationContract]
        bool CertificateDeleteById(int CertificateId);
        [OperationContract]
        bool CertificateDeleteByIds(string CertificateIds);
        [OperationContract]
        bool CertificateDeleteByCompany(int CompanyId);
        [OperationContract]
        List<CertificateModel> CertificateQueryList();
        [OperationContract]
        List<CertificateModel> CertificateQueryListByCompany(int CompanyId);
    }
}
