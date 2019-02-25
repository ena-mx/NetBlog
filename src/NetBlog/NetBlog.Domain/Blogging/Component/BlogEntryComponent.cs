namespace NetBlog.Domain.Blogging.Component
{
    using NetBlog.Domain.Blogging.State;
    using NetBlog.Domain.Blogging.Storage;
    using NetBlog.SharedFramework;
    using System;
    using System.Threading.Tasks;

    public sealed class BlogEntryComponent
    {
        #region Private members

        private int _blogEntryId;
        internal BlogEntryState BlogEntryState { get; set; }

        #endregion Private members

        #region Properties

        public int BlogEntryId => _blogEntryId;

        #endregion Properties

        #region Constructor

        public BlogEntryComponent(int blogEntryId, BlogEntryStorage blogEntryStorage)
        {
            if (_blogEntryId < 0)
                throw new ArgumentException(nameof(blogEntryId));
            _blogEntryId = blogEntryId;
            BlogEntryState = new ExistingBlogEntryState(blogEntryStorage, this);
        }

        public BlogEntryComponent(BlogEntryStorage blogEntryStorage)
        {
            BlogEntryState = new NewBlogEntryState(blogEntryStorage, this);
        }

        #endregion

        #region Protocol

        public async Task AddLogAsync(string remoteIpAddress) => await BlogEntryState.AddLogAsync(_blogEntryId, remoteIpAddress);

        public async Task<WorkflowResult> Create(
            string uriKey,
            string title,
            int minutesToRead,
            string textIntro,
            string textEntry,
            int keywordId
            ) => await BlogEntryState.Create(uriKey, title, minutesToRead, textIntro, textEntry, keywordId);

        public async Task<WorkflowResult> Update(
            string uriKey,
            string title,
            int minutesToRead,
            string textIntro,
            string textEntry,
            int keywordId
            ) => await BlogEntryState.Update(_blogEntryId, uriKey, title, minutesToRead, textIntro, textEntry, keywordId);

        internal void UpdateId(int id)
        {
            _blogEntryId = id;
        }

        #endregion Protocol
    }
}
