using Forum.Common.Base;
using Forum.Common.Threads;
using Forum.DAL.Base;
using System;
using System.Data.SqlClient;

namespace Forum.DAL.Threads
{
    public class ThreadsRepository : IThreadsRepository
    {
        #region locals
        private readonly IRepositoryConfigs _configs;
        #endregion locals

        #region ctor
        public ThreadsRepository(IRepositoryConfigs configs)
        {
            this._configs = configs;
        }

        #endregion ctor

        #region methods
        public void ChangeState(Guid id, bool state)
        {
            using (var sqlConnection = new SqlConnection(_configs.ConnectionString))
            {

                var command = new SqlCommand($@"UPDATE Threads SET Closed = '{(state ? 1 : 0)}' WHERE Id = '{id}'", sqlConnection);
                sqlConnection.Open();
                command.ExecuteNonQuery();
            }
        }

        public PagingResult<ThreadsGridViewModel> GetThreadsOfTopicGrid(Guid topicId, int page)
        {
            var result = new PagingResult<ThreadsGridViewModel>();

            using (var sqlConnection = new SqlConnection(_configs.ConnectionString))
            {

                var command = new SqlCommand(@"sp_GetThreadsOfTopicForMainGrid", sqlConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                var topicIdParam = new SqlParameter
                {
                    ParameterName = "@topicId",
                    Value = topicId,
                    SqlDbType = System.Data.SqlDbType.UniqueIdentifier
                };
                var pageParam = new SqlParameter
                {
                    ParameterName = "@page",
                    Value = page,
                    SqlDbType = System.Data.SqlDbType.Int
                };
                var countParam = new SqlParameter
                {
                    ParameterName = "@count",
                    Value = _configs.PagingCount,
                    SqlDbType = System.Data.SqlDbType.Int
                };
                var totalRowCountParam = new SqlParameter
                {
                    ParameterName = "@totalRowCount",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Direction = System.Data.ParameterDirection.Output
                };

                command.Parameters.Add(topicIdParam);
                command.Parameters.Add(pageParam);
                command.Parameters.Add(countParam);
                command.Parameters.Add(totalRowCountParam);
                sqlConnection.Open();
                var reader = command.ExecuteReader();

                result.CurrentPage = page;
                result.ShowPerPage = _configs.PagingCount;

                while (reader.Read())
                {
                    result.Items.Add(new ThreadsGridViewModel
                    {
                        ThreadId = (Guid)reader["ThreadId"],
                        ThreadName = (string)reader["Name"],
                        LastPostDate = reader["LastPostDate"] == DBNull.Value ? null :
                                                (DateTime?)Convert.ToDateTime(reader["LastPostDate"]),
                        TotalReplies = (int)reader["Replies"],
                        LastPostedBy = reader["LastPostedBy"] == DBNull.Value ? null :
                                                 (string)reader["LastPostedBy"],
                        Closed = (int)reader["Closed"] == 1,
                    });
                }
                reader.Close();
                result.TotalCount = (int)totalRowCountParam.Value;

            }

            return result;
        }

        public Guid InsertThread(ThreadDTO thread)
        {
            using (var sqlConnection = new SqlConnection(_configs.ConnectionString))
            {
                var command = new SqlCommand(@"sp_InsertThread", sqlConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var nameParam = new SqlParameter
                {
                    ParameterName = "@name",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = thread.Name,
                };

                var descParam = new SqlParameter
                {
                    ParameterName = "@description",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = thread.Description
                };

                var topicIdParam = new SqlParameter
                {
                    ParameterName = "@topicId",
                    SqlDbType = System.Data.SqlDbType.UniqueIdentifier,
                    Value = thread.TopicId
                };

                command.Parameters.Add(nameParam);
                command.Parameters.Add(descParam);
                command.Parameters.Add(topicIdParam);

                sqlConnection.Open();

                thread.Id = (Guid)command.ExecuteScalar();

            }
            return thread.Id;
        }

        public void Remove(Guid id)
        {
            using (var sqlConnection = new SqlConnection(_configs.ConnectionString))
            {
                var command = new SqlCommand($@"Delete from Threads where Id='{id}'", sqlConnection);

                sqlConnection.Open();

                command.ExecuteNonQuery();
            }
        }
        #endregion methods
    }

}