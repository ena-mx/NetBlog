namespace NetBlog.Domain.Blogging.Storage
{
    using EnaBricks.Generics;
    using System.Threading.Tasks;

    public abstract class BlogEntryStorage
    {
        public abstract Task AddLogAsync(int blogEntryId, string remoteIpAddress);

        public abstract Task<bool> ExistsBlogUriKey(int blogEntryId, string uriKey);

        public abstract Task<Option<int>> Create(
            string uriKey,
            string title,
            int minutesToRead,
            string textIntro,
            string textEntry,
            int keywordId
            );

        public abstract Task<bool> Update(
            int blogEntryId,
            string uriKey,
            string title,
            int minutesToRead,
            string textIntro,
            string textEntry,
            int keywordId
            );
    }
}
