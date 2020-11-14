using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using ShopperAdmin.Database;
using ShopperAdmin.Extensions.Helpers;
using Shared.Mvc.Entities;
using Shared.Mvc.ViewModels;
using ShopperAdmin.Services.Interfaces;
using FluentEmail.Core;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace ShopperAdmin.Services.Implementations
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