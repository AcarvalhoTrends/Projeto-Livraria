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
    public partial class GerenciamentoAutores : Page
    {
        //Criando uma variável de instância de AutoresDAO (para não precisar instanciar uma todas as vezes que for usar).
        AutoresDAO ioAutoresDAO = new AutoresDAO();

        //Utilizando uma ViewState, como uma propriedade privada da classe, para armazenar a lista de autores cadastrados.
        public BindingList<Autores> ListaAutores
        {
            get
            {
                //Caso a ViewState esteja vazia, chama o método CarregaDados() para preencher os autores.
                if ((BindingList<Autores>)ViewState["ViewStateListaAutores"] == null)
                    this.CarregaDados();

                //Retorna o conteúdo da ViewState.
                return (BindingList<Autores>)ViewState["ViewStateListaAutores"];
            }
            set
            {
                ViewState["ViewStateListaAutores"] = value;
            }
        }
        public Autores AutorSessao
        {
            get { return (Autores)Session["SessionAutorSelecionado"]; }
            set { Session["SessionAutorSelecionado"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            CarregaDados();
        }

        private void CarregaDados()
        {
            //Chamando o método BuscaAutores para salvar os autores cadastrados na ViewState.

            //Chamando o método BuscaAutores para salvar os autores cadastrados na ViewState.
            this.ListaAutores = this.ioAutoresDAO.BuscaAutores();
            if (gvGerenciamentoAutores != null)
            {
                this.gvGerenciamentoAutores.DataSource = ListaAutores;
                this.gvGerenciamentoAutores.DataBind();
            }
        }

        //Criando o método BtnNovoAutor_Click descrito na propriedade OnClick deste botão no arquivo .aspx.
        protected void BtnNovoAutor_Click(object sender, EventArgs e)
        {
            try
            {
                //Utilizando o Linq para obter o maior ID de autores cadastrados e incrementando o valor em 1
                //para garantir que a chave primária não se repita (esse campo não é auto-increment no banco).
                decimal ldcIdAutor = this.ListaAutores.OrderByDescending(a => a.AUT_ID_AUTOR).First().AUT_ID_AUTOR + 1;

                //Salvando os valores que o usuário preencheu em cada campo do formulário (utilizando o "this.NomeDoControle"
                //é possível recuperar o controle e acessar suas propriedades, isso é possível pois todo controle ASP tem
                //um ID único na página e deve ser marcado como runat="server" para virar um "ServerControl" e ser acessível
                //aqui no "CodeBehind" da página.
                string lsNomeAutor = this.tbxCadastroNomeAutor.Text;
                string lsSobrenomeAutor = this.tbxCadastroSobrenomeAutor.Text;
                string lsEmailAutor = this.tbxCadastroEmailAutor.Text;

                //Instanciando um objeto do tipo Autores para ser adicionado (perceba que só existe um construtor para essa classe
                //onde devem ser passados todos os valores, fizemos isso como mais uma forma de garantir que não será possível
                //cadastrar autores com informações faltando, mesmo que o banco permita isso - além dos RequiredFieldValidator).
                Autores ioAutor = new Autores(ldcIdAutor, lsNomeAutor, lsSobrenomeAutor, lsEmailAutor);

                //Chamando o método de inserir o novo autor na base de dados.
                this.ioAutoresDAO.InsereAutores(ioAutor);

                //Atualizando a ViewState com o novo autor recém-inserido.
                this.CarregaDados();

                HttpContext.Current.Response.Write("<script>alert('Autor cadastrado com sucesso!');</script>");
            }
            catch(Exception ex)
            {
                HttpContext.Current.Response.Write("<script>alert('Erro no cadastro do Autor.');</script>" + ex.Message);
            }

            //Limpando os campos do formulário.
            this.tbxCadastroNomeAutor.Text = string.Empty;
            this.tbxCadastroSobrenomeAutor.Text = string.Empty;
            this.tbxCadastroEmailAutor.Text = string.Empty;
        }
        protected void gvGerenciamentoAutores_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            try
            {
                if (e.Keys["AUT_ID_AUTOR"] == null)
                {
                    ShowMessage("Erro ao atualizar autor: ID do autor não encontrado");
                    return;
                }

                decimal autorId = Convert.ToDecimal(e.Keys["AUT_ID_AUTOR"]);
                string nome = e.NewValues["AUT_NM_NOME"]?.ToString();
                string sobrenome = e.NewValues["AUT_NM_SOBRENOME"]?.ToString();
                string email = e.NewValues["AUT_DS_EMAIL"]?.ToString();

                if (string.IsNullOrWhiteSpace(nome))
                {
                    ShowMessage("Informe o nome do autor");
                    return;
                }
                else if (string.IsNullOrWhiteSpace(sobrenome))
                {
                    ShowMessage("Informe o sobrenome do autor");
                    return;
                }
                else if (string.IsNullOrWhiteSpace(email))
                {
                    ShowMessage("Informe o email do autor");
                    return;
                }

                Autores autor = new Autores()
                {
                    AUT_ID_AUTOR = autorId,
                    AUT_NM_NOME = nome,
                    AUT_NM_SOBRENOME = sobrenome,
                    AUT_DS_EMAIL = email
                };
                this.ioAutoresDAO.AtualizarAutores(autor);

                e.Cancel = true;

                CarregaDados();
                ShowMessage("Autor atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                ShowMessage("Erro na atualização de cadastro: " + ex.Message);
            }
        }

        protected void gvGerenciamentoAutores_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
        {
            decimal autorId = Convert.ToDecimal(e.Keys["AUT_ID_AUTOR"]);
            ioAutoresDAO.RemoveAutor(autorId);
            e.Cancel = true;
            CarregaDados();
        }
        private void ShowMessage(string msg)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{msg}');", true);
        }

        protected void gvGerenciamentoAutores_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            decimal autorId = Convert.ToDecimal(gvGerenciamentoAutores.GetRowValues(e.VisibleIndex, "AUT_ID_AUTOR"));
            var autor = ioAutoresDAO.BuscaAutores(autorId).FirstOrDefault();

            if (e.ButtonID == "btnAutorInfo")
            {
                // Espaço vazio para lógica futura
            }
            else if (e.ButtonID == "btnLivros")
            {
                Session["SessionAutorSelecionado"] = autor;

                gvGerenciamentoAutores.JSProperties["cpRedirectToLivros"] = true;
            }
        }
    }
}
    


    

    
 