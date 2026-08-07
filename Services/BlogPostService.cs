using Night.Models;
using Night.Repositories;
using Night.ViewModels;

namespace Night.Services;

public class BlogPostService(IBlogPostRepository blogPostRepository, IImageService imageService) : IBlogPostService
{
    public async Task<IReadOnlyCollection<BlogPost>> GetAllBlogPostsAsync()
    {
        return await blogPostRepository.GetAllAsync();
    }

    public async Task<IReadOnlyCollection<BlogPost>> GetLatestPublishedAsync(int count)
    {
        return await blogPostRepository.GetLatestPublishedAsync(count);
    }

    public async Task<BlogPost?> GetBlogPostAsync(int id)
    {
        return await blogPostRepository.GetByIdAsync(id);
    }

    public Task<BlogPostFormViewModel> GetCreateBlogPostFormAsync()
    {
        return Task.FromResult(new BlogPostFormViewModel());
    }

    public async Task<BlogPostFormViewModel?> GetBlogPostForEditAsync(int id)
    {
        var blogPost = await blogPostRepository.GetByIdAsync(id);

        return blogPost is null ? null : MapForm(blogPost);
    }

    public async Task CreateBlogPostAsync(BlogPostFormViewModel viewModel)
    {
        await blogPostRepository.AddAsync(await MapEntity(viewModel));
    }

    public async Task UpdateBlogPostAsync(BlogPostFormViewModel viewModel)
    {
        var existingPost = await blogPostRepository.GetByIdAsync(viewModel.Id);
        if (existingPost is null)
        {
            return;
        }

        var updatedPost = await MapEntity(viewModel, existingPost);
        updatedPost.UpdatedAt = DateTime.UtcNow;
        await blogPostRepository.UpdateAsync(updatedPost);
    }

    public async Task DeleteBlogPostAsync(int id)
    {
        await blogPostRepository.DeleteAsync(id);
    }

    private static BlogPostFormViewModel MapForm(BlogPost blogPost)
    {
        return new BlogPostFormViewModel
        {
            Id = blogPost.Id,
            Title = blogPost.Title,
            Content = blogPost.Content,
            ExternalLink = blogPost.ExternalLink,
            ImageUrl = blogPost.ImageMediumUrl ?? blogPost.ImageSmallUrl ?? blogPost.ImageLargeUrl,
            IsPublished = blogPost.IsPublished
        };
    }

    private async Task<BlogPost> MapEntity(BlogPostFormViewModel viewModel, BlogPost? existing = null)
    {
        string? smallUrl = existing?.ImageSmallUrl;
        string? mediumUrl = existing?.ImageMediumUrl;
        string? largeUrl = existing?.ImageLargeUrl;

        if (viewModel.ImageFile is not null && viewModel.ImageFile.Length > 0)
        {
            // File upload takes precedence
            var sizes = await imageService.UploadImageAsync(viewModel.ImageFile, ImageType.BlogPosts);
            if (sizes is not null)
            {
                smallUrl = sizes.SmallUrl;
                mediumUrl = sizes.MediumUrl;
                largeUrl = sizes.LargeUrl;
            }
        }
        else if (!string.IsNullOrWhiteSpace(viewModel.ImageUrl) && viewModel.ImageUrl != existing?.ImageMediumUrl)
        {
            // Download URL and process into 3 sizes
            var sizes = await imageService.DownloadAndProcessUrlAsync(viewModel.ImageUrl, ImageType.BlogPosts);
            if (sizes is not null)
            {
                smallUrl = sizes.SmallUrl;
                mediumUrl = sizes.MediumUrl;
                largeUrl = sizes.LargeUrl;
            }
        }

        return new BlogPost
        {
            Id = viewModel.Id,
            Title = viewModel.Title,
            Content = viewModel.Content,
            ExternalLink = viewModel.ExternalLink,
            ImageSmallUrl = smallUrl,
            ImageMediumUrl = mediumUrl,
            ImageLargeUrl = largeUrl,
            IsPublished = viewModel.IsPublished,
            CreatedAt = existing?.CreatedAt ?? DateTime.UtcNow
        };
    }
}
