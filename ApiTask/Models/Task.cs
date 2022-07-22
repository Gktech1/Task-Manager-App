using System.Collections.Generic;

namespace ApiTask.Models
{
    public class Task
    {
        public string Id   { get; set; }
        public string Text { get; set; }

        //Many-to-Many Relationship
        public IEnumerable<AppUserTask> Users { get; set; }

        public Task()
        {
            Users = new List<AppUserTask>();
        }
    }
}
