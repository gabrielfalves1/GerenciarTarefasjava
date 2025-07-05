using Dapper;
using TaskCatalogDapper.Database;
using TaskCatalogDapper.Interfaces;
using TaskCatalogDapper.Models;

namespace TaskCatalogDapper.Repositories
{
    public class TaskRepository: ITaskRepository
    {
        private readonly DapperContext _context;

        public TaskRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            var query = "SELECT * FROM Tasks ORDER BY Priority DESC";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<TaskItem>(query);
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM Tasks WHERE Id = @Id";
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<TaskItem>(query, new { Id = id });
        }

        public async Task CreateAsync(TaskItem task)
        {
            var query = "INSERT INTO Tasks (Title, Description, Priority) VALUES (@Title, @Description, @Priority)";
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, task);
        }

        public async Task UpdateAsync(TaskItem task)
        {
            var query = "UPDATE Tasks SET Title = @Title, Description = @Description, Priority = @Priority WHERE ID = @Id";
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, task);
        }

        public async Task DeleteAsync(int id)
        {
            var query = "DELETE FROM Tasks WHERE Id = @Id";
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, new { Id = id });
        }
    }
}
