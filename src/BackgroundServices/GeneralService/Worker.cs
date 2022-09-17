using Airbnb.Application.Common.Interfaces;
using Airbnb.Domain.Enums.Reservations;

namespace GeneralService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        private readonly HttpClient _client;

        public Worker(ILogger<Worker> logger,HttpClient client)
        {
            _logger = logger;
        
            _client = client;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var result = await _client.GetAsync("https://localhost:7146/api/v1/reservations");
                    //.GetAsync);
                _logger.LogInformation(result.StatusCode.ToString());
                //if (result.IsSuccessStatusCode)
                //{

                //    _logger.LogInformation("Website is still up {StatusCode}", result.StatusCode);
                //}
                //else
                //{
                //    _logger.LogError("Website is down {StatusCode}", result.StatusCode);

                //}
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                //var values = await _unit.ReservationRepository.GetAllAsync(x =>
                //x.Status != (int)Enum_ReservationStatus.ReservationFinished
                //|| x.Status != (int)Enum_ReservationStatus.ReservationCancelled);
                //values.ForEach(async x => await new StatusChanger(_logger, _unit).UpdateReservationStatus(x.Id));
                await Task.Delay(100, stoppingToken);
            }
        }
    }
}