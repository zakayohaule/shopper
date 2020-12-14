using FluentEmail.Core;
using Microsoft.Extensions.Hosting;
using Shopper.Database;
using Shopper.Services.Interfaces;

namespace Shopper.Services.Implementations
{
    public class NotificationService : INotificationService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IFluentEmail _fluentEmail;
        private readonly IHostEnvironment _environment;
        private readonly IEmailQueueService _emailQueueService;

        public NotificationService(ApplicationDbContext dbContext, IFluentEmail fluentEmail, IHostEnvironment environment, IEmailQueueService emailQueueService)
        {
            _dbContext = dbContext;
            _fluentEmail = fluentEmail;
            _environment = environment;
            _emailQueueService = emailQueueService;
        }
        
    }
}