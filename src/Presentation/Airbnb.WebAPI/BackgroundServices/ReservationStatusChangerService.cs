//using Airbnb.Domain.Entities.PropertyRelated;
//using Airbnb.Domain.Enums.Reservations;
//using Newtonsoft.Json;
//using System.Diagnostics;

//namespace Airbnb.WebAPI.BackgroundServices
//{
//    public class ReservationStatusChangerService : BackgroundService
//    {
//        private readonly ILogger<ReservationStatusChangerService> _logger;
//        private HttpClient _client;

//        public ReservationStatusChangerService(ILogger<ReservationStatusChangerService> logger)
//        {
//            _logger = logger;
//        }
//        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//        {
//            //DateTime targetTime = DateTime.Today.AddDays(1);
//            DateTime targetTime = DateTime.Now;
//            while (!stoppingToken.IsCancellationRequested)
//            { 
//                var result = await _client
//                    .GetAsync("https://localhost:7146/api/v1/reservations/getavailablereservations",
//                    stoppingToken);
//                if (result.IsSuccessStatusCode)
//                {
//                    List<Reservation> reservations = JsonConvert
//                        .DeserializeObject<List<Reservation>>(await result.Content.ReadAsStringAsync(stoppingToken));
//                    await PatchReservationStatus(targetTime, result, reservations);
//                }
//                else
//                {
//                    _logger.LogError("Something went wrong. Status code: {StatusCode}", result.StatusCode);
//                }
//                // bele olmalidi eslinde
//                //targetTime = targetTime.AddDays(1);

//                targetTime = targetTime.AddHours(1);
//                int delayedTime = (int)targetTime.Subtract(DateTime.Now).TotalMilliseconds;
//                await Task.Delay(delayedTime, stoppingToken);
//            }
//        }

//        private async Task PatchReservationStatus(DateTime targetTime, HttpResponseMessage result, List<Reservation> reservations)
//        {
//            reservations.ForEach(async x =>
//            {
//                await _client
//               .PatchAsync($"https://localhost:7146/api/v1/reservations/updatereservationstatus/{x.Id}",
//               result.Content);
//                // filestream qoshub bu yazilari text e de yazmaq olar

//                _logger.LogInformation("{Id} Reservation status has been updated. Date: {targetTime}",
//                x.Id, targetTime);

//            });
//            await Task.CompletedTask;
//        }

//        public override Task StartAsync(CancellationToken cancellationToken)
//        {
//            _logger.LogInformation("Reservation status updater service started");
//            _client = new HttpClient();
//            return base.StartAsync(cancellationToken);
//        }
//        public override Task StopAsync(CancellationToken cancellationToken)
//        {
//            // httpClient ishlemesin deye dispose edirik,yoxsa achilmish client baglanmir.
//            _client.Dispose();
//            _logger.LogInformation("Reservation status updater service stopped");
//            return base.StopAsync(cancellationToken);
//        }
//    }
//}
