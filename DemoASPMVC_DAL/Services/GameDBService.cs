using DemoASPMVC_DAL.Interface;
using DemoASPMVC_DAL.Models;
using DemoASPMVC_DAL.Models.Form;
using System.Data.SqlClient;

namespace DemoASPMVC_DAL.Services
{
    public class GameDBService : IGameService
    {
        private readonly string connectionString;

        private readonly SqlConnection _connection;

        //public GameDBService(IConfiguration config)
        //{
        //    connectionString = config.GetConnectionString("default");
        //    _connection = new SqlConnection(connectionString);
        //}

        public GameDBService(SqlConnection connection)
        {
            _connection = connection;
        }

        protected Game Mapper(SqlDataReader reader)
        {
            return new Game
            {
                Id = (int)reader["Id"],
                Title = (string)reader["Title"],
                Description = (string)reader["Description"],
                IdGenre = (int)reader["IdGenre"]
            };
        }


        public bool Create(GameForm game)
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                string sql = "INSERT INTO Game (Title, Description, IdGenre) " +
                    "VALUES(@title, @desc, @genre)";
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("title", game.Title);
                cmd.Parameters.AddWithValue("desc", game.Description);
                cmd.Parameters.AddWithValue("genre", game.GenreId);

                _connection.Open();
                return cmd.ExecuteNonQuery() == 1;
            }
        }

        public bool Delete(int id)
        {
            // Delete game from fav table
            using(SqlCommand cmd = _connection.CreateCommand()) {

                cmd.CommandText = "Delete from Favoris where IdGame = @id";

                cmd.Parameters.AddWithValue( "id", id );

                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }

            // Then delete game
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                string sql = "DELETE FROM Game WHERE Id = @id";
                cmd.CommandText = sql;

                cmd.Parameters.AddWithValue("id", id);

                _connection.Open();
                return cmd.ExecuteNonQuery() == 1;
            }
        }

        public Game GetById(int id)
        {
            Game game = null;
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Game WHERE Id = @id";
                cmd.Parameters.AddWithValue("id", id);

                _connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) game = Mapper(reader);
                }
                _connection.Close();
            }
            return game;
        }

        public IEnumerable<Game> GetGames()
        {
            List<Game> game = new List<Game>();
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Game";

                _connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        game.Add(Mapper(reader));
                    }
                }
                _connection.Close();
            }
            return game;
        }

        public IEnumerable<Game> GetByUserId(int userId)
        {
            List<Game> list = new List<Game>();

            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Game g JOIN Favoris f ON g.Id = f.IdGame " +
                    "WHERE f.IdUser = @id";
                cmd.Parameters.AddWithValue("id", userId);
                _connection.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(Mapper(r));
                    }
                }
                _connection.Close();
            }
            return list;

        }

        public void AddFavorite(int idUser, int idGame)
        {
            using(SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO Favoris (IdUser, IdGame) VALUES (@idu, @idg)";
                cmd.Parameters.AddWithValue("idg", idGame);
                cmd.Parameters.AddWithValue("idu", idUser);

                _connection.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                } catch (SqlException ex)
                {
                    throw ex;
                }
                _connection.Close();
            }
        }
    }
}
