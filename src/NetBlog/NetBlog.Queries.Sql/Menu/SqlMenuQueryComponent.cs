namespace NetBlog.Queries.Sql.Menu
{
    using EnaBricks.DataBricks;
    using NetBlog.Queries.Menu;
    using System;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Threading.Tasks;

    public sealed class SqlMenuQueryComponent : MenuQueryComponent
    {
        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["BlogDB"].ConnectionString;
        private readonly string _cmdTxtMenuItems = @"
            SELECT CONVERT(XML, (
	            SELECT 
		            [Description], 
		            [FontAwasomeIcon], 
		            [Url], 
		            [IsLocal]
	            FROM [dbo].[MenuItem] WITH(NOLOCK)
	            ORDER BY [OrderNumber]
	            FOR XML PATH('MenuItem'), ELEMENTS, ROOT('MenuItems')
            )) AS 'XmlData'
        ";
        private readonly string _rootName = "MenuItems";

        public override async Task<MenuItem[]> GetMenuItems()
        {
            using (SqlDeserializerComponent<MenuItem[]> component = new SqlDeserializerComponent<MenuItem[]>(
               _connectionString,
               _cmdTxtMenuItems,
               _rootName,
               Array.Empty<SqlParameter>()))
            {
                try
                {
                    return await component.ExecuteCmdTextAndDeserialize() ?? Array.Empty<MenuItem>();
                }
                catch (InvalidOperationException)
                {
                    return Array.Empty<MenuItem>();
                }
            }
        }
    }
}