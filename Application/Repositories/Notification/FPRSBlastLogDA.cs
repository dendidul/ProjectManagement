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
    public class FPRSBlastLogDA : IFPRSBlastLogDA
    {
        private IDataAccessClientRepository _dataAccessClientRepository;
        private IConfigCreatorHelper _configCreatorHelper;
        private string Connection = "";
        private string ServerType = "";
        private string FPRSConnection = "";
        private string FPRSServerType = "";

        public FPRSBlastLogDA(IDataAccessClientRepository dataAccessClientRepository, IConfigCreatorHelper configCreatorHelper)
        {
            _dataAccessClientRepository = dataAccessClientRepository;
            _configCreatorHelper = configCreatorHelper;
            Connection = configCreatorHelper.Get("ConnectionDB:DMQILog:Constring");
            ServerType = configCreatorHelper.Get("ConnectionDB:DMQILog:ServerType");
            FPRSConnection = configCreatorHelper.Get("ConnectionDB:FPRS:Constring");
            FPRSServerType = configCreatorHelper.Get("ConnectionDB:FPRS:ServerType");
        }

        public long Insert(InsertFPRSBlastLog request)
        {
            long HeaderId = 0;
            var sql = @"sp_fprs_blast_log_insert_v2";
            var p = new DynamicParameters();

            p.Add("@o_intID", null, dbType: DbType.Int64, direction: ParameterDirection.Output);
            p.Add("@p_referral_code", request.ReferralCode, dbType: DbType.String);
            p.Add("@p_phone_no", request.PhoneNo, dbType: DbType.String);
            p.Add("@p_brand_id", request.BrandID, dbType: DbType.Int32);
            p.Add("@p_url_type", request.URLType, dbType: DbType.String);
            p.Add("@p_fprs_id", request.FPRSID, dbType: DbType.String);
            p.Add("@p_product_category_id", request.ProductCategoryID, dbType: DbType.String);
            p.Add("@p_region_id", request.RegionID, dbType: DbType.String);
            p.Add("@p_request_use_wa", request.RequestUseWA, dbType: DbType.Boolean);
            p.Add("@p_response_use_wa", request.ResponseUseWA, dbType: DbType.Boolean);
            p.Add("@p_wa_workspace", request.WAWorkspace, dbType: DbType.String);
            p.Add("@p_wa_template", request.WATemplate, dbType: DbType.String);
            p.Add("@p_wa_param", request.WAParam, dbType: DbType.String);
            p.Add("@p_sms_masking", request.SMSMasking, dbType: DbType.String);
            p.Add("@p_message_sms_template", request.MessageSMSTemplate, dbType: DbType.String);
            p.Add("@p_is_completed", request.IsCompleted, dbType: DbType.Boolean);
            p.Add("@p_completed_date", request.CompletedDate, dbType: DbType.DateTime);
            p.Add("@p_error_message", request.ErrorMessage, dbType: DbType.String);
            p.Add("@p_short_url", request.ShortURL, dbType: DbType.String);
            p.Add("@p_status_key", request.StatusKey, dbType: DbType.String);
            p.Add("@p_created_date", request.CreatedDate, dbType: DbType.DateTime);
            p.Add("@p_created_by", request.CreatedBy, dbType: DbType.String);
            p.Add("@p_request_param", request.RequestParam, dbType: DbType.String);

            _dataAccessClientRepository.Execute(sql, p, CommandType.StoredProcedure, Connection, ServerType);
            HeaderId = p.Get<long>("@o_intID");
            return HeaderId;
        }

        public void UpsertFPRSBlastSummary(FPRSBlastSummary request)
        {
            var sql = @"sp_DMQI_UpsertFprsBlastSummary";
            var p = new DynamicParameters();
          
            p.Add("@fprs_id", request.fprsid, dbType: DbType.String);
            p.Add("@phone_number", request.phoneNumber, dbType: DbType.String);
            p.Add("@is_success", request.is_success, dbType: DbType.Int32);
            p.Add("@request_is_wa", request.requestIsWA, dbType: DbType.Boolean);
            p.Add("@response_is_wa", request.responseIsWA, dbType: DbType.Boolean);
            p.Add("@notification_type", request.notificationType, dbType: DbType.String);

            _dataAccessClientRepository.Execute(sql, p, CommandType.StoredProcedure, FPRSConnection, FPRSServerType);
        }

    }
}
