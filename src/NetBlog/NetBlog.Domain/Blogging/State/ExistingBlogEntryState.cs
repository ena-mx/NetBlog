namespace NetBlog.Domain.Blogging.State
{
    using NetBlog.Domain.Blogging.Component;
    using NetBlog.Domain.Blogging.Storage;
    using NetBlog.SharedFramework;
    using System.Threading.Tasks;

    public sealed class ExistingBlogEntryState : BlogEntryState
    {
        public ExistingBlogEntryState(BlogEntryStorage blogEntryStorage, BlogEntryComponent blogEntryComponent) : base(blogEntryStorage, blogEntryComponent)
        {
        }

        public override async Task<WorkflowResult> Update(int blogEntryId, string uriKey, string title, int minutesToRead, string textIntro, string textEntry, int keywordId)
        {
            if (await _blogEntryStorage.ExistsBlogUriKey(blogEntryId, uriKey))
                return _uriKeyDuplicateError;
            await _blogEntryStorage.Update(blogEntryId, uriKey, title, minutesToRead, textIntro, textEntry, keywordId);
            return _successResult;
        }

        public override async Task AddLogAsync(int blogEntryId, string remoteIpAddress)
        {
            await _blogEntryStorage.AddLogAsync(blogEntryId, remoteIpAddress);
        }
    }
}
