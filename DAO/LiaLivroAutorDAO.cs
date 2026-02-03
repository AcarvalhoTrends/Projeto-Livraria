using System;
using DevExpress.Web;
using DevExpress.Web.Data;
using ProjetoLivrariaTrends.DAO;
using ProjetoLivrariaTrends.Models;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace ProjetoLivrariaTrends.Livraria
{
    public partial class GerenciadorCategoria : System.Web.UI.Page
    {
        TipoLivrosDAO ioTipoLivroDAO = new TipoLivrosDAO();

        // Propriedade para controlar os dados
        public BindingList<TipoLivro> ListaCategoria
        {
            get
            {
                if (ViewState["ViewStateListaCategoria"] == null)
                    this.AtualizarListaNoViewState(); // Método auxiliar criado abaixo
                return (BindingList<TipoLivro>)ViewState["ViewStateListaCategoria"];
            }
            set
            {
                ViewState["ViewStateListaCategoria"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // CORREÇÃO 1: Só carrega dados na primeira vez. 
            // Nos PostBacks (paginação), o evento DataBinding cuidará disso.
            if (!IsPostBack)
            {
                this.gvGerenciadorCategoria.DataBind();
            }
        }

        // CORREÇÃO 2: Evento OBRIGATÓRIO para o Grid do DevExpress funcionar paginação
        protected void gvGerenciadorCategoria_DataBinding(object sender, EventArgs e)
        {
            // O Grid pede os dados aqui. Passamos a lista do ViewState.
            (sender as ASPxGridView).DataSource = ListaCategoria;
        }

        // Método auxiliar para buscar do banco e guardar no ViewState
        private void AtualizarListaNoViewState()
        {
            try
            {
                this.ListaCategoria = ioTipoLivroDAO.BuscaTipoLivros();
            }
            catch (Exception ex)
            {
                ShowMessage("Erro ao buscar dados: " + ex.Message);
            }
        }

        protected void BtnNovoAutor_Click(object sender, EventArgs e)
        {
            try
            {
                // Lógica de ID (Nota: Idealmente o banco deveria ser Identity/AutoIncrement)
                decimal novoId = 1;
                if (this.ListaCategoria != null && this.ListaCategoria.Any())
                {
                    novoId = this.ListaCategoria.Max(a => a.TIL_ID_TIPO_LIVRO) + 1;
                }

                string desc = this.cmbCadastroCategoria.Text;
                TipoLivro ioCategoria = new TipoLivro(novoId, desc);

                this.ioTipoLivroDAO.InsereTipoLivros(ioCategoria);

                // CORREÇÃO 3: Atualiza o ViewState e manda o Grid se redesenhar
                AtualizarListaNoViewState();
                this.gvGerenciadorCategoria.DataBind();

                this.cmbCadastroCategoria.Text = ""; // Limpa campo
                ShowMessage("Categoria cadastrada com sucesso!");
            }
            catch (Exception ex)
            {
                ShowMessage("Erro no cadastro: " + ex.Message);
            }
        }

        protected void gvGerenciadorCategoria_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            try
            {
                decimal idCategoria = Convert.ToDecimal(e.Keys["TIL_ID_TIPO_LIVRO"]);
                string descricao = e.NewValues["TIL_DS_DESCRICAO"]?.ToString(); // Use ?. para evitar NullReference

                if (string.IsNullOrEmpty(descricao))
                {
                    throw new Exception("Nome da categoria é obrigatório.");
                }

                TipoLivro categoria = new TipoLivro(idCategoria, descricao);

                this.ioTipoLivroDAO.AtulizarCategoria(categoria);

                // Importante: Cancela a edição padrão do Grid
                e.Cancel = true;
                this.gvGerenciadorCategoria.CancelEdit();

                // Recarrega dados do banco e atualiza o grid
                AtualizarListaNoViewState();
                this.gvGerenciadorCategoria.DataBind();

                ShowMessage("Atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                e.Cancel = true; // Mantém o grid em modo de edição para o usuário tentar de novo
                ShowMessage("Erro ao atualizar: " + ex.Message);
            }
        }

        protected void gvGerenciadorCategoria_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
        {
            try
            {
                decimal idCategoria = Convert.ToDecimal(e.Keys["TIL_ID_TIPO_LIVRO"]);
                ioTipoLivroDAO.RemoverTipo(idCategoria);

                e.Cancel = true;

                AtualizarListaNoViewState();
                this.gvGerenciadorCategoria.DataBind();

                ShowMessage("Excluído com sucesso!");
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                ShowMessage("Erro ao excluir: " + ex.Message);
            }
        }

        private void ShowMessage(string msg)
        {
            // ScriptManager é mais seguro se estiver usando UpdatePanel, mas ClientScript funciona para PostBacks normais
            ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{msg}');", true);
        }
    }
}