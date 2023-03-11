using System.ComponentModel.DataAnnotations;

namespace WatchMe.Models
{
    public class Show
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int BarId { get; set; }
        public string Sport { get;set; }
    }
}