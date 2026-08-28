using Night.Models;

namespace Night.Repositories;

public interface IBlogPostRepository
{
    Task<IReadOnlyCollection<BlogPost>> GetAllAsync();

    Task<IReadOnlyCollection<BlogPost>> GetLatestPublishedAsync(int count);

    Task<BlogPost?> GetByIdAsync(int id);

    Task AddAsync(BlogPost blogPost);

    Task UpdateAsync(BlogPost blogPost);

    Task DeleteAsync(int id);
}
