using Night.Models;
using Night.ViewModels;

namespace Night.Services;

public interface IBlogPostService
{
    Task<IReadOnlyCollection<BlogPost>> GetAllBlogPostsAsync();

    Task<IReadOnlyCollection<BlogPost>> GetLatestPublishedAsync(int count);

    Task<BlogPost?> GetBlogPostAsync(int id);

    Task<BlogPostFormViewModel> GetCreateBlogPostFormAsync();

    Task<BlogPostFormViewModel?> GetBlogPostForEditAsync(int id);

    Task CreateBlogPostAsync(BlogPostFormViewModel viewModel);

    Task UpdateBlogPostAsync(BlogPostFormViewModel viewModel);

    Task DeleteBlogPostAsync(int id);
}
