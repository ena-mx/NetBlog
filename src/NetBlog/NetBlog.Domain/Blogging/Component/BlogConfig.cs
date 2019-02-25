namespace NetBlog.Domain.Blogging.Component
{
    using EnaBricks.Generics;
    using System.Threading.Tasks;

    public class BlogConfig
    {
        private static readonly string _blogNameKey = @"BlogName";
        private static readonly string _gitHubKey = @"GitHub";
        private static readonly string _linkedInKey = @"linkedInUrl";
        private static readonly string _profilePicKey = @"ProfilePicUrl";
        private static readonly string _twitterKey = @"TwitterUrl";
        private static readonly string _whoIAmKey = @"WhoIAmText";
        private static readonly string _yourAcclaimKey = @"YourAcclaimUrl";

        public virtual Task<Option<string>> GetConfigValueAsync(string blogKey) => Task.FromResult(Option<string>.None());

        public async Task<Option<string>> GetBlogNameAsync() => await GetConfigValueAsync(_blogNameKey);
        public async Task<Option<string>> GetGitHubUrl() => await GetConfigValueAsync(_gitHubKey);
        public async Task<Option<string>> GetLinkedIdUrl() => await GetConfigValueAsync(_linkedInKey);
        public async Task<Option<string>> GetProfilePicUrl() => await GetConfigValueAsync(_profilePicKey);
        public async Task<Option<string>> GetTwitterUrl() => await GetConfigValueAsync(_twitterKey);
        public async Task<Option<string>> GetWhoIAmHtml() => await GetConfigValueAsync(_whoIAmKey);
        public async Task<Option<string>> GetYourAcclaimUrl() => await GetConfigValueAsync(_yourAcclaimKey);
    }
}
