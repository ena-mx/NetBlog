namespace NetBlog.Domain.Blogging.Component
{
    using EnaBricks.Generics;
    using System.Threading.Tasks;

    public abstract class BlogEntryComponentFactory
    {
        public abstract BlogEntryComponent New();

        public abstract Task<Option<BlogEntryComponent>> LoadComponentAsynd(int id);
    }
}
