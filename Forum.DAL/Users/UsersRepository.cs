using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Forum.Common.Users;
using Forum.DAL.Base;

namespace Forum.DAL.Users
{
    public class UsersRepository : IUsersRepository
    {
        #region locals
        private readonly IRepositoryConfigs _configs;
        public UsersRepository(IRepositoryConfigs configs)
        {
            _configs = configs;
        }

        #endregion locals

        #region methods
        public UserDTO Get(Guid userId)
        {
            var result = new UserDTO();
            using (var sqlConnection = new SqlConnection(_configs.ConnectionString))
            {
                sqlConnection.Open();
                var command = new SqlCommand($@"select * ,
                                                (select count(*) from Posts
                                                where UserId = Id) as 'PostCount'
                                                from Users
                                                where Id = '{userId}'", sqlConnection);
                var reader = command.ExecuteReader();
                reader.Read();
                if (!reader.HasRows) return null;
               
                 result = Read(reader);
            }

           
            

            return result;
        }


        public UserDTO Get(string nickname, string password)
        {
            var result = new UserDTO();
            using (var sqlConnection = new SqlConnection(_configs.ConnectionString))
            {
                sqlConnection.Open();
              
                var command = new SqlCommand($@"select * ,
                                                (select count(*) from Posts
                                                where UserId = Id) as 'PostCount'
                                                from Users
                                                where nickname = @nickname and password = @password", sqlConnection);
                var nickParam = new SqlParameter { ParameterName = "@nickname", SqlDbType = SqlDbType.NVarChar, Value = nickname };
                var passParam = new SqlParameter { ParameterName = "@password", SqlDbType = SqlDbType.NVarChar, Value = password };
                command.Parameters.Add(nickParam);
                command.Parameters.Add(passParam);
                var reader = command.ExecuteReader();
                reader.Read();
                if (!reader.HasRows) return null;

                result = Read(reader);
            }

            return result;
        }

        public UserDTO Get(string nickname)
        {
            var result = new UserDTO();
            using (var sqlConnection = new SqlConnection(_configs.ConnectionString))
            {
                sqlConnection.Open();
                var command = new SqlCommand($@"select * ,
                                                (select count(*) from Posts
                                                where UserId = Id) as 'PostCount'
                                                from Users
                                                where nickname = '{nickname}'", sqlConnection);
                var reader = command.ExecuteReader();
                reader.Read();
                if (!reader.HasRows) return null;

                result = Read(reader);
            }

            return result;
        }


        public IEnumerable<UserDTO> GetAll()
        {
            var users = new List<UserDTO>();
            using (var sqlConnection = new SqlConnection(_configs.ConnectionString))
            {
                sqlConnection.Open();
                var command = new SqlCommand($@"select * ,
                                                (select count(*) from Posts
                                                where UserId = Id) as 'PostCount'
                                                from Users", sqlConnection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(Read(reader));
                }
            }

            return users;
        }

        public Guid Insert(UserDTO user)
        {
            Guid id;
            using (var sqlConnection = new SqlConnection(_configs.ConnectionString))
            {
                sqlConnection.Open();
                var command = new SqlCommand("sp_InsertUser", sqlConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var nickParam = new SqlParameter
                {
                    ParameterName = "@nickname",
                    Value = user.Nickname,
                    SqlDbType = System.Data.SqlDbType.NVarChar
                };

                var passParam = new SqlParameter
                {
                    ParameterName = "@password",
                    Value = user.Password,
                    SqlDbType = System.Data.SqlDbType.NVarChar
                };

                var cityParam = new SqlParameter
                {
                    ParameterName = "@city",
                    Value = user.City,
                    SqlDbType = System.Data.SqlDbType.NVarChar
                };

                var countryParam = new SqlParameter
                {
                    ParameterName = "@country",
                    Value = user.Country,
                    SqlDbType = System.Data.SqlDbType.NVarChar
                };
                var typeParam = new SqlParameter
                {
                    ParameterName = "@type",
                    Value = (int)user.Type,
                    SqlDbType = System.Data.SqlDbType.Int
                };
                var regDateParam = new SqlParameter
                {
                    ParameterName = "@regDate",
                    Value = user.RegistrationDate,
                    SqlDbType = System.Data.SqlDbType.DateTime
                };

                command.Parameters.Add(nickParam);
                command.Parameters.Add(passParam);
                command.Parameters.Add(cityParam);
                command.Parameters.Add(countryParam);
                command.Parameters.Add(typeParam);
                command.Parameters.Add(regDateParam);

                id = (Guid)command.ExecuteScalar();
            }
            return id;
        }

        public bool Remove(Guid userId)
        {
            using (var sqlConnection = new SqlConnection(_configs.ConnectionString))
            {
                sqlConnection.Open();
                var command = new SqlCommand($@" delete from Users
                                                 where Id = '{userId}'", sqlConnection);


                var reader = command.ExecuteNonQuery();

               
            }
            return true;
        }
        #endregion methods

        #region private
        private UserDTO Read(SqlDataReader reader)
        {
            var result = new UserDTO
            {
                Id = (Guid)reader["Id"],
                Nickname = (string)reader["Nickname"],
                City = (string)reader["City"],
                Country = (string)reader["Country"],
                Type = (UserType)reader["Type"],
                RegistrationDate = (DateTime)reader["RegistrationDate"],
                PostCount = (int)reader["PostCount"]
            };

            return result;
        }
        #endregion methods
    }
}
