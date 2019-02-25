namespace NetBlog.Queries.Blogging
{
    using System;

    public class BlogEntryModel
    {
        public string UriKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Title { get; set; }
        public int MinutesToRead { get; set; }
        public string TextIntro { get; set; }
        public int KeyWordId { get; set; }
        public string Keyword { get; set; }
        public int VisitCount { get; set; }
    }
}
