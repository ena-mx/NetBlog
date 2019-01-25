namespace NetBlog.Queries.Sql.Blogging
{
    using EnaBricks.DataBricks;
    using EnaBricks.Generics;
    using NetBlog.Queries.Blogging;
    using NetBlog.Queries.Common;
    using System;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Threading.Tasks;

    public sealed class SqlBlogEntryQueryComponent : BlogEntryQueryComponent
    {
        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["BlogDB"].ConnectionString;
        private readonly string _cmdTxtBlogsPage = "[dbo].[sp_BlogsPage]";
        private readonly string _cmdTxtCustomer = "[dbo].[sp_Blog]";
        private readonly string _rootName = "GenericPage";

        public override async Task<Option<BlogEntryTextModel>> BlogAsync(string uriKey)
        {
            using (SqlDeserializerComponent<BlogEntryTextModel> component = new SqlDeserializerComponent<BlogEntryTextModel>(
               _connectionString,
               _cmdTxtCustomer,
               new SqlParameter[]
               {
                    new SqlParameter("@uriKey", System.Data.SqlDbType.UniqueIdentifier)
                    {
                        Value = uriKey
                    }
               }))
            {
                try
                {
                    BlogEntryTextModel model = await component.ExecuteStoreProcedureAndDeserialize();
                    if (model == null)
                    {
                        return Option<BlogEntryTextModel>.None();
                    }
                    return Option<BlogEntryTextModel>.Some(model);
                }
                catch (InvalidOperationException)
                {
                    return Option<BlogEntryTextModel>.None();
                }
            }
        }

        public override async Task<Option<GenericPage<BlogEntryModel>>> BlogsAsync(int offset, int limit)
        {
            using (SqlDeserializerComponent<GenericPage<BlogEntryModel>> component = new SqlDeserializerComponent<GenericPage<BlogEntryModel>>(
               _connectionString,
               _cmdTxtBlogsPage,
               _rootName,
               new SqlParameter[]
               {
                    new SqlParameter("@offset", System.Data.SqlDbType.Int)
                    {
                        Value = offset
                    },
                    new SqlParameter("@limit", System.Data.SqlDbType.Int)
                    {
                        Value = limit
                    }
               }))
            {
                try
                {
                    var model = await component.ExecuteStoreProcedureAndDeserialize();
                    if (model == null)
                    {
                        return Option<GenericPage<BlogEntryModel>>.None();
                    }
                    return Option<GenericPage<BlogEntryModel>>.Some(model);
                }
                catch (InvalidOperationException)
                {
                    return Option<GenericPage<BlogEntryModel>>.None();
                }
            }
        }
    }
}
