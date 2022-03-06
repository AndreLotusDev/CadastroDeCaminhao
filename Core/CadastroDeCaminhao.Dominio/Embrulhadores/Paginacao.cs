using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroDeCaminhao.Dominio.Embrulhadores
{
    public class Paginacao<TModel>
    {
        const int PaginaMaxima = 100;
        private int _tamanhoDaPagina;

        public Paginacao()
        {
            Itens = new List<TModel>();
        }

        public int TamanhoDaPagina
        {
            get => _tamanhoDaPagina;
            set => _tamanhoDaPagina = (value > PaginaMaxima) ? PaginaMaxima : value;
        }
        public int PaginaAtual { get; set; }
        public int TotalDeElementos { get; set; }
        public int TotalDePagina { get; set; }
        public List<TModel> Itens { get; set; }
    }

}
