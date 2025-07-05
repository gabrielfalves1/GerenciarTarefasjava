using TaskCatalogDapper.Models;

namespace TaskCatalogDapper.Interfaces
{
    public interface ITaskRepository
    {
        public Task<IEnumerable<TaskItem>> GetAllAsync();
        public Task<TaskItem?> GetByIdAsync(int id);
        public Task CreateAsync(TaskItem task);
        public Task UpdateAsync(TaskItem task);
        public Task DeleteAsync(int id);
    }
}
