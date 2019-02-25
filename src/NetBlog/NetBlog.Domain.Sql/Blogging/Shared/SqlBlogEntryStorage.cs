namespace NetBlog.Domain.Sql.Blogging.Shared
{
    using EnaBricks.DataBricks;
    using EnaBricks.Generics;
    using NetBlog.Domain.Blogging.Storage;
    using System;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Threading.Tasks;

    public sealed class SqlBlogEntryStorage : BlogEntryStorage
    {
        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["BlogDB"].ConnectionString;

        #region Commands

        private static readonly string _addLogCmdText = @"
            INSERT INTO [dbo].[BlogEntryLog]
            (BlogEntryId, RemoteIpAddress, [Date])
            VALUES
            (@blogEntryId, @remoteIpAddress, GETDATE())
        ";

        private static readonly string _createCmdText = @"
            INSERT INTO [dbo].[BlogEntry]
            (KeyWordId, UriKey, CreatedDate, Title, MinutesToRead, TextIntro, TextEntry)
            VALUES
            (@keywordId, @uriKey, GETDATE(), @title, @minutesToRead, @textIntro, @textEntry)

            SELECT @@IDENTITY
        ";

        private static readonly string _existsBlogUriCmdText = @"
            IF EXISTS(SELECT [BlogEntryId] FROM [dbo].[BlogEntry] WITH(NOLOCK) WHERE [BlogEntryId] <> @blogEntryId and [UriKey] = @uriKey)
            BEGIN
	            SELECT 1
            END
            ELSE
            BEGIN
	            SELECT 0
            END
        ";

        private static readonly string _updateCmdText = @"
            UPDATE [dbo].[BlogEntry]
            SET 
				KeyWordId = @keywordId, 
				UriKey = @uriKey,
				Title = @title, 
				MinutesToRead = @minutesToRead, 
				TextIntro = @textIntro, 
				TextEntry = @textEntry
            WHERE [BlogEntryId] = @blogEntryId
        ";

        #endregion

        public override async Task AddLogAsync(int blogEntryId, string remoteIpAddress)
        {
            SqlCommandHelper commandHelper = new SqlCommandHelper(_connectionString);
            SqlParameter[] parameters = new SqlParameter[2]
            {
                new SqlParameter("@blogEntryId", System.Data.SqlDbType.Int)
                {
                    Value = blogEntryId
                },
                new SqlParameter("@remoteIpAddress", System.Data.SqlDbType.VarChar)
                {
                    Value = remoteIpAddress
                }
            };
            await commandHelper.ExecuteNonQueryAsync(_addLogCmdText, false, parameters);
        }

        public override async Task<Option<int>> Create(string uriKey, string title, int minutesToRead, string textIntro, string textEntry, int keywordId)
        {
            SqlCommandHelper commandHelper = new SqlCommandHelper(_connectionString);
            SqlParameter[] parameters = new SqlParameter[6]
            {
                new SqlParameter("@uriKey", System.Data.SqlDbType.VarChar)
                {
                    Value = uriKey
                },
                new SqlParameter("@title", System.Data.SqlDbType.VarChar)
                {
                    Value = title
                },
                new SqlParameter("@minutesToRead", System.Data.SqlDbType.Int)
                {
                    Value = minutesToRead
                },
                new SqlParameter("@textIntro", System.Data.SqlDbType.VarChar)
                {
                    Value = textIntro
                },
                new SqlParameter("@textEntry", System.Data.SqlDbType.VarChar)
                {
                    Value = textEntry
                },
                new SqlParameter("@keywordId", System.Data.SqlDbType.Int)
                {
                    Value = keywordId
                }
            };
            try
            {
                return Option<int>.Some(Convert.ToInt32(await commandHelper.ExecuteScalarAsync(_createCmdText, false, parameters)));
            }
            catch(Exception ex)
            {
            }

            return Option<int>.None();
        }

        public override async Task<bool> ExistsBlogUriKey(int blogEntryId, string uriKey)
        {
            SqlCommandHelper commandHelper = new SqlCommandHelper(_connectionString);
            SqlParameter[] parameters = new SqlParameter[2]
            {
                new SqlParameter("@blogEntryId", System.Data.SqlDbType.Int)
                {
                    Value = blogEntryId
                },
                new SqlParameter("@uriKey", System.Data.SqlDbType.VarChar)
                {
                    Value = uriKey
                }
            };
            return Convert.ToBoolean(await commandHelper.ExecuteScalarAsync(_existsBlogUriCmdText, false, parameters));
        }

        public override async Task<bool> Update(int blogEntryId, string uriKey, string title, int minutesToRead, string textIntro, string textEntry, int keywordId)
        {
            SqlCommandHelper commandHelper = new SqlCommandHelper(_connectionString);
            SqlParameter[] parameters = new SqlParameter[7]
            {
                new SqlParameter("@blogEntryId", System.Data.SqlDbType.Int)
                {
                    Value = blogEntryId
                },
                new SqlParameter("@uriKey", System.Data.SqlDbType.VarChar)
                {
                    Value = uriKey
                },
                new SqlParameter("@title", System.Data.SqlDbType.VarChar)
                {
                    Value = title
                },
                new SqlParameter("@minutesToRead", System.Data.SqlDbType.Int)
                {
                    Value = minutesToRead
                },
                new SqlParameter("@textIntro", System.Data.SqlDbType.VarChar)
                {
                    Value = textIntro
                },
                new SqlParameter("@textEntry", System.Data.SqlDbType.VarChar)
                {
                    Value = textEntry
                },
                new SqlParameter("@keywordId", System.Data.SqlDbType.Int)
                {
                    Value = keywordId
                }
            };
            try
            {
                return await commandHelper.ExecuteNonQueryAsync(_updateCmdText, false, parameters) > 0;
            }
            catch
            {
            }

            return false;
        }
    }
}
