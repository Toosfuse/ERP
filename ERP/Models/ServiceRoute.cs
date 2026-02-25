namespace ERP.Models
{
    public class ServiceRoute
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string VehicleType { get; set; }
        public TimeSpan StartTime { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public ICollection<RouteStop> Stops { get; set; }
        public ICollection<VehicleLog> Logs { get; set; }
    }

    public class RouteStop
    {
        public int Id { get; set; }
        public int ServiceRouteId { get; set; }
        public int Sequence { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        
        public ServiceRoute ServiceRoute { get; set; }
    }

    public class VehicleLog
    {
        public int Id { get; set; }
        public int ServiceRouteId { get; set; }
        public DateTime LogDate { get; set; }
        public TimeSpan? ArrivalTime { get; set; }
        public TimeSpan? DepartureTime { get; set; }
        public string Notes { get; set; }
        
        public ServiceRoute ServiceRoute { get; set; }
    }
}
