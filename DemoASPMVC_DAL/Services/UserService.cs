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
    public class UserService : BaseRepository<User>, IUserService
    {
        public UserService(IConfiguration config) : base(config)
        {
        }

        protected override User Mapper(IDataReader reader)
        {
            return new User
            {
                Id = (int)reader["Id"],
                Nickname = (string)reader["Nickname"],
                Email = (string)reader["Email"],
                RoleId = (int)reader["RoleId"],
                RoleName = ((int)reader["RoleId"] == 1) ? "User" : ((int)reader["RoleId"] == 2) ? "Modo" : "Admin"
            };
        }

        public User Login(string email, string pwd)
        {
            using (SqlConnection cnx = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = cnx.CreateCommand())
                {
                    cmd.CommandText = "UserLogin";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("pwd", pwd);
                    cnx.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        try
                        {
                            if (reader.Read())
                                return Mapper(reader);
                            throw new Exception("Erreur de connexion : Mot de passe ou Email invalide");
                        }
                        catch (SqlException ex)
                        {
                            throw ex;
                        }
                    }
                }
            }
        }

        public bool Register(string email, string pwd, string nickname)
        {
            using (SqlConnection cnx = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = cnx.CreateCommand())
                {
                    cmd.CommandText = "UserRegister";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("nickname", nickname);
                    cmd.Parameters.AddWithValue("password", pwd);

                    cnx.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }

        }
    }
}
