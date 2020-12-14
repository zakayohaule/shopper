﻿using System;
 using System.Collections.Concurrent;
 using System.Threading;
 using System.Threading.Tasks;
 using Serilog;
 using Shopper.Services.Interfaces;

 namespace Shopper.Services.Implementations
{
    public class EmailQueueService : IEmailQueueService
    {
        private readonly ILogger _logger;
        
        public ConcurrentQueue<Func<CancellationToken, Task>> _emailQueue =
            new ConcurrentQueue<Func<CancellationToken, Task>>();

        public SemaphoreSlim _signal = new SemaphoreSlim(0);

        public EmailQueueService(ILogger logger)
        {
            _logger = logger;
        }

        public void QueueMail(Func<CancellationToken, Task> mail)
        {
            if (mail == null)
            {
                throw new ArgumentNullException($"The email task can not be null");
            }
            
            _logger.Warning($"Sending email to {mail.Target.ToString()}");
            _emailQueue.Enqueue(mail);
            _signal.Release();
        }

        public async Task<Func<CancellationToken, Task>> DequeueMail(CancellationToken cancellationToken)
        {
            await _signal.WaitAsync(cancellationToken);
            _emailQueue.TryDequeue(out var dequeuedMail);

            return dequeuedMail;
        }
    }
}