using Core.Dto.OrderTransaction;
using Dapper;
using Infrastructure.Helper.Config;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.OrderTransaction
{
    public class OrderTransactionDA : IOrderTransactionDA
    {
        private IDataAccessClientRepository _dataAccessClientRepository;
        private IConfigCreatorHelper _configCreatorHelper;
     
        private string Connection = "";
        private string ServerType = "";

        public OrderTransactionDA(IDataAccessClientRepository dataAccessClientRepository, IConfigCreatorHelper configCreatorHelper)
        {
            _dataAccessClientRepository = dataAccessClientRepository;
            _configCreatorHelper = configCreatorHelper;
           
            Connection = configCreatorHelper.Get("ConnectionDB:POSKHD:Constring");
            ServerType = configCreatorHelper.Get("ConnectionDB:POSKHD:ServerType");
        }


        public List<TransactionHeader> GetUnsentPointOrder()
        {
            var sql = "sp_get_list_send_transaction_to_loyalty";

            var p = new DynamicParameters();


            var result = _dataAccessClientRepository.GetList<TransactionHeader>(sql, p
            , System.Data.CommandType.StoredProcedure, Connection, ServerType).ToList();
            return result;
        }


        public List<TransactionDetailsProduct> GetDetailsTransaction(Int64 orderTransactionId)
        {
            var sql = "sp_get_list_transaction_product_by_id";

            var p = new DynamicParameters();
            p.Add("@ordertransactionid", orderTransactionId, dbType: DbType.Int64);


            var result = _dataAccessClientRepository.GetList<TransactionDetailsProduct>(sql, p
            , System.Data.CommandType.StoredProcedure, Connection, ServerType).ToList();
            return result;
        }

        public void UpdateResultSendPoint(Int64 OrderTransactionId,string status, int point, string errorMessage)
        {
            //var p = new DynamicParameters();

            string lala = @"
                update order_transactions 
                set point = " + point + @",
                send_point_loyalty_status  = '" + status + @"',
                send_point_loyalty_date  = now(),
                send_point_loyalty_errormessage  = '" + errorMessage + @"'
                where order_transaction_id = " + OrderTransactionId;

            _dataAccessClientRepository.Execute(
                @"
                update order_transactions 
                set point = "+point+@",
                send_point_loyalty_status  = '"+status+@"',
                send_point_loyalty_date  = now(),
                send_point_loyalty_errormessage  = '"+errorMessage+@"'
                where order_transaction_id = "+OrderTransactionId
               ,null, System.Data.CommandType.Text, Connection, ServerType);

        }
    }
}
