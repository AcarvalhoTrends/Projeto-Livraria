using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLivrariaTrends.Models
{
    [Serializable]
    public class LiaLivroAutor
    {
        public decimal LIA_ID_AUTOR { get; set; }
        public decimal LIA_ID_LIVRO { get; set; }
        public decimal LIA_PC_ROYALTY { get; set; }

        public LiaLivroAutor(decimal asIdAutorLiaLv, decimal asIdLivroLiaLv, decimal asPcRoyaltLiaLv)
        {
            this.LIA_ID_AUTOR = asIdAutorLiaLv;
            this.LIA_ID_LIVRO = asIdLivroLiaLv;
            this.LIA_PC_ROYALTY = asPcRoyaltLiaLv;
        }
    }
}