namespace NetBlog.Domain.Sql.Blogging
{
    using EnaBricks.Generics;
    using NetBlog.Domain.Blogging.Component;
    using System.Collections.Concurrent;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Threading.Tasks;

    public sealed class SqlBlogConfig : BlogConfig
    {
        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["BlogDB"].ConnectionString;
        private static readonly string _cmdText = @"
            SELECT ISNULL([BlogKeyDescription], '') AS 'ParamValue' FROM [dbo].[BlogConfig] WITH(NOLOCK) WHERE [BlogKeyId] = @blogKeyId
        ";
        private readonly ConcurrentDictionary<string, Option<string>> _dictionary = new ConcurrentDictionary<string, Option<string>>();

        public override async Task<Option<string>> GetConfigValueAsync(string blogKey)
        {
            if (_dictionary.ContainsKey(blogKey))
                return _dictionary[blogKey];

            Option<string> blogKeyValue = await GetConfigValueInternalAsync(blogKey);
            _dictionary.TryAdd(blogKey, blogKeyValue);
            return blogKeyValue;
        }

        private async Task<Option<string>> GetConfigValueInternalAsync(string blogKey)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(_cmdText, connection))
                {
                    command.Parameters.AddRange(new SqlParameter[1]
                    {
                        new SqlParameter("@blogKeyId", System.Data.SqlDbType.VarChar)
                        {
                            Value = blogKey
                        }
                    });
                    await connection.OpenAsync();

                    string returnValue = (await command.ExecuteScalarAsync() as string) ?? string.Empty;

                    if (string.IsNullOrWhiteSpace(returnValue))
                        return Option<string>.None();

                    return Option<string>.Some(returnValue);
                }
            }
        }
    }
}
