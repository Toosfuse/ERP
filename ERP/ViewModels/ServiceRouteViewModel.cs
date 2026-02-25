namespace ERP.ViewModels
{
    public class ServiceRouteViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string VehicleType { get; set; }
        public string StartTime { get; set; }
        public bool IsActive { get; set; }
        public string CreatedAt { get; set; }
        public int StopsCount { get; set; }
        public string FirstStopName { get; set; }
        public List<RouteStopViewModel> Stops { get; set; } = new List<RouteStopViewModel>();
    }

    public class RouteStopViewModel
    {
        public int Id { get; set; }
        public int ServiceRouteId { get; set; }
        public int Sequence { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class VehicleLogViewModel
    {
        public int Id { get; set; }
        public int ServiceRouteId { get; set; }
        public string RouteName { get; set; }
        public string LogDate { get; set; }
        public string ArrivalTime { get; set; }
        public string DepartureTime { get; set; }
        public string Notes { get; set; }
    }
}
