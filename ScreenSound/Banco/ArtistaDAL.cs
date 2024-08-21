using Microsoft.Data.SqlClient;
using ScreenSound.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.Banco
{
    internal class ArtistaDAL
    {
        public IEnumerable<Artista> List()
        {
            var list = new List<Artista>();
            using var connection = new Connection().GetConnection();
            connection.Open();

            string sql = "SELECT * FROM Artistas";
            SqlCommand cmd = new SqlCommand(sql, connection);
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string nome = Convert.ToString(reader["Nome"]);
                string bio = Convert.ToString(reader["Bio"]);
                string foto = Convert.ToString(reader["FotoPerfil"]);
                int id = Convert.ToInt32(reader["Id"]);

                Artista artista = new Artista(nome, bio) { Id = id, FotoPerfil = foto };

                list.Add(artista);
            }

            return list;
        }

        public void Add(Artista artista)
        {
            using var connection = new Connection().GetConnection();
            connection.Open();

            string sql = "INSERT INTO Artistas (Nome, FotoPerfil, Bio) VALUES (@nome, @perfilPadrao, @bio)";
            SqlCommand cmd = new SqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@nome", artista.Nome);
            cmd.Parameters.AddWithValue("@perfilPadrao", artista.FotoPerfil);
            cmd.Parameters.AddWithValue("@bio", artista.Bio);

            int retorno = cmd.ExecuteNonQuery();
            Console.WriteLine($"Comando executado, {retorno} linhas afetadas.");
        }

        public void Delete(Artista artista)
        {
            using var connection = new Connection().GetConnection();
            connection.Open();

            string sql = $"DELETE FROM Artistas WHERE Id = @id";
            SqlCommand cmd = new SqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@id", artista.Id);

            int retorno = cmd.ExecuteNonQuery();
            Console.WriteLine($"Comando executado, {retorno} linhas afetadas.");
        }

        public void Update(Artista artista)
        {
            using var connection = new Connection().GetConnection();
            connection.Open();

            string sql = "UPDATE Artistas SET Nome = @nome, Bio = @bio WHERE Id = @id";
            SqlCommand cmd = new SqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@nome", artista.Nome);
            cmd.Parameters.AddWithValue("@bio", artista.Bio);
            cmd.Parameters.AddWithValue("@id", artista.Id);

            int retorno = cmd.ExecuteNonQuery();
            Console.WriteLine($"Comando executado, {retorno} linhas afetadas.");
        }
    }
}
