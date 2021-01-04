namespace CateringSystem.Business.Models
{
    public class RoomSetupModel
    {
        public int Id { get; set; }
        public string BuildingName { get; set; }
        public int BuildingId { get; set; }
        public string RoomNo { get; set; }
        public string Description { get; set; }
        public bool? IsRack { get; set; }
    }
}
