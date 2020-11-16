using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;
using Shared.Mvc.Entities;
using Shared.Mvc.Enums;
using Shared.Mvc.ViewModels;

namespace Shopper.Mvc.Controllers
{
    public class BaseController : Controller
    {
        [ViewData]
        public string Title { get; set; } = "Shopper";
        
        internal void AddBreadcrumb(string displayName, string urlPath)
        {
            List<Message> messages;

            if (ViewBag.Breadcrumb == null)
            {
                messages = new List<Message>();
            }
            else
            {
                messages = ViewBag.Breadcrumb as List<Message>;
            }

            messages.Add(new Message { DisplayName = displayName, URLPath = urlPath });
            ViewBag.Breadcrumb = messages;
        }

        internal void AddPageHeader(string pageHeader = "", string pageDescription = "")
        {
            ViewBag.PageHeader = Tuple.Create(pageHeader, pageDescription);
        }

        internal enum PageAlertType
        {
            Error,
            Info,
            Warning,
            Success
        }

        internal void AddPageAlerts(PageAlertType pageAlertType, string description)
        {
            List<Message> messages;

            if (ViewBag.PageAlerts == null)
            {
                messages = new List<Message>();
            }
            else
            {
                messages = ViewBag.PageAlerts as List<Message>;
            }

            messages.Add(new Message { Type = pageAlertType.ToString().ToLower(), ShortDesc = description });
            ViewBag.PageAlerts = messages;
        }

        internal void AddToast(ToastType type, string body)
        {
            ViewBag.Toast = new ToastModel
            {
                Type = type,
                Body = body,
            };
        }

        internal void AddErrorToast(string body)
        {
            AddToast(ToastType.Error, body);
        }
        
        internal void AddSuccessToast(string body)
        {
            AddToast(ToastType.Success, body);
        }
        
        internal void AddWarningToast(string body)
        {
            AddToast(ToastType.Warning, body);
        }
        
        internal void AddInfoToast(string body)
        {
            AddToast(ToastType.Info, body);
        }
        
        internal void AddDangerToast(string body)
        {
            AddToast(ToastType.Error, body);
        }
    }
}