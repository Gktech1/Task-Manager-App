namespace ApiTask.Models
{
    public class AppUserTask
    {
        // Bridge Class
        public string Id { get; set; }
        public string UserId { get; set; }
        public string TaskId { get; set; }
        public AppUser User { get; set; }
        public Task Task { get; set; }
    }
}
