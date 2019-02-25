namespace NetBlog.Requests
{
    using System.ComponentModel.DataAnnotations;
    using Resources;

    public class AddUpdateBlogEntry
    {
        [Display(Name = nameof(BlogModelResources.AddUpdateBlogEntry_Keyword), ResourceType = typeof(BlogModelResources))]
        [Required]
        [Range(1, double.MaxValue)]
        public int KeyWordId { get; set; }

        [Display(Name = nameof(BlogModelResources.AddUpdateBlogEntry_UriKey), ResourceType = typeof(BlogModelResources))]
        [Required(AllowEmptyStrings = false)]
        [StringLength(200, MinimumLength = 1)]
        public string UriKey { get; set; }

        [Display(Name = nameof(BlogModelResources.AddUpdateBlogEntry_Title), ResourceType = typeof(BlogModelResources))]
        [Required(AllowEmptyStrings = false)]
        [StringLength(50, MinimumLength = 1)]
        public string Title { get; set; }

        [Display(Name = nameof(BlogModelResources.MinutesToRead), ResourceType = typeof(BlogModelResources))]
        [Required(AllowEmptyStrings = false)]
        [Range(1, double.MaxValue)]
        public int MinutesToRead { get; set; }

        [Display(Name = nameof(BlogModelResources.AddUpdateBlogEntry_TextIntro), ResourceType = typeof(BlogModelResources))]
        [Required(AllowEmptyStrings = false)]
        [StringLength(256, MinimumLength = 1)]
        public string TextIntro { get; set; }

        [Display(Name = nameof(BlogModelResources.AddUpdateBlogEntry_Entry), ResourceType = typeof(BlogModelResources))]
        [Required(AllowEmptyStrings = false)]
        [MinLength(1)]
        public string TextEntry { get; set; }
    }
}