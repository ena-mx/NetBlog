namespace NetBlog.Queries.Menu
{
    using System.Threading.Tasks;

    public abstract class MenuQueryComponent
    {
        public abstract Task<MenuItem[]> GetLeftMenuItemsAsync();
        public abstract Task<MenuItem[]> GetTitlebarMenuItemsAsync();
    }
}