using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Configuration;
using ProjetoLivrariaTrends.Models;

namespace ProjetoLivrariaTrends.DAO
{
    public class TipoLivrosDAO
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        // Método para buscar todas as categorias
        public BindingList<TipoLivro> BuscaTipoLivros()
        {
            BindingList<TipoLivro> lista = new BindingList<TipoLivro>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT TIL_ID_TIPO_LIVRO, TIL_DS_DESCRICAO FROM TIL_TIPO_LIVRO";
                SqlCommand cmd = new SqlCommand(sql, conn);

                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Certifique-se que o construtor da model TipoLivro aceita (decimal, string)
                            TipoLivro tipo = new TipoLivro(
                                reader.GetDecimal(0),
                                reader.GetString(1)
                            );
                            lista.Add(tipo);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao buscar tipos de livros: " + ex.Message);
                }
            }
            return lista;
        }

        // Método para inserir (utilizado no seu BtnNovoAutor_Click)
        public void InsereTipoLivros(TipoLivro novoTipo)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO TIL_TIPO_LIVRO (TIL_ID_TIPO_LIVRO, TIL_DS_DESCRICAO) VALUES (@id, @desc)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", novoTipo.TIL_ID_TIPO_LIVRO);
                cmd.Parameters.AddWithValue("@desc", novoTipo.TIL_DS_DESCRICAO);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Método para remover (utilizado no seu gvGerenciadorCategoria_RowDeleting)
        public void RemoverTipo(decimal id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "DELETE FROM TIL_TIPO_LIVRO WHERE TIL_ID_TIPO_LIVRO = @id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Método para atualizar (utilizado no seu gvGerenciadorCategoria_RowUpdating)
        public void AtulizarCategoria(TipoLivro categoria)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "UPDATE TIL_TIPO_LIVRO SET TIL_DS_DESCRICAO = @desc WHERE TIL_ID_TIPO_LIVRO = @id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@desc", categoria.TIL_DS_DESCRICAO);
                cmd.Parameters.AddWithValue("@id", categoria.TIL_ID_TIPO_LIVRO);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}