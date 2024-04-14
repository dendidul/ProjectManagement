using System.Data;

namespace Infrastructure.Repository
{
    public interface IDataAccessClientRepository
    {
        //IEnumerable<T> GetList(string query, object param = null, CommandType commandType = CommandType.Text, string connectionString = "", string serverType = "");

        //T Get(string query, object param = null, CommandType commandType = CommandType.Text, string connectionString = "", string serverType = "");

        bool Execute(string query, object param = null, CommandType commandType = CommandType.Text, string connectionString = "", string serverType = "");
        IEnumerable<TParam> GetList<TParam>(string query, object param = null, CommandType commandType = CommandType.Text, string connectionString = "", string serverType = "");
        TParam Get<TParam>(string query, object param = null, CommandType commandType = CommandType.Text, string connectionString = "", string serverType = "");
    }
}