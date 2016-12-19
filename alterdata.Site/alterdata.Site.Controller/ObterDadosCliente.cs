using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alterdata.Site.Domain;

namespace alterdata.Site.Controller
{
    public class ObterDadosCliente
    {
        #region propriedades
        private ClienteDtoRepository _clienteDtoRepository;
        private ClienteDtoRepository clienteDtoRepository
        {
            get { return _clienteDtoRepository ?? (_clienteDtoRepository = new ClienteDtoRepository()); }
        }
        #endregion

        public void ObterPesquisaClientes(string nome)
        {
            clienteDtoRepository.Pesquisa(nome);
        }

        public void AtualizarCliente(ClienteDTO cliente)
        {
            clienteDtoRepository.atualizarCliente(cliente);
        }

        public void inserircliente(ClienteDTO cliente)
        {
            clienteDtoRepository.inserirCliente(cliente);
        }
    }
}
