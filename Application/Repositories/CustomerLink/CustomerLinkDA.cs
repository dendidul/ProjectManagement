using Dapper;
using Core.Dto.CustomerLink;
using Infrastructure.Helper.Config;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.CustomerLink
{
    public class CustomerLinkDA : ICustomerLinkDA
    {
        private IDataAccessClientRepository _dataAccessClientRepository;
        private IConfigCreatorHelper _configCreatorHelper;
        private string Connection = "";
        private string ServerType = "";

        public CustomerLinkDA(IDataAccessClientRepository dataAccessClientRepository, IConfigCreatorHelper configCreatorHelper)
        {
            _dataAccessClientRepository = dataAccessClientRepository;
            _configCreatorHelper = configCreatorHelper;
            Connection = configCreatorHelper.Get("ConnectionDB:FPRS:Constring");
            ServerType = configCreatorHelper.Get("ConnectionDB:FPRS:ServerType");
        }

        public List<CustomerLinkResponse> GetUnsentLink()
        {
            var sql = "sp_DMQI_getCustUnsentLink";

            var p = new DynamicParameters();


            var result = _dataAccessClientRepository.GetList<CustomerLinkResponse>(sql, p
            , System.Data.CommandType.StoredProcedure, Connection, ServerType).ToList();
            return result;
        }

        public void UpdateSentLink(string fprsId, string shortUrl,string notes)
        {
            var sql = "sp_DMQI_UpdateSentlinkCustomer";

            var p = new DynamicParameters();
            p.Add("@fprsId", fprsId, dbType: DbType.String);
            p.Add("@shortlink", shortUrl, dbType: DbType.String);
            p.Add("@notes", notes, dbType: DbType.String);


            var result = _dataAccessClientRepository.Execute(sql, p
            , System.Data.CommandType.StoredProcedure, Connection, ServerType);


        }




    }
}
