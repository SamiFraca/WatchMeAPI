using System.ComponentModel.DataAnnotations;

namespace WatchMe.Models
{
    public class Show
    {
        [Key]
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}