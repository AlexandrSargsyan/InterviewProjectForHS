using Forum.Common.Base;
using Forum.Common.Topics;
using Forum.DAL.Base;
using System;
using System.Data.SqlClient;

namespace Forum.DAL.Topics
{
    public class TopicsRepository : ITopicsRepository
    {
        #region locals
        private IRepositoryConfigs _configs;

        #endregion locals

        #region ctor
        public TopicsRepository(IRepositoryConfigs configs)
        {
            _configs = configs;
        }
        #endregion ctor

        #region methods
        public Guid Add(string name)
        {
            Guid id;
            using (var sqlConnection = new SqlConnection(_configs.ConnectionString))
            {
                var command = new SqlCommand(@"sp_InsertTopic", sqlConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                var nameParam = new SqlParameter
                {
                    ParameterName = "@name",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = name
                };
                command.Parameters.Add(nameParam);
                sqlConnection.Open();

                id = (Guid)command.ExecuteScalar();
            }

            return id;

        }

        public PagingResult<TopicGridViewModel> GetTopicsGrid(int page)
        {
            var result = new PagingResult<TopicGridViewModel>();

            using (var sqlConnection = new SqlConnection(_configs.ConnectionString))
            {

                var command = new SqlCommand(@"sp_GetTopicsForMainGrid", sqlConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
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

                command.Parameters.Add(pageParam);
                command.Parameters.Add(countParam);
                command.Parameters.Add(totalRowCountParam);
                sqlConnection.Open();
                var reader = command.ExecuteReader();

                result.CurrentPage = page;
                result.ShowPerPage = _configs.PagingCount;

                while (reader.Read())
                {
                    result.Items.Add(new TopicGridViewModel
                    {
                        TopicId = (Guid)reader["TopicId"],
                        TopicName = (string)reader["Name"],
                        LastPostDate = reader["PostDate"] == DBNull.Value ? null :
                                                (DateTime?)Convert.ToDateTime(reader["PostDate"]),
                        TotalReplies = (int)reader["Total"],
                        LastPostName = reader["PostName"] == DBNull.Value ? null :
                                                 (string)reader["PostName"]
                    });
                }
                reader.Close();
                result.TotalCount = (int)totalRowCountParam.Value;

            }

            return result;
        }

        public void Remove(Guid id)
        {
            using (var sqlConnection = new SqlConnection(_configs.ConnectionString))
            {

                var command = new SqlCommand($@"Delete From Topics Where Id = '{id}'", sqlConnection);
                sqlConnection.Open();

                command.ExecuteNonQuery();
            }
        }

        #endregion methods

    }
}
