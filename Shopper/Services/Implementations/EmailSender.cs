using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Serilog;
using Shopper.Services.Interfaces;

namespace Shopper.Services.Implementations
{
    public class EmailSender : BackgroundService
    {
        private readonly IEmailQueueService _emailQueueService;
        private readonly ILogger _logger;

        public EmailSender(IEmailQueueService emailQueueService, ILogger logger)
        {
            _emailQueueService = emailQueueService;
            this._logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.Information("************* Email sender background service has service started *************");
            await SendEmailAsync(stoppingToken);
        }

        public async Task SendEmailAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var queuedMail = _emailQueueService.DequeueMail(cancellationToken);

                try
                {
                    var result = await queuedMail;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}
