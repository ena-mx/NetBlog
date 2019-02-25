namespace NetBlog.Queries.Blogging
{
    using NetBlog.Requests;

    public class BlogEntryTextModel : BlogEntryModel
    {
        public int BlogEntryId { get; set; }
        public string TextEntry { get; set; }

        public AddUpdateBlogEntry MapToAddUpdateBlogEntryRequest() => new AddUpdateBlogEntry
        {
            KeyWordId = KeyWordId,
            MinutesToRead = MinutesToRead,
            TextEntry = TextEntry,
            TextIntro = TextIntro,
            Title = Title,
            UriKey = UriKey
        };
    }
}
