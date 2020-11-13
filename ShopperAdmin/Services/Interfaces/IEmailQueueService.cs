using System;
using System.Threading;
using System.Threading.Tasks;

namespace ShopperAdmin.Services.Interfaces
{
    public interface IEmailQueueService
    {
        void QueueMail(Func<CancellationToken, Task> mail);
        Task<Func<CancellationToken, Task>> DequeueMail(CancellationToken cancellationToken);
    }
}