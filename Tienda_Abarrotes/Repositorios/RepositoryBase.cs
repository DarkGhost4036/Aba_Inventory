using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda_Abarrotes.Repositorios
{
    public abstract class RepositoryBase
    {
        private readonly string _connectionString;


        public RepositoryBase()
        {
            _connectionString =
                "Server=LENOVO_LOQ\\VSGESTION;"+
                "Database=Tienda_Abarrotes_BD;"+
                "Integrated Security=true";
        }
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
