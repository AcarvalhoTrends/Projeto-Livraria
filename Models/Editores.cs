using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLivrariaTrends.Models
{
    [Serializable]
    public class Editores
    {
        public decimal EDI_ID_EDITOR { get; set; }
        public string EDI_NM_EDITOR { get; set; }
        public string EDI_DS_EMAIL { get; set; }
        public string EDI_DS_URL { get; set; }

        public Editores(decimal asIdEditor, string asNmEditor, string asEmailEditor, string asUrlEditor)
        {
            this.EDI_ID_EDITOR = asIdEditor;
            this.EDI_NM_EDITOR = asNmEditor;
            this.EDI_DS_EMAIL = asEmailEditor;
            this.EDI_DS_URL = asUrlEditor;
        }
        public Editores() {}
    }
}