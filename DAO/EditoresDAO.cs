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
    public class EditoresDAO
    {
        SqlCommand ioQuery;
        SqlConnection ioConexao;

        public BindingList<Editores> BuscaEditores(decimal? asIdEditor = null)
        {
            BindingList<Editores> ioListEditores = new BindingList<Editores>();
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    if (asIdEditor != null)
                    {
                        ioQuery = new SqlCommand("SELECT * FROM EDI_EDITORES WHERE EDI_ID_EDITOR = @idEditor", ioConexao);
                        ioQuery.Parameters.Add(new SqlParameter("@idEditor", asIdEditor));
                    }
                    else
                    {
                        ioQuery = new SqlCommand("SELECT * FROM EDI_EDITORES", ioConexao);
                    }
                    using (SqlDataReader IoReader = ioQuery.ExecuteReader())
                    {
                        while (IoReader.Read())
                        {
                            Editores ioNovoEditor = new Editores(IoReader.GetDecimal(0), IoReader.GetString(1), IoReader.GetString(2), IoReader.GetString(3));
                            ioListEditores.Add(ioNovoEditor);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return ioListEditores;
        }
        
        public int InsereEditores(Editores ioNovoEditor)
        {
            if (ioNovoEditor == null)
                throw new NullReferenceException();
            int liQtdRegistrosInseridos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = (new SqlCommand("INSERT INTO EDI_EDITORES (EDI_ID_EDITOR,EDI_NM_EDITOR,EDI_DS_EMAIL,EDI_DS_URL) VALUES (@idEditor,@NmEditor,@emailEditor,@editorUrl)", ioConexao));
                    ioQuery.Parameters.Add(new SqlParameter("@idEditor", ioNovoEditor.EDI_ID_EDITOR));
                    ioQuery.Parameters.Add(new SqlParameter("@NmEditor", ioNovoEditor.EDI_NM_EDITOR));
                    ioQuery.Parameters.Add(new SqlParameter("@emailEditor", ioNovoEditor.EDI_DS_EMAIL));
                    ioQuery.Parameters.Add(new SqlParameter("@editorUrl", ioNovoEditor.EDI_DS_URL));
                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch 
                {
                    throw new Exception("Erro ao inserir Editor");
                }

            }
            return liQtdRegistrosInseridos;
        }

        public int RemoverEditor(Editores aoEditor)
        {
            if (aoEditor == null)
                throw new NullReferenceException("O objeto Editor não pode ser nulo");
            int liQtdRegistrosRemovidos = 0;
            using (SqlConnection ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("SELECT COUNT(*) FROM LIV_LIVROS WHERE LIV_ID_EDITOR = @idEditor", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idEditor", aoEditor.EDI_ID_EDITOR));
                    int count = (int)ioQuery.ExecuteScalar();
                    if (count == 0) 
                        {
                        SqlCommand ioQuery = new SqlCommand("DELETE FROM EDI_EDITORES WHERE EDI_ID_EDITOR = @idEditor", ioConexao);
                        ioQuery.Parameters.Add(new SqlParameter("@idEditor", aoEditor.EDI_ID_EDITOR));
                        liQtdRegistrosRemovidos = ioQuery.ExecuteNonQuery();
                        }
                    else
                    {
                        HttpContext.Current.Response.Write("<script>alert('Não é possivel remover editor.');</script>");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao remover Editor", ex);
                }
            }
            return liQtdRegistrosRemovidos;
        }

        public int RemoverEditor(decimal idEditor)
        {
            Editores objParaRemover = new Editores { EDI_ID_EDITOR = idEditor };
            return RemoverEditor(objParaRemover);
        }


        public int AtualizarEditores(Editores aoEditor)
        {
            if (aoEditor == null)
                throw new NullReferenceException();
            int liQtdRegistrosAtualizados = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = (new SqlCommand("UPDATE EDI_EDITORES SET EDI_NM_EDITOR = @NmEditor,EDI_DS_EMAIL = @emailEditor,  EDI_DS_URL = @editorUrl WHERE EDI_ID_EDITOR = @idEditor", ioConexao));
                    ioQuery.Parameters.Add(new SqlParameter("@idEditor", aoEditor.EDI_ID_EDITOR));
                    ioQuery.Parameters.Add(new SqlParameter("@NmEditor", aoEditor.EDI_NM_EDITOR));
                    ioQuery.Parameters.Add(new SqlParameter("@emailEditor", aoEditor.EDI_DS_EMAIL));
                    ioQuery.Parameters.Add(new SqlParameter("@editorUrl", aoEditor.EDI_DS_URL));
                    liQtdRegistrosAtualizados = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message) ;
                }

            }
            return liQtdRegistrosAtualizados;
        }










    }
}
