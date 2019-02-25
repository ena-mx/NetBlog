namespace NetBlog.Domain.Blogging.State
{
    using NetBlog.Domain.Blogging.Component;
    using NetBlog.Domain.Blogging.Storage;
    using NetBlog.SharedFramework;
    using System;
    using System.Threading.Tasks;

    public abstract class BlogEntryState
    {
        #region Private members

        protected readonly BlogEntryStorage _blogEntryStorage;
        protected readonly BlogEntryComponent _blogEntryComponent;
        private static readonly WorkflowResult _invalidStateResult = new WorkflowResult(Resources.BlogModelResources.BlogState_InvalidStateError);
        protected static readonly WorkflowResult _successResult = new WorkflowResult();
        protected static readonly WorkflowResult _uriKeyDuplicateError = new WorkflowResult(Resources.BlogModelResources.BlogState_UriKeyDuplicateError);
        protected static readonly WorkflowResult _storageError = new WorkflowResult(Resources.BlogModelResources.BlogState_StorageError);

        #endregion 

        #region Constructor

        public BlogEntryState(BlogEntryStorage blogEntryStorage, BlogEntryComponent blogEntryComponent)
        {
            _blogEntryStorage = blogEntryStorage ?? throw new ArgumentNullException(nameof(blogEntryStorage));
            _blogEntryComponent = blogEntryComponent ?? throw new ArgumentNullException(nameof(blogEntryComponent));
        }

        #endregion Constructor

        #region Protocol

        public virtual Task AddLogAsync(int blogEntryId, string remoteIpAddress) => Task.FromResult(_invalidStateResult);

        public virtual Task<WorkflowResult> Update(
            int blogEntryId,
            string uriKey,
            string title,
            int minutesToRead,
            string textIntro,
            string textEntry,
            int keywordId
            ) => Task.FromResult(_invalidStateResult);

        public virtual Task<WorkflowResult> Create(
            string uriKey,
            string title,
            int minutesToRead,
            string textIntro,
            string textEntry,
            int keywordId
            ) => Task.FromResult(_invalidStateResult);

        #endregion Protocol
    }
}
