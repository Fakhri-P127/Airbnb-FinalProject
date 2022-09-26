using Airbnb.Application.Contracts.v1;

namespace Airbnb.StatusUpdaterService
{
    public class StatusUpdateWorker : BackgroundService
    {
        private readonly ILogger<StatusUpdateWorker> _logger;
        private HttpClient _client;

        public StatusUpdateWorker(ILogger<StatusUpdateWorker> logger)
        {
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //DateTime targetTime = DateTime.Today.AddDays(1);
            DateTime targetTime = DateTime.Now;
            while (!stoppingToken.IsCancellationRequested)
            {
                await PatchStatus(ApiRoutes.Reservations.Name, ApiRoutes.Reservations.UpdateReservationStatus);
                await PatchStatus(ApiRoutes.Hosts.Name,ApiRoutes.Hosts.UpdateHostStatus);

                // bele olmalidi eslinde
                //targetTime = targetTime.AddDays(1);
                targetTime = targetTime.AddMinutes(1);
                int delayedTime = (int)targetTime.Subtract(DateTime.Now).TotalMilliseconds;
                await Task.Delay(delayedTime, stoppingToken);
            }
        }
        private async Task PatchStatus(string controller,string action)
        {
            HttpResponseMessage patchResult = await _client
                .PatchAsync($"{ApiRoutes.BaseUrl}/{controller}/{action}", null);
            if (patchResult.IsSuccessStatusCode)
                _logger.LogInformation("{controller} has been updated. Date: {dateNow}",controller,DateTime.Now);
            else
                _logger.LogError("Something went wrong. Status code: {StatusCode}", patchResult.StatusCode);

            await Task.CompletedTask;
        }
     
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Status updater service started");
            _client = new HttpClient();
            return base.StartAsync(cancellationToken);
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            // httpClient ishlemesin deye dispose edirik,yoxsa achilmish client baglanmir.
            _client.Dispose();
            _logger.LogInformation("Status updater service stopped");
            return base.StopAsync(cancellationToken);
        }
    }
}


#region bad code
//HttpResponseMessage result = await _client
//    .GetAsync("https://localhost:7146/api/v1/reservations/getavailablereservations",
//    stoppingToken);
//if (result.IsSuccessStatusCode)
//{
//    List<PostReservationResponse> reservations = JsonConvert
//        .DeserializeObject<List<PostReservationResponse>>(await result.Content.ReadAsStringAsync(stoppingToken));
//    await PatchReservationStatus(result, reservations);
//}
//else
//{
//    _logger.LogError("Something went wrong. Status code: {StatusCode}", result.StatusCode);
//}
//private async Task PatchReservationStatus(HttpResponseMessage result, List<PostReservationResponse> reservations)
//{
//    reservations.ForEach(async x =>
//    {
//        HttpResponseMessage patchResult = await _client
//       .PatchAsync($"https://localhost:7146/api/v1/reservations/updatereservationstatus/{x.Id}",
//       result.Content);
//        // filestream qoshub bu yazilari text e de yazmaq olar
//        if (patchResult.IsSuccessStatusCode)   
//            _logger.LogInformation("{Id} Reservation status has been updated. Date: {targetTime}",
//            x.Id, DateTime.Now);
//        else 
//            _logger.LogError("Something went wrong. Status code: {StatusCode}", patchResult.StatusCode);

//    });
//    await Task.CompletedTask;
//}

//private async Task PatchHostStatus()
//{
//    HttpResponseMessage patchResult = await _client
//        .PatchAsync("https://localhost:7146/api/v1/hosts/UpdateHostStatus", null);
//    if (patchResult.IsSuccessStatusCode)
//        _logger.LogInformation($"All of the hosts status has been updated. Date: {DateTime.Now}");
//    else
//        _logger.LogError("Something went wrong. Status code: {StatusCode}", patchResult.StatusCode);

//    await Task.CompletedTask;
//}
#endregion