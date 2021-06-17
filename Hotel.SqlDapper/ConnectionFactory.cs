using Hotel.Domain.Environment;
using System.Data.SqlClient;

namespace Hotel.SqlDapper
{
    class ConnectionFactory
    {
        public static SqlConnection Build(string connectionString = null)
        {
            return new SqlConnection(connectionString ?? Config.Get.SqlConnection);
        }
    }
}
