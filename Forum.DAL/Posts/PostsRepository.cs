using Forum.Common.Posts;
using Forum.DAL.Base;
using System;
using System.Data.SqlClient;

namespace Forum.DAL.Posts
{
    public class PostsRepository : IPostsRepository
    {
        #region locals
        private IRepositoryConfigs _configs;

        #endregion locals

        #region ctor
        public PostsRepository(IRepositoryConfigs configs)
        {
            this._configs = configs;
        }

        #endregion ctor

        #region methods
        public PostDTO Get(Guid id)
        {
            var result = new PostDTO();
            using(var sqlConnection = new SqlConnection(_configs.ConnectionString))
            {
                var command = new SqlCommand($"SELECT * FROM POSTS WHERE Posts.Id = '{id}'", sqlConnection);
                sqlConnection.Open();
                var reader = command.ExecuteReader();
                reader.Read();
                if (!reader.HasRows) return null;

                result.Id = (Guid)reader["Id"];
                result.Text = (string)reader["Text"];
                result.Name = (string)reader["Name"];
                result.CreateDate = (DateTime)reader["CreateDate"];
                result.UpdateDate = reader["UpdateDate"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(reader["UpdateDate"]);
                result.UserId = (Guid)reader["UserId"];
                result.ThreadId = (Guid)reader["ThreadId"];
                    
            }
            return result;
        }

        public ThreadPostsViewModel GetThreadPosts(Guid threadId, int page, bool latestFirst)
        {
            var result = new ThreadPostsViewModel();
            result.Posts.CurrentPage = page;
            result.Posts.ShowPerPage = _configs.PagingCount;
            using (var sqlConnection = new SqlConnection(_configs.ConnectionString))
            {
               
                sqlConnection.Open();
                result.Posts.TotalCount = GetCountOfPosts(threadId, sqlConnection);
                var threadDetails = GetThreadDetails(threadId, sqlConnection);
                result.ThreadText = threadDetails.Item1;
                result.Closed = threadDetails.Item2;

                var command = new SqlCommand($@"Select P.Id, P.[Name],P.[Text],U.Nickname, U.RegistrationDate, U.City, U.Country,
                                                (select count(*) from Posts where Posts.UserId = U.Id) as 'TotalPosts'
                                                 from Posts P
                                                JOIN Users U ON U.Id = P.UserId AND 
                                                P.ThreadId = '{threadId}'
                                                ORDER BY CreateDate {(latestFirst ? "DESC" : "ASC")} 
                                                OFFSET {(page-1)*_configs.PagingCount} ROWS
                                                FETCH NEXT {_configs.PagingCount} ROWS ONLY", sqlConnection);

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Posts.Items.Add(new PostViewModel
                    {
                        PostId = (Guid)reader["Id"],
                        PostName = (string)reader["Name"],
                        PostText = (string)reader["Text"],
                        UserNickname = (string)reader["Nickname"],
                        UserRegistrationDate = (DateTime)reader["RegistrationDate"],
                        UserCity = (string)reader["City"],
                        UserCountry = (string)reader["Country"],
                        UserTotalPosts =(int)reader["TotalPosts"]
                    });
                }

            }

            return result;
        }

        public Guid InsertOrEditPost(PostDTO post)
        {
            
            using (var sqlConnection = new SqlConnection(_configs.ConnectionString))
            {
                var command = new SqlCommand("sp_InsertOrEditPost", sqlConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                var idParam = new SqlParameter
                {
                    ParameterName = "@id",
                    SqlDbType = System.Data.SqlDbType.UniqueIdentifier,
                    IsNullable = true,
                    Value = (object)post.Id ?? DBNull.Value
                };
                var textParam = new SqlParameter
                {
                    ParameterName = "@text",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = post.Text
                };
                var cDateParam = new SqlParameter
                {
                    ParameterName = "@createdate",
                    SqlDbType = System.Data.SqlDbType.DateTime,
                    Value = post.CreateDate
                };
                var uDateParam = new SqlParameter
                {
                    ParameterName = "@updatedate",
                    SqlDbType = System.Data.SqlDbType.DateTime,
                    IsNullable = true,
                    Value = (object)post.UpdateDate ?? DBNull.Value
                };
                var threadIdParam = new SqlParameter
                {
                    ParameterName = "@threadId",
                    SqlDbType = System.Data.SqlDbType.UniqueIdentifier,
                    Value = post.ThreadId
                };
                var userIdParam = new SqlParameter
                {
                    ParameterName = "@userId",
                    SqlDbType = System.Data.SqlDbType.UniqueIdentifier,
                    Value = post.UserId
                };
                command.Parameters.Add(idParam);
                command.Parameters.Add(textParam);
                command.Parameters.Add(cDateParam);
                command.Parameters.Add(uDateParam);
                command.Parameters.Add(threadIdParam);
                command.Parameters.Add(userIdParam);

                sqlConnection.Open();
                post.Id =(Guid)command.ExecuteScalar();
            }

            return post.Id.Value;
        }

        public void Remove(Guid id)
        {
            using (var sqlConnection = new SqlConnection(_configs.ConnectionString))
            {
                var command = new SqlCommand($"DELETE FROM POSTS WHERE Posts.Id = '{id}'", sqlConnection);
                sqlConnection.Open();
                var reader = command.ExecuteNonQuery();

            }
        }
        #endregion methods

        #region private
        private int GetCountOfPosts(Guid threadId, SqlConnection connection)
        {
            var command = new SqlCommand($"SELECT COUNT(*) FROM Posts where ThreadId = '{threadId}'", connection);
            var result = (int)command.ExecuteScalar();

            return result;
        }

        private Tuple<string, bool> GetThreadDetails(Guid threadId, SqlConnection connection)
        {
            var command = new SqlCommand($"SELECT [Description], [Closed] FROM Threads where Id = '{threadId}'", connection);
            var reader = command.ExecuteReader();
            reader.Read();
            var desc = (string)reader["Description"];
            var closed = (bool)reader["Closed"];
            reader.Close();
            return new Tuple<string, bool>(desc, closed);
        }

        #endregion private
    }
}
