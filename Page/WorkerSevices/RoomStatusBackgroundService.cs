using BusineessLogic.Repository;

namespace Page.WorkerSevices;

public class RoomStatusBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<RoomStatusBackgroundService> _logger;

    public RoomStatusBackgroundService(IServiceProvider serviceProvider, ILogger<RoomStatusBackgroundService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Room Status Background Service running.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var roomStatusUpdaterService = scope.ServiceProvider.GetRequiredService<IBookingRepository>();
                    await roomStatusUpdaterService.UpdateRoomStatusesAsync();
                }

                _logger.LogInformation("Room statuses updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating room statuses.");
            }

            await Task.Delay(TimeSpan.FromHours(1), stoppingToken); // Check every hour
        }
        
    }
}
