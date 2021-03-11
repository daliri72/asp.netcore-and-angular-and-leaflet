namespace LocationMap.DomainClasses.ViewModels
{
    public class LocationViewModel
    {
        public int Id { get; set; }
        public string LocationName { get; set; }
        public int? LocationTypeId { get; set; }
        public string LocationTypeName { get; set; }
        
        public string Lat { get; set; }
        public string Lng { get; set; }
        public string FileName { get; set; }
    }
}