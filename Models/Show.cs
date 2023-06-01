using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace WatchMe.Models
{
    public class Show
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }

        [JsonIgnore]
        public DateTime Start { get; set; }

        [JsonIgnore]
        public DateTime End { get; set; }

        [SwaggerSchema("MM/dd HH:mm")]
        public string StartTime
        {
            get { return Start.ToString("MM/dd HH:mm"); }
        }

        [SwaggerSchema("MM/dd HH:mm")]
        public string EndTime
        {
            get { return End.ToString("MM/dd HH:mm"); }
        }
        public int BarId { get; set; }
        public string Sport { get; set; }
    }
}
