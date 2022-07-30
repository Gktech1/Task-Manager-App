using System.ComponentModel.DataAnnotations;

namespace ApiTask.DTOs
{
    public class AddNewTaskDto
    {
        [Required]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Task text must be between 5 and 200 character")]
        public string Text { get; set; }    
    }
}
