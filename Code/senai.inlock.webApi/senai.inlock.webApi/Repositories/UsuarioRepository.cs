using senai.inlock.webApi_.Domains;
using senai.inlock.webApi_.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi_.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        string stringConexao = "Data Source =DESKTOP-VVNQ5LN; initial catalog=inlock_games_manha; integrated security =true";

        public void Create(UsuarioDomain novoUsuario)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryCreate = "INSERT INTO Usuarios (email, senha, idTipoUsuario) VALUES (@email, @senha, @idTipoUsuario)";

                using (SqlCommand cmd = new SqlCommand(queryCreate, con))
                {
                    cmd.Parameters.AddWithValue("@email", novoUsuario.email);
                    cmd.Parameters.AddWithValue("@senha", novoUsuario.senha);
                    cmd.Parameters.AddWithValue("@idTipoUsuario", novoUsuario.idTipoUsuario);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM Usuarios WHERE idUsuario = @Id";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<UsuarioDomain> Read()
        {
            List<UsuarioDomain> listaUsuarios = new List<UsuarioDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = "SELECT idUsuario, email, senha, idTipoUsuario FROM Usuarios";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        UsuarioDomain usuario = new UsuarioDomain()
                        {
                            idUsuario = Convert.ToInt32(rdr[0]),
                            email = rdr[1].ToString(),
                            senha = rdr[2].ToString(),
                            idTipoUsuario = Convert.ToInt32(rdr[3])
                        };

                        listaUsuarios.Add(usuario);
                    }
                }
            }
            return listaUsuarios;
        }

        public UsuarioDomain ReadById(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = "SELECT idUsuario, email, senha, idTipoUsuario FROM Usuarios WHERE idUsuario = @Id";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {

                    cmd.Parameters.AddWithValue("@Id", id);

                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        UsuarioDomain usuario = new UsuarioDomain()
                        {
                            idUsuario = Convert.ToInt32(rdr[0]),
                            email = rdr[1].ToString(),
                            senha = rdr[2].ToString(),
                            idTipoUsuario = Convert.ToInt32(rdr[3])
                        };

                        return usuario;
                    }
                }
            }
            return null;
        }

        public void Update(UsuarioDomain usuarioAtualizado, int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdate = "UPDATE Usuarios SET email = @email, senha = @senha, idTipoUsuario = @idTipoUsuario WHERE idUsuario = @Id";

                using (SqlCommand cmd = new SqlCommand(queryUpdate, con))
                {
                    cmd.Parameters.AddWithValue("@email", usuarioAtualizado.email);
                    cmd.Parameters.AddWithValue("@senha", usuarioAtualizado.senha);
                    cmd.Parameters.AddWithValue("@idTipoUsuario", usuarioAtualizado.idTipoUsuario);
                    cmd.Parameters.AddWithValue("@Id", id);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public UsuarioDomain Login(string email, string senha)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryLogin = "SELECT idUsuario, email, senha, idTipoUsuario FROM Usuarios WHERE email = @email AND senha = @senha";

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(queryLogin, con))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@senha", senha);

                    con.Open();

                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        UsuarioDomain usuarioBuscado = new UsuarioDomain()
                        {
                            idUsuario = Convert.ToInt32(rdr[0]),
                            email = rdr[1].ToString(),
                            senha = rdr[2].ToString(),
                            idTipoUsuario = Convert.ToInt32(rdr[3])
                        };

                        return usuarioBuscado;
                    }

                    return null;
                }
            }
        }
    }
}
