using DemoASPMVC_DAL.Interface;
using DemoASPMVC_DAL.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoASPMVC_DAL.Services
{
    public class GenreService : BaseRepository<Genre>, IGenreService
    {
        public GenreService(IConfiguration config) : base(config)
        {
        }

        protected override Genre Mapper(IDataReader reader)
        {
            return new Genre
            {
                Id = (int)reader["Id"],
                Label = (string)reader["Label"]
            };
        }

        public bool Add(string genre)
        {
            using (SqlConnection cnx = new SqlConnection(_connectionString))
            {
                cnx.Open();
                using (SqlCommand cmd = cnx.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Genre VALUES (@genre)";
                    cmd.Parameters.AddWithValue("genre", genre);

                    return cmd.ExecuteNonQuery() == 1;
                }
            }
        }
    }
}
