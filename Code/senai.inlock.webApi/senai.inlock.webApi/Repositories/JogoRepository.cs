using senai.inlock.webApi_.Domains;
using senai.inlock.webApi_.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi_.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        string stringConexao = "Data Source =DESKTOP-VVNQ5LN; initial catalog=inlock_games_manha; integrated security =true";

        public void Create(JogoDomain novoJogo)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryCreate = "INSERT INTO Jogos (nomeJogo, descricao, dataLancamento, valor, idEstudio) VALUES (@nomeJogo, @descricao, @dataLancamento, @valor, @idEstudio)";

                using (SqlCommand cmd = new SqlCommand(queryCreate, con))
                {
                    cmd.Parameters.AddWithValue("@nomeJogo", novoJogo.nomeJogo);
                    cmd.Parameters.AddWithValue("@descricao", novoJogo.descricao);
                    cmd.Parameters.AddWithValue("@dataLancamento", novoJogo.dataLancamento);
                    cmd.Parameters.AddWithValue("@valor", novoJogo.valor);
                    cmd.Parameters.AddWithValue("@idEstudio", novoJogo.idEstudio);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<JogoDomain> Read()
        {
            List<JogoDomain> listaJogos = new List<JogoDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = "SELECT idJogo, nomeJogo, descricao, dataLancamento, valor, idEstudio FROM Jogos";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        JogoDomain jogo = new JogoDomain()
                        {
                            idJogo = Convert.ToInt32(rdr[0]),
                            nomeJogo = rdr[1].ToString(),
                            descricao = rdr[2].ToString(),
                            dataLancamento = Convert.ToDateTime(rdr[3]),
                            valor = Convert.ToDouble(rdr[4]),
                            idEstudio = Convert.ToInt32(rdr[5])
                        };

                        listaJogos.Add(jogo);
                    }
                }
            }
            return listaJogos;
        }

        public JogoDomain ReadById(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryReadById = "SELECT * FROM Jogos WHERE idJogo = @Id";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(queryReadById, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        JogoDomain jogo = new JogoDomain()
                        {
                            idJogo = Convert.ToInt32(rdr[0]),
                            nomeJogo = rdr[1].ToString(),
                            descricao = rdr[2].ToString(),
                            dataLancamento = Convert.ToDateTime(rdr[3]),
                            valor = Convert.ToDouble(rdr[4]),
                            idEstudio = Convert.ToInt32(rdr[5])
                        };

                        return jogo;
                    }

                    return null;
                }
            }
        }

        public void Update(JogoDomain jogoAtualizado, int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdate = "UPDATE Jogos SET nomeJogo = @nomeJogo, descricao = @descricao, dataLancamento = @dataLancamento, valor = @valor, idEstudio = @idEstudio WHERE idJogo = @Id";

                using (SqlCommand cmd = new SqlCommand(queryUpdate, con))
                {
                    cmd.Parameters.AddWithValue("@nomeJogo", jogoAtualizado.nomeJogo);
                    cmd.Parameters.AddWithValue("@descricao", jogoAtualizado.descricao);
                    cmd.Parameters.AddWithValue("@dataLancamento", jogoAtualizado.dataLancamento);
                    cmd.Parameters.AddWithValue("@valor", jogoAtualizado.valor);
                    cmd.Parameters.AddWithValue("@idEstudio", jogoAtualizado.idEstudio);
                    cmd.Parameters.AddWithValue("@Id", id);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM Jogos WHERE idJogo = @Id";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
