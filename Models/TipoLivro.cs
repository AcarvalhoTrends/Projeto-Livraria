using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLivrariaTrends.Models
{
    [Serializable]
    public class TipoLivro
    {
        public decimal TIL_ID_TIPO_LIVRO { get; set; }
        public string TIL_DS_DESCRICAO { get; set; }

        public TipoLivro(decimal asIdTipoLivroTilTipo, string asDescricaoTilTipo)
        {

            this.TIL_ID_TIPO_LIVRO = asIdTipoLivroTilTipo;
            this.TIL_DS_DESCRICAO = asDescricaoTilTipo;
        }
        public TipoLivro() { }
    }
    
}