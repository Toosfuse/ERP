using ERP.Data;
using ERP.Models;
using ERP.Services;
using ERP.ViewModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERP.Controllers
{
    public class ServiceRouteController : Controller
    {
        private readonly ERPContext _context;
        private readonly IServices _services;

        public ServiceRouteController(ERPContext context, IServices services)
        {
            _context = context;
            _services = services;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region ServiceRoute
        public async Task<IActionResult> Routes_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = await _context.ServiceRoutes
                .AsNoTracking()
                .Include(x => x.Stops)
                .OrderByDescending(x => x.Id)
                .ToListAsync();

            var result = data.Select(x => new ServiceRouteViewModel
            {
                Id = x.Id,
                Name = x.Name,
                VehicleType = x.VehicleType,
                StartTime = x.StartTime.ToString(@"hh\:mm"),
                IsActive = x.IsActive,
                CreatedAt = _services.iGregorianToPersianDateTime(x.CreatedAt),
                StopsCount = x.Stops.Count,
                FirstStopName = x.Stops.OrderBy(s => s.Sequence).FirstOrDefault()?.Name
            }).ToList();

            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Routes_Create([DataSourceRequest] DataSourceRequest request, ServiceRouteViewModel model)
        {
            ModelState.Remove("CreatedAt");
            ModelState.Remove("Stops");
            ModelState.Remove("FirstStopName");
            ModelState.Remove("StopsCount");
            
            if (!ModelState.IsValid)
                return Json(new[] { model }.ToDataSourceResult(request, ModelState));

            var entity = new ServiceRoute
            {
                Name = model.Name.Trim(),
                VehicleType = model.VehicleType.Trim(),
                StartTime = TimeSpan.Parse(model.StartTime),
                IsActive = true
            };

            _context.ServiceRoutes.Add(entity);
            await _context.SaveChangesAsync();

            if (model.Stops?.Count > 0)
            {
                var stops = model.Stops.Select((s, index) => new RouteStop
                {
                    ServiceRouteId = entity.Id,
                    Sequence = index + 1,
                    Name = s.Name?.Trim() ?? "",
                    Address = s.Address?.Trim() ?? ""
                }).ToList();

                _context.RouteStops.AddRange(stops);
                await _context.SaveChangesAsync();
                model.StopsCount = stops.Count;
                model.FirstStopName = stops.FirstOrDefault()?.Name;
            }
            else
            {
                model.StopsCount = 0;
            }

            model.Id = entity.Id;
            model.IsActive = entity.IsActive;
            model.CreatedAt = _services.iGregorianToPersianDateTime(entity.CreatedAt);

            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRouteWithStops([FromBody] CreateRouteRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name) || request.Stops == null || request.Stops.Count == 0)
                return Json(new { success = false, message = "نام مسیر و ایستگاهها الزامی هستند" });

            var route = new ServiceRoute
            {
                Name = request.Name.Trim(),
                VehicleType = request.VehicleType.Trim(),
                StartTime = TimeSpan.Parse(request.StartTime),
                IsActive = true
            };

            _context.ServiceRoutes.Add(route);
            await _context.SaveChangesAsync();

            var stops = request.Stops.Select((s, index) => new RouteStop
            {
                ServiceRouteId = route.Id,
                Sequence = index + 1,
                Name = s.Name?.Trim() ?? "",
                Address = s.Address?.Trim() ?? ""
            }).ToList();

            _context.RouteStops.AddRange(stops);
            await _context.SaveChangesAsync();

            return Json(new { success = true, routeId = route.Id, stopsCount = stops.Count });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Routes_Update([DataSourceRequest] DataSourceRequest request, ServiceRouteViewModel model)
        {
            ModelState.Remove("CreatedAt");

            if (!ModelState.IsValid)
                return Json(new[] { model }.ToDataSourceResult(request, ModelState));

            var entity = await _context.ServiceRoutes.FindAsync(model.Id);
            if (entity == null)
                return Json(new[] { model }.ToDataSourceResult(request, ModelState));

            entity.Name = model.Name.Trim();
            entity.VehicleType = model.VehicleType.Trim();
            entity.StartTime = TimeSpan.Parse(model.StartTime);
            entity.IsActive = model.IsActive;

            await _context.SaveChangesAsync();

            model.CreatedAt = _services.iGregorianToPersianDateTime(entity.CreatedAt);
            return Json(new[] { model }.ToDataSourceResult(request));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Routes_Destroy([DataSourceRequest] DataSourceRequest request, ServiceRouteViewModel model)
        {
            var entity = await _context.ServiceRoutes.FindAsync(model.Id);
            if (entity != null)
            {
                _context.ServiceRoutes.Remove(entity);
                await _context.SaveChangesAsync();
            }

            return Json(new[] { model }.ToDataSourceResult(request));
        }
        #endregion

        #region RouteStop
        public async Task<IActionResult> Stops_Read([DataSourceRequest] DataSourceRequest request, int routeId)
        {
            var data = await _context.RouteStops
                .AsNoTracking()
                .Where(x => x.ServiceRouteId == routeId)
                .OrderBy(x => x.Sequence)
                .ToListAsync();

            var result = data.Select(x => new RouteStopViewModel
            {
                Id = x.Id,
                ServiceRouteId = x.ServiceRouteId,
                Sequence = x.Sequence,
                Name = x.Name,
                Address = x.Address
            }).ToList();

            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Stops_Create([DataSourceRequest] DataSourceRequest request, RouteStopViewModel model)
        {
            if (!ModelState.IsValid)
                return Json(new[] { model }.ToDataSourceResult(request, ModelState));

            var entity = new RouteStop
            {
                ServiceRouteId = model.ServiceRouteId,
                Sequence = model.Sequence,
                Name = model.Name?.Trim() ?? "",
                Address = model.Address?.Trim() ?? ""
            };

            _context.RouteStops.Add(entity);
            await _context.SaveChangesAsync();

            model.Id = entity.Id;
            return Json(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Stops_Update([DataSourceRequest] DataSourceRequest request, RouteStopViewModel model)
        {
            if (!ModelState.IsValid)
                return Json(new[] { model }.ToDataSourceResult(request, ModelState));

            var entity = await _context.RouteStops.FindAsync(model.Id);
            if (entity == null)
                return Json(new[] { model }.ToDataSourceResult(request, ModelState));

            entity.Sequence = model.Sequence;
            entity.Name = model.Name.Trim();
            entity.Address = model.Address.Trim();

            await _context.SaveChangesAsync();
            return Json(new[] { model }.ToDataSourceResult(request));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Stops_Destroy([DataSourceRequest] DataSourceRequest request, RouteStopViewModel model)
        {
            var entity = await _context.RouteStops.FindAsync(model.Id);
            if (entity != null)
            {
                _context.RouteStops.Remove(entity);
                await _context.SaveChangesAsync();
            }

            return Json(new[] { model }.ToDataSourceResult(request));
        }
        #endregion

        #region VehicleLog
        public IActionResult Calendar()
        {
            return View();
        }

        public async Task<IActionResult> Logs_Read([DataSourceRequest] DataSourceRequest request, string date)
        {
            DateTime logDate = DateTime.TryParse(date, out var d) ? d : DateTime.Now;

            var data = await _context.VehicleLogs
                .AsNoTracking()
                .Where(x => x.LogDate.Date == logDate.Date)
                .Include(x => x.ServiceRoute)
                .OrderBy(x => x.ServiceRoute.StartTime)
                .ToListAsync();

            var result = data.Select(x => new VehicleLogViewModel
            {
                Id = x.Id,
                ServiceRouteId = x.ServiceRouteId,
                RouteName = x.ServiceRoute.Name,
                LogDate = _services.iGregorianToPersianDateTime(x.LogDate),
                ArrivalTime = x.ArrivalTime.HasValue ? x.ArrivalTime.Value.ToString(@"hh\:mm") : null,
                DepartureTime = x.DepartureTime.HasValue ? x.DepartureTime.Value.ToString(@"hh\:mm") : null,
                Notes = x.Notes
            }).ToList();

            return Json(result.ToDataSourceResult(request));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logs_Create([DataSourceRequest] DataSourceRequest request, VehicleLogViewModel model)
        {
            if (!ModelState.IsValid)
                return Json(new[] { model }.ToDataSourceResult(request, ModelState));

            var entity = new VehicleLog
            {
                ServiceRouteId = model.ServiceRouteId,
                LogDate = DateTime.Parse(model.LogDate),
                ArrivalTime = !string.IsNullOrEmpty(model.ArrivalTime) ? TimeSpan.Parse(model.ArrivalTime) : null,
                DepartureTime = !string.IsNullOrEmpty(model.DepartureTime) ? TimeSpan.Parse(model.DepartureTime) : null,
                Notes = model.Notes?.Trim()
            };

            _context.VehicleLogs.Add(entity);
            await _context.SaveChangesAsync();

            model.Id = entity.Id;
            return Json(new[] { model }.ToDataSourceResult(request));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logs_Update([DataSourceRequest] DataSourceRequest request, VehicleLogViewModel model)
        {
            if (!ModelState.IsValid)
                return Json(new[] { model }.ToDataSourceResult(request, ModelState));

            var entity = await _context.VehicleLogs.FindAsync(model.Id);
            if (entity == null)
                return Json(new[] { model }.ToDataSourceResult(request, ModelState));

            entity.ArrivalTime = !string.IsNullOrEmpty(model.ArrivalTime) ? TimeSpan.Parse(model.ArrivalTime) : null;
            entity.DepartureTime = !string.IsNullOrEmpty(model.DepartureTime) ? TimeSpan.Parse(model.DepartureTime) : null;
            entity.Notes = model.Notes?.Trim();

            await _context.SaveChangesAsync();
            return Json(new[] { model }.ToDataSourceResult(request));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logs_Destroy([DataSourceRequest] DataSourceRequest request, VehicleLogViewModel model)
        {
            var entity = await _context.VehicleLogs.FindAsync(model.Id);
            if (entity != null)
            {
                _context.VehicleLogs.Remove(entity);
                await _context.SaveChangesAsync();
            }

            return Json(new[] { model }.ToDataSourceResult(request));
        }

        public async Task<IActionResult> GetRoutes()
        {
            var data = await _context.ServiceRoutes
                .AsNoTracking()
                .Where(x => x.IsActive)
                .Select(x => new { id = x.Id, name = x.Name })
                .ToListAsync();

            return Json(data);
        }
        #endregion
    }

    public class SaveStopsRequest
    {
        public int RouteId { get; set; }
        public List<RouteStopViewModel> Stops { get; set; }
    }

    public class CreateRouteRequest
    {
        public string Name { get; set; }
        public string VehicleType { get; set; }
        public string StartTime { get; set; }
        public List<RouteStopViewModel> Stops { get; set; }
    }
}
