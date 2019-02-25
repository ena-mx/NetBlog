namespace NetBlog.Domain.Sql.Blogging.Component
{
    using EnaBricks.DataBricks;
    using EnaBricks.Generics;
    using NetBlog.Domain.Blogging.Component;
    using NetBlog.Domain.Sql.Blogging.Shared;
    using System;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Threading.Tasks;

    public sealed class SqlBlogEntryComponentFactory : BlogEntryComponentFactory
    {
        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["BlogDB"].ConnectionString;
        private static readonly string _existsBlog = @"
            IF EXISTS(SELECT [BlogEntryId] FROM [dbo].[BlogEntry] WITH(NOLOCK) WHERE [BlogEntryId] = @blogEntryId)
            BEGIN
	            SELECT 1
            END
            ELSE
            BEGIN
	            SELECT 0
            END
        ";

        public override async Task<Option<BlogEntryComponent>> LoadComponentAsynd(int id)
        {
            SqlCommandHelper commandHelper = new SqlCommandHelper(_connectionString);
            SqlParameter[] parameters = new SqlParameter[1]
            {
                new SqlParameter("@blogEntryId", System.Data.SqlDbType.Int)
                {
                    Value = id
                }
            };

            bool exists = Convert.ToBoolean(await commandHelper.ExecuteScalarAsync(_existsBlog, false, parameters));

            commandHelper = null;
            parameters = null;

            if (!exists)
                return Option<BlogEntryComponent>.None();

            return Option<BlogEntryComponent>.Some(new BlogEntryComponent(id, new SqlBlogEntryStorage()));
        }

        public override BlogEntryComponent New()
        {
            return new BlogEntryComponent(new SqlBlogEntryStorage());
        }
    }
}
