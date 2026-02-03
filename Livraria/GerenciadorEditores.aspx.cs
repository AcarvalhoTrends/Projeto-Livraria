using DevExpress.Web;
using DevExpress.Web.Data;
using ProjetoLivrariaTrends.DAO;
using ProjetoLivrariaTrends.Models;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjetoLivrariaTrends.Livraria
{
    public partial class GerenciadorEditores : System.Web.UI.Page
    {


        EditoresDAO IoEditoresDAO = new EditoresDAO();

        // Utilizando uma ViewState, como uma propriedade privada da classe, para armazenar a lista de autores cadastrados.
        public BindingList<Editores> ListaEditores
        {
            get
            {
                // Caso a ViewState esteja vazia, chama o método CarregaDados() para preencher os autores.
                if ((BindingList<Editores>)ViewState["ViewStateListaEditores"] == null)
                {
                    this.CarregaDados();
                }
                // Retorna o conteúdo da ViewState.
                return (BindingList<Editores>)ViewState["ViewStateListaEditores"];
            }
            set
            {
                ViewState["ViewStateListaEditores"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CarregaDados();
        }

        private void CarregaDados()
        {
            // Chamando o método BuscaAutores para salvar os autores cadastrados na ViewState.
            this.ListaEditores = this.IoEditoresDAO.BuscaEditores();
            this.gvGerenciamentoEditores.DataSource = ListaEditores;
            this.gvGerenciamentoEditores.DataBind();
        }

        // Criando o método BtnNovoAutor_Click descrito na propriedade OnClick deste botão no arquivo .aspx.
        protected void BtnSalvar_Click(object sender, EventArgs e)
        {
            try
            {

                decimal asIdEditor = this.ListaEditores.OrderByDescending(a => a.EDI_ID_EDITOR).First().EDI_ID_EDITOR + 1;

                string NomeEditor = this.tbxCadastroNomeEditor.Text;
                string EmailEditor = this.tbxCadastroEmailEditor.Text;
                string UrlEditor = this.tbxCadastroUrlEditor.Text;


                Editores ioEditor = new Editores(asIdEditor, NomeEditor, EmailEditor, UrlEditor);
                this.IoEditoresDAO.InsereEditores(ioEditor);


                this.CarregaDados();

                HttpContext.Current.Response.Write("<script>alert('Autor cadastrado com sucesso!');</script>");
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Erro no cadastro de editores.');</script>");
            }


            this.tbxCadastroNomeEditor.Text = String.Empty;
            this.tbxCadastroEmailEditor.Text = String.Empty;
            this.tbxCadastroUrlEditor.Text = String.Empty;
        }
        protected void gvGerenciamentoEditores_RowUpdating(Object sender, ASPxDataUpdatingEventArgs e)
        {
            e.Cancel = true;
            try
            {
                decimal asIdEditor = Convert.ToDecimal(e.Keys["EDI_ID_EDITOR"]);
                string NomeEditor = e.NewValues["EDI_NM_EDITOR"]?.ToString();
                string EmailEditor = e.NewValues["EDI_DS_EMAIL"]?.ToString();
                string UrlEditor = e.NewValues["EDI_DS_URL"]?.ToString();
                if (string.IsNullOrEmpty(NomeEditor))
                {
                    ShowMessage("informe o autor");
                    return;
                }
                else if (string.IsNullOrEmpty(EmailEditor))
                {
                    ShowMessage("informe o email");
                    return;
                }
                else if (string.IsNullOrEmpty(UrlEditor))
                {
                    ShowMessage("informe o url");
                    return;
                }
                Editores editor = new Editores()
                {
                    EDI_ID_EDITOR = asIdEditor,
                    EDI_NM_EDITOR = NomeEditor,
                    EDI_DS_EMAIL = EmailEditor,
                    EDI_DS_URL = UrlEditor
                };
                this.IoEditoresDAO.AtualizarEditores(editor);
                 
                this.gvGerenciamentoEditores.CancelEdit(); 
                CarregaDados();
                ShowMessage("editor atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);
            }
         }
        protected void gvGerenciamentoEditores_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
        {
            try
            {
                decimal asIdEditor = Convert.ToDecimal(e.Keys["EDI_ID_EDITOR"]);
                IoEditoresDAO.RemoverEditor(asIdEditor);
                e.Cancel = true;
                this.gvGerenciamentoEditores.CancelEdit();

                CarregaDados();
               
            }
            catch 
            {
                HttpContext.Current.Response.Write("<script>alert('Erro no cadastro do Autor.');</script>");
            }
        }
        private void ShowMessage(string msg)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{msg}');", true);
        }
     
        protected void gvGerenciamentoEditores_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
           
            if (e.ButtonID == "btnLivros")
            {
               
                object idEditor = gvGerenciamentoEditores.GetRowValues(e.VisibleIndex, "EDI_ID_EDITOR");

                
                Session["SessionEditorSelecionado"] = idEditor;

                 gvGerenciamentoEditores.JSProperties["cpRedirectToLivros"] = true;
            }
            else if (e.ButtonID == "btnAutorInfo")
            {
             }
        }
        // ESTE É O MÉTODO QUE FALTA PARA O ERRO SUMIR
        protected void gvGerenciamentoEditores_PageIndexChanged(object sender, EventArgs e)
        {
            // Recarrega os dados para a nova página da tabela funcionar
            this.CarregaDados();
        }
    }
}


    