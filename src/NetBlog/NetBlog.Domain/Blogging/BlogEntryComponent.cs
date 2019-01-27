namespace NetBlog.Domain.Blogging
{
    using System;
    using System.Threading.Tasks;

    public sealed class BlogEntryComponent
    {
        private readonly int _blogEntryId;

        public int BlogEntryId => _blogEntryId;

        public BlogEntryComponent(int blogEntryId)
        {
            if (_blogEntryId < 0)
                throw new ArgumentException(nameof(blogEntryId));
            _blogEntryId = blogEntryId;
        }

        //public Task AddLogAsync(string remoteIpAddress);
    }

    public abstract class BlogEntryState
    {
        public abstract Task AddLogAsync(string remoteIpAddress);
    }

    public abstract class BlogEntryStorage
    {
        public abstract Task AddLogAsync(int blogEntryId, string remoteIpAddress);
    }
}
