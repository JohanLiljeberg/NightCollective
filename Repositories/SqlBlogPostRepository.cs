using Microsoft.EntityFrameworkCore;
using Night.Data;
using Night.Models;

namespace Night.Repositories;

public class SqlBlogPostRepository(AppDbContext dbContext) : IBlogPostRepository
{
    public async Task<IReadOnlyCollection<BlogPost>> GetAllAsync()
    {
        return await dbContext.BlogPosts
            .AsNoTracking()
            .OrderByDescending(bp => bp.CreatedAt)
            .ToListAsync();
    }

    public async Task<IReadOnlyCollection<BlogPost>> GetLatestPublishedAsync(int count)
    {
        return await dbContext.BlogPosts
            .AsNoTracking()
            .Where(bp => bp.IsPublished)
            .OrderByDescending(bp => bp.CreatedAt)
            .Take(count)
            .ToListAsync();
    }

    public async Task<BlogPost?> GetByIdAsync(int id)
    {
        return await dbContext.BlogPosts
            .AsNoTracking()
            .FirstOrDefaultAsync(bp => bp.Id == id);
    }

    public async Task AddAsync(BlogPost blogPost)
    {
        await dbContext.BlogPosts.AddAsync(blogPost);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(BlogPost blogPost)
    {
        dbContext.BlogPosts.Update(blogPost);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var blogPost = await dbContext.BlogPosts.FindAsync(id);
        if (blogPost is null)
        {
            return;
        }

        dbContext.BlogPosts.Remove(blogPost);
        await dbContext.SaveChangesAsync();
    }
}
