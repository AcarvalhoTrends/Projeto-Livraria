using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLivrariaTrends.Models
{
    [Serializable]
    public class Livros
    {
        public decimal LIV_ID_LIVRO { get; set; }
        public decimal LIV_ID_TIPO_LIVRO { get; set; }
        public decimal LIV_ID_EDITOR { get; set; }
        public string LIV_NM_TITULO { get; set; }
        public decimal LIV_VL_PRECO { get; set; }
        public decimal LIV_PC_ROYALTY { get; set; }
        public string LIV_DS_RESUMO { get; set; }
        public int LIV_NU_EDICAO { get; set; }

        public Livros()
        {
        }

        public Livros(decimal asIdLivro, decimal asIdTipoLivro, decimal asEditorLivro, string asNmTituloLivro, decimal asPrecoLivro, decimal asPcRoyaltLivro, string asResumoLivro, int asNuEdicaoLivro)
        {
            this.LIV_ID_LIVRO = asIdLivro;
            this.LIV_ID_TIPO_LIVRO = asIdTipoLivro;
            this.LIV_ID_EDITOR = asEditorLivro;
            this.LIV_NM_TITULO = asNmTituloLivro;
            this.LIV_VL_PRECO = asPrecoLivro;
            this.LIV_PC_ROYALTY = asPcRoyaltLivro;
            this.LIV_DS_RESUMO = asResumoLivro;
            this.LIV_NU_EDICAO = asNuEdicaoLivro;
        }
    }
}