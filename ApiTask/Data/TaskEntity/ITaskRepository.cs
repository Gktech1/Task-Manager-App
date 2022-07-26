using System.Linq;
using System.Threading.Tasks;
using ApiTask.Models;
using Task = ApiTask.Models.Task;

namespace ApiTask.Services.Interfaces.TaskEntity
{
    public interface ITaskRepository
    { 
        Task<bool> AddTaskAsyncTask(Task entity);
        Task<Task> GetTaskByIdAsync(string id);
        Task<bool> DeleteTaskAsync(Task entity);
        Task<bool> UpdateTaskAsync (Task entity);    
        IQueryable<Task> GetAllTasksAsync(); // Lazy loading 
        Task <bool> Save();

    }
}
