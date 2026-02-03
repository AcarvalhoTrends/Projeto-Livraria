using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ProjetoLivrariaTrends.Models;
using System.ComponentModel;
using System.Configuration;

namespace ProjetoLivrariaTrends.DAO
{
    public class AutoresDAO
    {
        SqlCommand ioQuery;
        SqlConnection ioConexao;

        public BindingList<Autores> BuscaAutores(decimal? adcIdautor = null)
        {
            BindingList<Autores> ioListAutores = new BindingList<Autores>();
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    if (adcIdautor != null)
                    {
                        ioQuery = new SqlCommand("SELECT * FROM AUT_AUTORES WHERE AUT_ID_AUTOR = @idAutor", ioConexao);
                        ioQuery.Parameters.Add(new SqlParameter("@idAutor", adcIdautor));
                    }
                    else
                    {
                        ioQuery = new SqlCommand("SELECT * FROM AUT_AUTORES", ioConexao);
                    }
                    using (SqlDataReader IoReader = ioQuery.ExecuteReader())
                    {
                        while (IoReader.Read())
                        {
                            Autores IoNovoAutor = new Autores(IoReader.GetDecimal(0), IoReader.GetString(1), IoReader.GetString(2), IoReader.GetString(3));
                            ioListAutores.Add(IoNovoAutor);
                        }
                    }
                }
                catch
                {
                    throw new Exception("Erro ao ler autor");
                }
            }
            return ioListAutores;
        }

        public int InsereAutores(Autores aoNovoAutor)
        {
            if (aoNovoAutor == null)
                throw new NullReferenceException();
            int liQtdRegistrosInseridos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = (new SqlCommand("INSERT INTO AUT_AUTORES(AUT_ID_AUTOR, AUT_NM_NOME, AUT_NM_SOBRENOME, AUT_DS_EMAIL) VALUES(@idAutor,@NomeAutor,@sobrenomeAutor,@emailAutor)", ioConexao));
                    ioQuery.Parameters.Add(new SqlParameter("@idAutor", aoNovoAutor.AUT_ID_AUTOR));
                    ioQuery.Parameters.Add(new SqlParameter("@NomeAutor", aoNovoAutor.AUT_NM_NOME));
                    ioQuery.Parameters.Add(new SqlParameter("@sobrenomeAutor", aoNovoAutor.AUT_NM_SOBRENOME));
                    ioQuery.Parameters.Add(new SqlParameter("@emailAutor", aoNovoAutor.AUT_DS_EMAIL));
                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro real: " + ex.Message);
                }
            }
            return liQtdRegistrosInseridos;
        }

        public int RemoveAutor(Autores aoAutor)
        {
            if (aoAutor == null)
                throw new NullReferenceException();
            int liQtdRegistrosRemovidos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = (new SqlCommand("DELETE FROM AUT_AUTORES WHERE AUT_ID_AUTOR =@idAutor", ioConexao));
                    ioQuery.Parameters.Add(new SqlParameter("@idAutor", aoAutor.AUT_ID_AUTOR));

                    liQtdRegistrosRemovidos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return liQtdRegistrosRemovidos;
        }
        public int RemoveAutor(decimal idAutor)
        {
             
            Autores objParaRemover = new Autores { AUT_ID_AUTOR = idAutor };
            return RemoveAutor(objParaRemover);
        }

        public int AtualizarAutores(Autores aoAutor)
        {
            if (aoAutor == null)
                throw new NullReferenceException();
            int liQtdRegistrosAtulizados = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = (new SqlCommand(@"UPDATE AUT_AUTORES SET AUT_NM_NOME = @NomeAutor, AUT_NM_SOBRENOME = @sobrenomeAutor, AUT_DS_EMAIL = @emailAutor WHERE AUT_ID_AUTOR = @idAutor", ioConexao));
                    ioQuery.Parameters.Add(new SqlParameter("@idAutor", aoAutor.AUT_ID_AUTOR));
                    ioQuery.Parameters.Add(new SqlParameter("@NomeAutor", aoAutor.AUT_NM_NOME));
                    ioQuery.Parameters.Add(new SqlParameter("@sobrenomeAutor", aoAutor.AUT_NM_SOBRENOME));
                    ioQuery.Parameters.Add(new SqlParameter("@emailAutor", aoAutor.AUT_DS_EMAIL));
                    liQtdRegistrosAtulizados = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro real: " + ex.Message);
                }
            }
            return liQtdRegistrosAtulizados;
        }

        public BindingList<Livros> FindLivrosByAutor(Autores autor)
        {
            BindingList<Livros> listaLivrosAutor = new BindingList<Livros>();
            if (autor == null)
                throw new NullReferenceException();
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("SELECT lvr.LIV_ID_LIVRO,lvr.LIV_ID_TIPO_LIVRO,lvr.LIV_ID_EDITOR,lvr.LIV_NM_TITULO,lvr.LIV_VL_PRECO,lvr.LIV_PC_ROYALTY,lvr.LIV_DS_RESUMO,aut.AUT_ID_AUTORFROM LIA_LIVRO_AUTOR AS lINNER JOIN LIV_LIVROS AS lvr ON l.LIA_ID_LIVRO = lvr.LIV_ID_LIVROINNER JOIN AUT_AUTORES AS aut ON l.LIA_ID_AUTOR = aut.AUT_ID_AUTOR WHERE l.LIA_ID_AUTOR = @idAutorLivro", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idAutorLivro", autor.AUT_ID_AUTOR));
                    using (SqlDataReader IoReader = ioQuery.ExecuteReader())
                    {
                        while (IoReader.Read())
                        {
                            Livros ioNovaListaLivroAutores = new Livros(IoReader.GetDecimal(0), IoReader.GetDecimal(1), IoReader.GetDecimal(2), IoReader.GetString(3), IoReader.GetDecimal(4), IoReader.GetDecimal(5), IoReader.GetString(6), IoReader.GetInt16(7));
                            listaLivrosAutor.Add(ioNovaListaLivroAutores);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro real: " + ex.Message);
                }
            }
            return listaLivrosAutor;
        }

    }


}

