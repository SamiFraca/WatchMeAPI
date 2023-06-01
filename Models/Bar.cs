using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WatchMe.Models
{
    public class Bar
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public string Name { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public ICollection<Show>? Shows { get; set; }
        public string? Description { get; set; }
        public int UserId { get; set; }
        public string? ImageUrl { get; set; }
    }
}
