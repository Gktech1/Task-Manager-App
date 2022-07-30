using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTask.Services.Interfaces;
using ApiTask.Services.Interfaces.TaskEntity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Task = ApiTask.Models.Task;

namespace ApiTask.Services
{
    public class TaskServices : ITaskServices
    {
        private readonly  ITaskRepository _taskRepo;
        public TaskServices(ITaskRepository taskRepo)
        {
            _taskRepo = taskRepo;
            this.SetTask();
        }
        public IQueryable<Task> Tasks { get; private set; }

        private void SetTask()
        {
            this.Tasks = _taskRepo.GetAllTasksAsync();
        }
        public  Task<List<Task>> PaginatedResult(int size, int page)
        {
          return System.Threading.Tasks.Task.Run(() => this.Tasks.Skip(page - 1 * size).Take(size).ToListAsync());
        }

        public async Task<string> AddTask(Task entity)
        {
          if (await _taskRepo.AddTaskAsyncTask(entity))
          {
              return entity.Id;
          }
          return null;
        }

        public async Task<Task> GetTaskById(string id)
        { 
            return await _taskRepo.GetTaskByIdAsync(id); 
            //return await Tasks.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
