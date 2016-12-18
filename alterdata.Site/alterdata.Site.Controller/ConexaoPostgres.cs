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
        private string _stringConnection = @"User ID=root;Password=myPassword;Host=localhost;Port=5432;Database=myDataBase;Pooling=true;Min Pool Size=0;Max Pool Size=100;Connection Lifetime=0";
        private NpgsqlConnection _conexao = null;

        private bool conectar()
        {
            _conexao = new NpgsqlConnection(_stringConnection);
            try
            {
                if(ConnectionState.Closed == _conexao.State)
                    _conexao.Open();
                    
                return true;
            }
            catch
            {
                _conexao.Close();
                return false;
            }
        }
        private bool desconectar()
        {
            try
            {
                if (_conexao.State != ConnectionState.Closed)
                {
                    _conexao.Close();
                    _conexao.Dispose();
                }
                return true;
            }
            catch
            {
                _conexao.Dispose();
                return false;
            }
        }

        public DataTable Pesquisa(string comandoSql)
        {
                       
            using ()
            {
                try
                {
                    if (_conexao.State == ConnectionState.Closed)
                    {
                        _conexao.Open();
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
