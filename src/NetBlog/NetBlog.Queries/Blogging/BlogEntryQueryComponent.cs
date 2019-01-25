namespace NetBlog.Queries.Blogging
{
    using EnaBricks.Generics;
    using NetBlog.Queries.Common;
    using System.Threading.Tasks;

    public abstract class BlogEntryQueryComponent
    {
        public abstract Task<Option<GenericPage<BlogEntryModel>>> BlogsAsync(int offset, int limit);
        public abstract Task<Option<BlogEntryTextModel>> BlogAsync(string uriKey);
    }
}
