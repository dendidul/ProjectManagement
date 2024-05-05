using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Dto.User;
using Infrastructure.Helper.Config;
using Dapper;
using System.Data;
using Infrastructure.Repository;

namespace Application.Repositories.UserApi
{
    public class UserAPIDA:IUserAPIDA
    {
        private IDataAccessClientRepository _dataAccessClientRepository;
        private  IConfigCreatorHelper _configCreatorHelper;
        private string Connection = "";
        private string ServerType = "";

        public UserAPIDA(IDataAccessClientRepository dataAccessClientRepository, IConfigCreatorHelper configCreatorHelper)
        {
            _dataAccessClientRepository = dataAccessClientRepository;
            _configCreatorHelper = configCreatorHelper;
            Connection = configCreatorHelper.Get("ConnectionDB:DMQI:Constring");
            ServerType = configCreatorHelper.Get("ConnectionDB:DMQI:ServerType");
        }

        
        public UserAPI AuthorizedUrlByAPIKeyAndSecret(string apikey,string secretkey,string url_method)
        {
          

            var sql = @"sp_AuthorizeAccessWithCredentialAndURL";

            var p = new DynamicParameters();
            p.Add("@apikey", apikey, dbType: DbType.String);
            p.Add("@secretkey", secretkey, dbType: DbType.String);
            p.Add("@url_method", url_method, dbType: DbType.String);

            var result = _dataAccessClientRepository.Get<UserAPI>(sql, p
            , System.Data.CommandType.StoredProcedure, Connection, ServerType);

            //var result = _dataAccessClientRepository.Get(sql, new
            //{
            //    @apikey = apikey,
            //    @secretkey = secretkey,
            //    @url_method = url_method 
            //}, System.Data.CommandType.Text, Connection, ServerType);

            //var sql = @"select user_api_id,api_key,secret_key,api_key_name,url_method from users_api as ua 
            //            join users_api_access as uac on ua.user_api_id = uac.user_id
            //            ";

            //var result = _dataAccessClientRepository.Get(sql, null, System.Data.CommandType.Text, Connection, ServerType);

            return result;


        }

        public UserAPI AuthorizeUrlByAPIKey(string apikey, string url_method)
        {
            //var sql = @"select user_api_id,api_key,secret_key,api_key_name,url_method from users_api as ua 
            //            join users_api_access as uac on ua.user_api_id = uac.user_id
            //            where api_key = @apikey and url_method = @url_method and uac.is_active = 1";

            //var result = _dataAccessClientRepository.Get(sql, new
            //{
            //    @apikey = apikey,               
            //    @url_method = url_method
            //}, System.Data.CommandType.Text, Connection, ServerType);

            var sql = @"sp_AuthorizeAccessByApiKeyAndUrl";

            var p = new DynamicParameters();
            p.Add("@apikey", apikey, dbType: DbType.String);         
            p.Add("@url_method", url_method, dbType: DbType.String);

            var result = _dataAccessClientRepository.Get<UserAPI>(sql, p
            , System.Data.CommandType.StoredProcedure, Connection, ServerType);


            //var sql = @"select user_api_id,api_key,secret_key,api_key_name,url_method from users_api as ua 
            //            join users_api_access as uac on ua.user_api_id = uac.user_id
            //            ";

            //var result = _dataAccessClientRepository.Get(sql, null, System.Data.CommandType.Text, Connection, ServerType);

            return result;


        }


        public UserAPI GetUserApi(string apikey)
        {
            //var sql = @"select user_api_id,api_key,api_key_name,secret_key from users_api as ua 
            //            where api_key = @apikey ";

            //var result = _dataAccessClientRepository.Get(sql, new
            //{
            //    @apikey = apikey

            //}, System.Data.CommandType.Text, Connection, ServerType);

            var sql = @"sp_getUserAPI";

            var p = new DynamicParameters();
            p.Add("@apikey", apikey, dbType: DbType.String);          

            var result = _dataAccessClientRepository.Get<UserAPI>(sql, p
            , System.Data.CommandType.StoredProcedure, Connection, ServerType);


            //var sql = @"select user_api_id,api_key,secret_key,api_key_name,url_method from users_api as ua 
            //            join users_api_access as uac on ua.user_api_id = uac.user_id
            //            ";

            //var result = _dataAccessClientRepository.Get(sql, null, System.Data.CommandType.Text, Connection, ServerType);

            return result;

        }

    }
}
