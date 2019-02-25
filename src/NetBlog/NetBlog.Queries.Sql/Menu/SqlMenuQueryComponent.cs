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

        private readonly string _cmdTxtLeftMenuItems = @"
            SELECT CONVERT(XML, (
	            SELECT 
		            [Description], 
		            [FontAwasomeIcon], 
		            [Url], 
		            [IsLocal],
                    [IsSecured]
	            FROM [dbo].[MenuItem] WITH(NOLOCK)
				WHERE [IsMobileMenu] = 1
	            ORDER BY [OrderNumberMobile]
	            FOR XML PATH('MenuItem'), ELEMENTS, ROOT('MenuItems')
            )) AS 'XmlData'
        ";

        private readonly string _cmdTxtTitlebarMenuItems = @"
            SELECT CONVERT(XML, (
	            SELECT 
		            [Description], 
		            [FontAwasomeIcon] AS 'FontAwasomeIcon', 
		            [Url], 
		            [IsLocal],
                    [IsSecured]
	            FROM [dbo].[MenuItem] WITH(NOLOCK)
				WHERE [MenuItem].[IsTitleBar] = 1
	            ORDER BY [OrderNumber]
	            FOR XML PATH('MenuItem'), ELEMENTS, ROOT('MenuItems')
            )) AS 'XmlData'
        ";
        private readonly string _rootName = "MenuItems";

        public override async Task<MenuItem[]> GetLeftMenuItemsAsync()
        {
            using (SqlDeserializerComponent<MenuItem[]> component = new SqlDeserializerComponent<MenuItem[]>(
               _connectionString,
               _cmdTxtLeftMenuItems,
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

        public override async Task<MenuItem[]> GetTitlebarMenuItemsAsync()
        {
            using (SqlDeserializerComponent<MenuItem[]> component = new SqlDeserializerComponent<MenuItem[]>(
               _connectionString,
               _cmdTxtTitlebarMenuItems,
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