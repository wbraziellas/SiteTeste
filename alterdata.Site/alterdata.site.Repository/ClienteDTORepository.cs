using System;
using System.Collections.Generic;
using alterdata.Site.Domain;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alterdata.site.Repository
{
    public static class ClienteDTORepository
    {
        public static string selectCostumers()
        {
            var comandoSql = "select * from clientes";
            return comandoSql;
        }

        public static string updateCostumer()
        {
            var comandoSql = "update clientes set id = @_id, nome = @_nome, cpf = @_cpf, rg = @_rg, endereco = @_endereco, telefone = @_telefone";
            return comandoSql;
        }

        public static string addCostumer(ClienteDTO cliente)
        {
            var comandoSql = "insert into clientes(id, nome, cpf, rg, endereco, telefone)values()";
            return comandoSql;
        }
    }
}
