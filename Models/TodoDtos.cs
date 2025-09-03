using System.ComponentModel.DataAnnotations;

namespace MyFirstApi.Models
{
    public class TodoCreateDto
    {
        [Required, StringLength(100)]
        public string Title { get; set; } = string.Empty;
    }

    public class TodoUpdateDto
    {
        [Required, StringLength(100)]
        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
    }

    public class TodoReadDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
    }
}
