namespace WatchMe.Models
{

    public class Bar
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public ICollection<Show>? Shows { get; set; }
    }
}