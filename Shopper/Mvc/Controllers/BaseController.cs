using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shopper.Mvc.Entities;
using Shopper.Mvc.Enums;
using Shopper.Mvc.ViewModels;

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

            if (messages != null)
            {
                messages.Add(new Message {DisplayName = displayName, URLPath = urlPath});
                ViewBag.Breadcrumb = messages;
            }
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
            TempData["Toast"] = JsonConvert.SerializeObject(new ToastModel
            {
                Type = type,
                Body = body,
            });
        }

        internal void ToastError(string body)
        {
            AddToast(ToastType.Error, body);
        }

        internal void ToastSuccess(string body)
        {
            AddToast(ToastType.Success, body);
        }

        internal void ToastWarning(string body)
        {
            AddToast(ToastType.Warning, body);
        }

        internal void ToastInfo(string body)
        {
            AddToast(ToastType.Info, body);
        }

        internal void ToastDanger(string body)
        {
            AddToast(ToastType.Error, body);
        }
    }
}
