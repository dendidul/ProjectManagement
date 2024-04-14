using Dapper;
using Infrastructure.Helper.Config;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class DataAccessClientRepository : IDataAccessClientRepository
    {


        private IConfigCreatorHelper _configuration;

        public DataAccessClientRepository(IConfigCreatorHelper configuration)
        {

            _configuration = configuration;
        }



        //public IEnumerable<T> GetList(string query, object param = null, CommandType commandType = CommandType.Text, string connectionString = "", string serverType = "")
        //{
        //    if (serverType == "SQLServer")
        //    {
        //        using (IDbConnection dbConnection = new SqlConnection(connectionString))
        //        {
        //            return dbConnection.Query<T>(query, param, commandType: commandType);
        //        }
        //    }
        //    else
        //    {
        //        using (IDbConnection dbConnection = new NpgsqlConnection(connectionString))
        //        {
        //            return dbConnection.Query<T>(query, param, commandType: commandType);
        //        }
        //    }



        //}

        public IEnumerable<TParam> GetList<TParam>(string query, object param = null, CommandType commandType = CommandType.Text, string connectionString = "", string serverType = "")
        {
            if (serverType == "SQLServer")
            {
                using (IDbConnection dbConnection = new SqlConnection(connectionString))
                {
                    return dbConnection.Query<TParam>(query, param, commandType: commandType);
                }
            }
            else
            {
                using (IDbConnection dbConnection = new NpgsqlConnection(connectionString))
                {
                    return dbConnection.Query<TParam>(query, param, commandType: commandType);
                }
            }

               
        }

        public TParam Get<TParam>(string query, object param = null, CommandType commandType = CommandType.Text, string connectionString = "", string serverType = "")
        {
            if (serverType == "SQLServer")
            {
                using (IDbConnection dbConnection = new SqlConnection(connectionString))
                {

                    return dbConnection.Query<TParam>(query, param, commandType: commandType).FirstOrDefault();

                }
            }
            else
            {
                using (IDbConnection dbConnection = new NpgsqlConnection(connectionString))
                {

                    return dbConnection.Query<TParam>(query, param, commandType: commandType).FirstOrDefault();
                }
            }

        }



        //public T Get(string query, object param = null, CommandType commandType = CommandType.Text, string connectionString = "", string serverType = "")
        //{
        //    if (serverType == "SQLServer")
        //    {
        //        using (IDbConnection dbConnection = new SqlConnection(connectionString))
        //        {

        //            return dbConnection.Query<T>(query, param, commandType: commandType).FirstOrDefault();

        //        }
        //    }
        //    else
        //    {
        //        using (IDbConnection dbConnection = new NpgsqlConnection(connectionString))
        //        {

        //            return dbConnection.Query<T>(query, param, commandType: commandType).FirstOrDefault();
        //        }
        //    }

        //}

        public bool Execute(string query, object param = null, CommandType commandType = CommandType.Text, string connectionString = "", string serverType = "")
        {
            if (serverType == "SQLServer")
            {
                using (IDbConnection dbConnection = new SqlConnection(connectionString))
                {
                    var result = dbConnection.Execute(query, param, commandType: commandType);
                    return result >= 0;
                }
            }
            else
            {
                using (IDbConnection dbConnection = new NpgsqlConnection(connectionString))
                {
                    var result = dbConnection.Execute(query, param, commandType: commandType);
                    return result >= 0;
                }

              
            }


        }

    }
}
