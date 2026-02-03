using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLivrariaTrends.Models
{
    [Serializable]
    public class Autores
    {
        public decimal AUT_ID_AUTOR { get; set; }
        public string AUT_NM_NOME { get; set; }
        public string AUT_NM_SOBRENOME { get; set; }
        public string AUT_DS_EMAIL { get; set; }
        public Autores(decimal adcIdautor, string asNomeAutor, string asSobrenomeAutor, string asEmailAutor)
        {
            this.AUT_ID_AUTOR = adcIdautor;
            this.AUT_NM_NOME = asNomeAutor;
            this.AUT_NM_SOBRENOME = asSobrenomeAutor;
            this.AUT_DS_EMAIL = asEmailAutor;
        }
        public Autores() { }

    }
}