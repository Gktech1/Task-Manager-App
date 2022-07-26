using System.Linq;
using System.Threading.Tasks;
using ApiTask.Data;
using Microsoft.EntityFrameworkCore;
using Task = ApiTask.Models.Task;

namespace ApiTask.Services.Interfaces.TaskEntity
{
    public class TaskRepository : ITaskRepository   
    {
        private readonly APIContext _context;

        public TaskRepository(APIContext context)
        {
            _context = context;
                    
        }
        public async Task<bool> AddTaskAsyncTask(Task entity)
        {
            await _context.Tasks.AddAsync(entity);
            return await Save();
        }

        public async Task<Task> GetTaskByIdAsync(string id)
        {
           return  await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> DeleteTaskAsync(Task entity)
        {
            _context.Tasks.Remove(entity);
            return await Save(); 
        }

        public async Task<bool> UpdateTaskAsync(Task entity)
        { 
            _context.Tasks.Update(entity);
           return await Save();
        } 

        public IQueryable<Task> GetAllTasksAsync()
        {
            return _context.Tasks.AsQueryable();

            //return  Task.Run(() => _context.Tasks.AsQueryable());

        }

        public async Task<bool> Save()
        {
          return await  _context.SaveChangesAsync() > 0;
        }
    }
}
