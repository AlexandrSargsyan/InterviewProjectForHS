namespace Forum.DAL.Base
{
    public class RepositoryConfigs : IRepositoryConfigs
    {
        #region locals
        private string _connectionString { get; set; }

        #endregion locals

        #region ctor
        public RepositoryConfigs(string connectionString)
        {
            _connectionString = connectionString;
        }

        #endregion ctor

        #region properties

        public string ConnectionString => _connectionString;

        public int PagingCount => 20;

        #endregion properties

    }
}
