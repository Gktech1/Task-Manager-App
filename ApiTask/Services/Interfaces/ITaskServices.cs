using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task = ApiTask.Models.Task;

namespace ApiTask.Services.Interfaces
{
    public interface ITaskServices
    {
        IQueryable<Task> Tasks { get; }
        Task<List<Task>> PaginatedResult(int size, int page);
        Task<string> AddTask(Task entity);
        Task<Task> GetTaskById(string id);
        
    }
}
