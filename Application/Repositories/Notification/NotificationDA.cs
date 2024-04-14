using Dapper;
using Core.Dto.Notification;
using Infrastructure.Helper.Config;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.Notification
{
    public class NotificationDA : INotificationDA
    {
        private IDataAccessClientRepository _dataAccessClientRepository;
        private IConfigCreatorHelper _configCreatorHelper;
        private string Connection = "";
        private string ServerType = "";

        public NotificationDA(IDataAccessClientRepository dataAccessClientRepository, IConfigCreatorHelper configCreatorHelper)
        {
            _dataAccessClientRepository = dataAccessClientRepository;
            _configCreatorHelper = configCreatorHelper;
            Connection = configCreatorHelper.Get("ConnectionDB:DMQI:Constring");
            ServerType = configCreatorHelper.Get("ConnectionDB:DMQI:ServerType");
        }

        public long Insert(NotificationInsert request)
        {
            long HeaderId = 0;
            var sql = @"sp_notification_insert";
            var p = new DynamicParameters();

            p.Add("@p_notification_type", request.NotificationType, dbType: DbType.String);
            p.Add("@p_phone_no", request.PhoneNo, dbType: DbType.String);
            p.Add("@p_sender_type", request.SenderType, dbType: DbType.String);
            p.Add("@p_notification_message", request.NotificationMessage, dbType: DbType.String);
            p.Add("@p_transaction_header_id", request.TransactionHeaderID, dbType: DbType.Int64);
            p.Add("@p_email_from", request.EmailFrom, dbType: DbType.String);
            p.Add("@p_email_to", request.EmailTo, dbType: DbType.String);
            p.Add("@p_sms_masking", request.SMSMasking, dbType: DbType.String);
            p.Add("@p_wa_workspace", request.WAWorkspace, dbType: DbType.String);
            p.Add("@p_wa_template", request.WATemplate, dbType: DbType.String);
            p.Add("@p_wa_params", request.WAParams, dbType: DbType.String);
            p.Add("@p_send_date", request.SendDate, dbType: DbType.DateTime);
            p.Add("@p_is_completed", request.IsCompleted, dbType: DbType.Boolean);
            p.Add("@p_created_by", request.CreatedBy, dbType: DbType.String);
            p.Add("@p_created_date", request.CreatedDate, dbType: DbType.DateTime);
            p.Add("@p_fprs_id", request.FPRSId, dbType: DbType.String);
            p.Add("@p_brand_id", request.BrandId, dbType: DbType.Int32);
            p.Add("@p_product_category_id", request.ProductCategoryId, dbType: DbType.String);
            p.Add("@o_intID", null, dbType: DbType.Int64, direction: ParameterDirection.Output);

            _dataAccessClientRepository.Execute(sql, p, CommandType.StoredProcedure, Connection, ServerType);
            HeaderId = p.Get<long>("@o_intID");
            return HeaderId;
        }


    }
}
