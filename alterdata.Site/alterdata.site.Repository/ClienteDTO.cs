using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alterdata.Site.Domain
{
    public class ClienteDTO
    {
        public int id { get; set; }
        public string nome { get; set; }
        private string _cpf;
        public string cpf
        {
            get
            {
                return _cpf;
            }
            set
            {
                _cpf = value.Replace(".", "");
                _cpf = _cpf.Replace("-", "");
            }
        }
        public string rg { get; set; }
        public string endereco { get; set; }
        public string telefone { get; set; }         
    }
}
