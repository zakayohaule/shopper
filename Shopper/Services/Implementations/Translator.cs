using System;
using System.Collections.Generic;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Shopper.Extensions.Configurations;
using Shopper.Services.Interfaces;

namespace Shopper.Services.Implementations
{
    public class Translator : ITranslator
    {
        private readonly Dictionary<string, Dictionary<string, string>> _dictionaries = new Dictionary<string, Dictionary<string, string>>();
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Translator(IMemoryCache memoryCache, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _dictionaries.Add("sw", memoryCache.Get<Dictionary<string, string>>("sw"));
        }

        public string this[string key]
        {
            get
            {
                var lang = _httpContextAccessor.HttpContext.Request.Cookies["lang"] ?? "en";

                if (lang.Equals("en"))
                {
                    return key;
                }

                var dictionary = _dictionaries[lang];

                if (dictionary == null)
                {
                    return key;
                }
                if (!dictionary.TryGetValue(key, out var translated))
                {
                    return key;
                }

                return translated;
            }
        }
    }
}
