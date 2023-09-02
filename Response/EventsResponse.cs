namespace Assessment.Response
{
    public class EventsResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string Location { get; set; } = "";
        public int Slots { get; set; }
        public double Price { get; set; }
        public DateTime EventDate { get; set; } = DateTime.Now;
    }
}