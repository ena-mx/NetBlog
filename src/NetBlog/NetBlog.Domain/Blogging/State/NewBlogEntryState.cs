namespace NetBlog.Domain.Blogging.State
{
    using EnaBricks.Generics;
    using NetBlog.Domain.Blogging.Component;
    using NetBlog.Domain.Blogging.Storage;
    using NetBlog.SharedFramework;
    using System.Linq;
    using System.Threading.Tasks;

    public sealed class NewBlogEntryState : BlogEntryState
    {
        public NewBlogEntryState(BlogEntryStorage blogEntryStorage, BlogEntryComponent blogEntryComponent) : base(blogEntryStorage, blogEntryComponent)
        {
        }

        public override async Task<WorkflowResult> Create(string uriKey, string title, int minutesToRead, string textIntro, string textEntry, int keywordId)
        {
            if (await _blogEntryStorage.ExistsBlogUriKey(_blogEntryComponent.BlogEntryId, uriKey))
                return _uriKeyDuplicateError;

            Option<int> result = await _blogEntryStorage.Create(uriKey, title, minutesToRead, textIntro, textEntry, keywordId);

            if (!result.Any())
                return _storageError;

            int id = result.Single();
            _blogEntryComponent.UpdateId(id);
            _blogEntryComponent.BlogEntryState = new ExistingBlogEntryState(_blogEntryStorage, _blogEntryComponent);

            return _successResult;
        }
    }
}
