using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data;
using alterdata.site.Repository;

namespace alterdata.Site.Controller
{
    public class ConexaoPostgres
    {
        public DataTable ConectarBasePostGres(string comandoSql)
        {
            var stringConnection = "User ID=root;Password=myPassword;Host=localhost;Port=5432;Database=myDataBase;Pooling=true;Min Pool Size=0;Max Pool Size=100;Connection Lifetime=0";
            
            using (var conexao = new NpgsqlConnection(stringConnection))
            {
                try
                {
                    if (conexao.State == ConnectionState.Closed)
                    {
                        conexao.Open();
                    }

                    var sqlCommand = new NpgsqlCommand(comandoSql);
                    var adapter = new NpgsqlDataAdapter() { SelectCommand = sqlCommand };
                    var data = new DataTable();

                    adapter.Fill(data);

                    return data;
                }
                catch
                {
                    return new DataTable();
                }
                finally
                {
                    conexao.Close();
                }
            }            
        }

                
    }
}
