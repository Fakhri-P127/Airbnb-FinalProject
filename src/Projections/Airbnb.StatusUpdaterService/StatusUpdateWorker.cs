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
                // bele olmalidi eslinde, axsham her gece 1 defe update etsin statuslari. 
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
            // httpClient gunde 1 defe ishleyir ve bosh boshuna achiq qalmasin deye dispose edirik.
            _client.Dispose();
            _logger.LogInformation("Status updater service stopped");
            return base.StopAsync(cancellationToken);
        }
    }
}