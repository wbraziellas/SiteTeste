using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data;
using alterdata.Site;

namespace alterdata.Site.Domain
{
    public class ConexaoPostgres
    {
        private string _stringConnection = @"User ID=root;Password=myPassword;Host=localhost;Port=5432;Database=myDataBase;Pooling=true;Min Pool Size=0;Max Pool Size=100;Connection Lifetime=0";
        private NpgsqlConnection _conexao = null;
        private string _sqlCommand = string.Empty;
               

        public bool inserirClienteS(ClienteDTO cliente)
        {
            _sqlCommand = @"INSERT INTO CLIENTES("+
                "   ID,"+
                "   NOME,"+
                "   CPF,"+
                "   RG,"+
                "   ENDERECO,"+
                "   TELEFONE)"+
                "VALUES("+
                "   @ID,"+
                "   @NOME,"+
                "   @CPF,"+
                "   @RG,"+
                "   @ENDERECO,"+
                "   @TELEFONE);";

            

            if(conectar())
            {
                try
                {
                    var sqlCmd = new NpgsqlCommand(_sqlCommand, _conexao);
                    sqlCmd.Parameters.Add(new NpgsqlParameter("@ID", cliente.id));
                    sqlCmd.Parameters.Add(new NpgsqlParameter("@NOME", cliente.nome));
                    sqlCmd.Parameters.Add(new NpgsqlParameter("@CPF", cliente.cpf));
                    sqlCmd.Parameters.Add(new NpgsqlParameter("@RG", cliente.rg));
                    sqlCmd.Parameters.Add(new NpgsqlParameter("@ENDERECO", cliente.endereco));
                    sqlCmd.Parameters.Add(new NpgsqlParameter("@TELEFONE", cliente.telefone));

                    sqlCmd.ExecuteNonQuery();

                    return true;
                }              
                catch(NpgsqlException sqlError)
                {
                    throw sqlError;
                }
                finally
                {
                    desconectar();
                }
            }
            else
            {
                return false;
            }
        }

        public bool atualizarCliente(ClienteDTO cliente)
        {
            if (conectar())
            {
                _sqlCommand = @"UPDATE CLIENTES SET" +
                    "   NOME = @NOME," +
                    "   NOME = @CPF," +
                    "   NOME = @RG," +
                    "   NOME = @ENDERECO," +
                    "   NOME = @TELEFONE" +
                    "WHERE ID = @ID";
                try
                {
                    var sqlCmd = new NpgsqlCommand(_sqlCommand, _conexao);
                    sqlCmd.Parameters.Add(new NpgsqlParameter("@ID", cliente.id));
                    sqlCmd.Parameters.Add(new NpgsqlParameter("@NOME", cliente.nome));
                    sqlCmd.Parameters.Add(new NpgsqlParameter("@CPF", cliente.cpf));
                    sqlCmd.Parameters.Add(new NpgsqlParameter("@RG", cliente.rg));
                    sqlCmd.Parameters.Add(new NpgsqlParameter("@ENDERECO", cliente.endereco));
                    sqlCmd.Parameters.Add(new NpgsqlParameter("@TELEFONE", cliente.telefone));

                    sqlCmd.ExecuteNonQuery();

                    return true;
                }
                catch (NpgsqlException sqlError)
                {
                    throw sqlError;
                }
                finally
                {
                    desconectar();
                }
            }
            else
            {
                return false;
            }
        }
    
        public IEnumerable<ClienteDTO> Pesquisa(string nome)
        {
            _sqlCommand = @"select * from clientes where nome like /    ";


            try
            {
                conectar();

                var sql = new NpgsqlCommand(_sqlCommand, _conexao);                
                var adapter = new NpgsqlDataAdapter() { SelectCommand = sql };
                var data = new DataTable();

                adapter.Fill(data);

                return ConverterDataEmClienteDTO(data);
            }
            catch(NpgsqlException sqlError)
            {
                throw sqlError;
            }
            finally
            {
                _conexao.Close();
            }
                        
        }

        #region Métodos privados

        private bool conectar()
        {
            _conexao = new NpgsqlConnection(_stringConnection);
            try
            {
                if (ConnectionState.Closed == _conexao.State)
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

        private IEnumerable<ClienteDTO> ConverterDataEmClienteDTO(DataTable data)
        {
            var listaCliente = new List<ClienteDTO>();
            foreach(DataRow row in data.Rows)
            {
                var linha = row.ItemArray;
                var cliente = new ClienteDTO()
                {
                    id = int.Parse(linha[0].ToString()),
                    nome = linha[1].ToString(),
                    cpf = linha[3].ToString(),
                    rg = linha[4].ToString(),
                    endereco = linha[5].ToString(),
                    telefone = linha[6].ToString()
                };
                listaCliente.Add(cliente);                
            }

            return listaCliente;
        }
        #endregion
        
    }
}
