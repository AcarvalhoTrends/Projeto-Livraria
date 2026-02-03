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
    public partial class GerenciadorLivro : System.Web.UI.Page
    {
        //Criando uma variável de instância de LivroDAO (para não precisar instanciar uma todas as vezes que for usar).
        LivroDAO ioLivroDAO = new LivroDAO();

        //Utilizando uma ViewState, como uma propriedade privada da classe, para armazenar a lista de Livro cadastrados.
        public BindingList<Livros> ListaLivro
        {
            get
            {
                //Caso a ViewState esteja vazia, chama o método CarregaDados() para preencher os Livro.
                if ((BindingList<Livros>)ViewState["ViewStateListaLivro"] == null)
                    this.CarregaDados();
                // Retorna o conteúdo da ViewState.
                return (BindingList<Livros>)ViewState["ViewStateListaLivro"];
            }
            set
            {
                ViewState["ViewStateListaLivro"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CarregaDados();
        }

        private void CarregaDados()
        {
            try
            {
                //Chamando o método BuscaLivro para salvar os Livro cadastrados na ViewState.
                this.ListaLivro = this.ioLivroDAO.BuscaLivros();
                this.gvGerenciamentoLivros.DataSource = ioLivroDAO.BuscaLivros();
                this.gvGerenciamentoLivros.DataBind();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Criando o método BtnNovoAutor_Click descrito na propriedade OnClick deste botão no arquivo .aspx).
        protected void BtnNovoAutor_Click(object sender, EventArgs e)
        {
            try
            {
                decimal asIdLivro = this.ListaLivro.OrderByDescending(a => a.LIV_ID_LIVRO).First().LIV_ID_LIVRO + 1;
                decimal asIdTipoLivroTilTipo = Convert.ToDecimal(this.cmbCadastroTipoLivro.Value);
                decimal asIdEditor = Convert.ToDecimal(this.cmbCadastroIdEditorLivro.Value);
                string asldcsTitulo = this.tbxCadastroTituloLivro.Text;
                decimal asldcPreco = Convert.ToDecimal(this.tbxCadastroPrecoLivro.Text);
                decimal asldcRoyalty = Convert.ToDecimal(this.tbxCadastroRoyaltyLivro.Text);
                string asCsResumo = this.tbxCadastroResumoLivro.Text;
                int lniEdicao = Convert.ToInt32(this.tbxCadastroEdicaoLivro.Text);
                decimal ldcIdAutor = Convert.ToDecimal(this.cmbCadastroAutor.Value);

                Livros Iolivro = new Livros(asIdLivro, asIdTipoLivroTilTipo, asIdEditor, asldcsTitulo, asldcPreco, asldcRoyalty, asCsResumo, lniEdicao);
                this.ioLivroDAO.InserirLivros(Iolivro);
                this.CarregaDados();
                HttpContext.Current.Response.Write("<script>alert('Autor cadastrado com sucesso!');</script>");
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Erro no cadastro do Autor.');</script>");
            }

            this.cmbCadastroTipoLivro.Value = String.Empty;
            this.cmbCadastroIdEditorLivro.Value = String.Empty;
            this.tbxCadastroTituloLivro.Text = String.Empty;
            this.tbxCadastroPrecoLivro.Text = String.Empty;
            this.tbxCadastroRoyaltyLivro.Text = String.Empty;
            this.tbxCadastroResumoLivro.Text = String.Empty;
            this.tbxCadastroEdicaoLivro.Text = String.Empty;
        }

        protected void gvGerenciamentoLivro_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            try
            {
                // 1. Captura dos IDs (Keys) e Novos Valores (NewValues)
                decimal idLivro = Convert.ToDecimal(e.Keys["LIV_ID_LIVRO"]);
                decimal idTipoLivro = Convert.ToDecimal(e.NewValues["TIL_ID_TIPO_LIVRO"]);
                decimal idEditora = Convert.ToDecimal(e.NewValues["LIV_ID_EDITOR"]);
                string titulo = e.NewValues["LIV_NM_TITULO"]?.ToString();
                decimal preco = e.NewValues["LIV_VL_PRECO"] != null ? Convert.ToDecimal(e.NewValues["LIV_VL_PRECO"]) : 0;
                decimal royalty = e.NewValues["LIV_PC_ROYALTY"] != null ? Convert.ToDecimal(e.NewValues["LIV_PC_ROYALTY"]) : 0;
                string resumo = e.NewValues["LIV_DS_RESUMO"]?.ToString();
                int edicao = e.NewValues["LIV_NU_EDICAO"] != null ? Convert.ToInt32(e.NewValues["LIV_NU_EDICAO"]) : 1;

                // 2. Validações
                if (string.IsNullOrEmpty(titulo))
                {
                    ShowMessage("Informe o título do livro.");
                    return;
                }
                if (preco <= 0)
                {
                    ShowMessage("Informe um preço válido.");
                    return;
                }

                // 3. Mapeamento para o Objeto
                Livros livro = new Livros()
                {
                    LIV_ID_LIVRO = idLivro,
                    LIV_ID_TIPO_LIVRO = idTipoLivro,
                    LIV_ID_EDITOR = idEditora,
                    LIV_NM_TITULO = titulo,
                    LIV_VL_PRECO = preco,
                    LIV_PC_ROYALTY = royalty,
                    LIV_DS_RESUMO = resumo,
                    LIV_NU_EDICAO = edicao
                };

                this.ioLivroDAO.AtualizarLivros(livro);

                // 5. Finalização do Grid
                e.Cancel = true; // Interrompe a atualização automática do ASPxGridView
                this.gvGerenciamentoLivros.CancelEdit(); // Sai do modo de edição
                CarregaDados(); // Atualiza o grid com os novos dados
                ShowMessage("Livro atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                // É boa prática logar o erro real (ex.Message) para debugar
                ShowMessage("Erro na atualização do cadastro do livro.");
            }
        }

        protected void gvGerenciamentoLivro_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
        {
            try
            {
                // 1. Captura o ID do livro a partir das chaves do Grid
                decimal idLivro = Convert.ToDecimal(e.Keys["LIV_ID_LIVRO"]);
                ioLivroDAO.RemoverLivro(idLivro);

                // 3. Cancela a exclusão automática do ASPxGridView para usar a lógica manual acima
                e.Cancel = true;

                // 4. Atualiza os dados no componente visual
                CarregaDados();
                ShowMessage("Livro excluído com sucesso!");
            }
            catch
            {
                ShowMessage("Erro ao excluir o livro. Verifique se existem dependências vinculadas.");
            }
        }

        private void ShowMessage(string msg)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{msg}');", true);
        }

        protected void gvGerenciamentoLivro_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            try
            {
                // 1. Obtém o ID do livro da linha onde o botão foi clicado
                decimal livroId = Convert.ToDecimal(gvGerenciamentoLivros.GetRowValues(e.VisibleIndex, "LIV_ID_LIVRO"));

                // 2. Busca os dados completos do livro no banco de dados através do DAO
                var livro = ioLivroDAO.BuscaLivros(livroId).FirstOrDefault();

                // 3. Verifica qual botão customizado foi clicado
                if (e.ButtonID == "btnLivroInfo")
                {
                    // Espaço reservado para lógica de exibição de informações detalhadas
                }
                else if (e.ButtonID == "btnAutoresDoLivro")
                {
                    // Armazena o objeto livro na sessão para ser recuperado na tela de destino
                    Session["SessionLivroSelecionado"] = livro;

                    // Define uma propriedade JS para disparar o redirecionamento no lado do cliente (ClientSideEvents)
                    gvGerenciamentoLivros.JSProperties["cpRedirectToAutores"] = true;
                }
            }
            catch
            {
                // Tratamento de erro básico para evitar que a aplicação pare em caso de falha no callback
            }
        }
    }
}