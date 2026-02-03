using System;
using System.Data.SqlClient;
using ProjetoLivrariaTrends.Models;
using System.ComponentModel;
using System.Configuration;


namespace ProjetoLivrariaTrends.DAO
{
    public class LivroDAO
    {
        SqlCommand ioQuery;
        SqlConnection ioConexao;
        public BindingList<Livros> BuscaLivros(decimal? asIdLivro = null)
        {
            BindingList<Livros> ioListLivros = new BindingList<Livros>();
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                ioConexao.Open();
                try
                {
                    if (asIdLivro != null)
                    {
                        ioQuery = new SqlCommand("SELECT * FROM LIV_LIVROS WHERE LIV_ID_LIVRO = @idLivro", ioConexao);
                        ioQuery.Parameters.Add(new SqlParameter("@idLivro", asIdLivro));
                    }
                    else
                    {
                        ioQuery = new SqlCommand("SELECT * FROM LIV_LIVROS", ioConexao);
                    }
                    using (SqlDataReader IoReader = ioQuery.ExecuteReader())
                    {
                        while (IoReader.Read())
                        {
                            Livros ioNovosLivros = new Livros(
                            Convert.ToDecimal(IoReader["LIV_ID_LIVRO"]),
                            Convert.ToDecimal(IoReader["LIV_ID_TIPO_LIVRO"]),
                            Convert.ToDecimal(IoReader["LIV_ID_EDITOR"]),
                            IoReader["LIV_NM_TITULO"].ToString(),
                            Convert.ToDecimal(IoReader["LIV_VL_PRECO"]),
                            Convert.ToDecimal(IoReader["LIV_PC_ROYALTY"]),
                            IoReader["LIV_DS_RESUMO"] == DBNull.Value ? null : IoReader["LIV_DS_RESUMO"].ToString(),
                            Convert.ToInt32(IoReader["LIV_NU_EDICAO"]));           
                        }
                    }
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);

                }
            }
            return ioListLivros;
        }

        public int InserirLivros(Livros ioNovosLivros)
        {
            if (ioNovosLivros ==  null)
                throw new NullReferenceException();
            int liQtdRegistrosInseridos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = (new SqlCommand("INSERT INTO LIV_LIVROS (LIV_ID_LIVRO,LIV_ID_TIPO_LIVRO,LIV_ID_EDITOR,LIV_NM_TITULO,LIV_VL_PRECO,LIV_PC_ROYALTY,LIV_DS_RESUMO,LIV_NU_EDICAO) VALUES(@idLivro,@idTipoLivro,@idEditorLiv,@NmTiTuloLiv,@PcLivro,@PcRoyaltLiv,@ResumoLiv,@nuEdicaoLiv)", ioConexao));
                    ioQuery.Parameters.Add(new SqlParameter("@idLivro", ioNovosLivros.LIV_ID_LIVRO));
                    ioQuery.Parameters.Add(new SqlParameter("@idTipoLivro", ioNovosLivros.LIV_ID_TIPO_LIVRO));
                    ioQuery.Parameters.Add(new SqlParameter("@idEditorLiv", ioNovosLivros.LIV_ID_EDITOR));
                    ioQuery.Parameters.Add(new SqlParameter("@NmTiTuloLiv", ioNovosLivros.LIV_NM_TITULO));
                    ioQuery.Parameters.Add(new SqlParameter("@PcLivro", ioNovosLivros.LIV_VL_PRECO));
                    ioQuery.Parameters.Add(new SqlParameter("@PcRoyaltLiv", ioNovosLivros.LIV_PC_ROYALTY));
                    ioQuery.Parameters.Add(new SqlParameter("@ResumoLiv", ioNovosLivros.LIV_DS_RESUMO));
                    ioQuery.Parameters.Add(new SqlParameter("@nuEdicaoLiv", ioNovosLivros.LIV_NU_EDICAO));
                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch
                { 
                    throw new Exception("Erro ao inserir Editor"); 
                }
            }
            return liQtdRegistrosInseridos;
        }





        public int RemoverLivro(Livros aoLivro)
        {
            if (aoLivro == null)
                throw new NullReferenceException();
            int liQtdRegistrosRemovidos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {  

                    ioConexao.Open();
                    ioQuery = new SqlCommand("DELETE FROM LIV_LIVROS WHERE LIV_ID_LIVRO = @idLivro", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idLivro", aoLivro.LIV_ID_LIVRO));
                    liQtdRegistrosRemovidos = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao inserir livro");
                }
            }
            return liQtdRegistrosRemovidos;
        }
        public int RemoverLivro(decimal idLivro)
        {

            Livros objParaRemover = new Livros { LIV_ID_LIVRO = idLivro };
            return RemoverLivro(objParaRemover);
        }
        public int AtualizarLivros(Livros aoLivro)
        {
            if (aoLivro == null)
                throw new NullReferenceException();
            int liQtdRegistrosAtulizados = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {

                    ioConexao.Open();
                    ioQuery = new SqlCommand("UPDATE LIV_LIVROS SET  LIV_NM_TITULO = @NmTiTuloLiv, LIV_VL_PRECO = @PcLivro, LIV_PC_ROYALTY = @PcRoyaltLiv, LIV_DS_RESUMO = @ResumoLiv, LIV_NU_EDICAO = @nuEdicaoLiv WHERE LIV_ID_LIVRO = @idLivro", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idLivro", aoLivro.LIV_ID_LIVRO));
                    ioQuery.Parameters.Add(new SqlParameter("@idTipoLivro", aoLivro.LIV_ID_TIPO_LIVRO));
                    ioQuery.Parameters.Add(new SqlParameter("@idEditorLiv", aoLivro.LIV_ID_EDITOR));
                    ioQuery.Parameters.Add(new SqlParameter("@NmTiTuloLiv", aoLivro.LIV_NM_TITULO));
                    ioQuery.Parameters.Add(new SqlParameter("@PcLivro", aoLivro.LIV_VL_PRECO));
                    ioQuery.Parameters.Add(new SqlParameter("@PcRoyaltLiv", aoLivro.LIV_PC_ROYALTY));
                    ioQuery.Parameters.Add(new SqlParameter("@ResumoLiv", aoLivro.LIV_DS_RESUMO));
                    ioQuery.Parameters.Add(new SqlParameter("@nuEdicaoLiv", aoLivro.LIV_NU_EDICAO));
                    liQtdRegistrosAtulizados = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro real: " + ex.Message);
                }
                return liQtdRegistrosAtulizados;
            }
           
        }
    }
}
