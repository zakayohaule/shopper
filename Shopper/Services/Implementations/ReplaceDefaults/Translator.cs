// using System.Collections.Generic;
// using Microsoft.AspNetCore.Http;
// using Microsoft.Extensions.Caching.Memory;
// using Microsoft.Extensions.Localization;
//
// namespace Shopper.Services.Implementations.ReplaceDefaults
// {
//     public class Translator : IStringLocalizer
//     {
//         private readonly IHttpContextAccessor _httpContextAccessor;
//         private readonly IMemoryCache _memoryCache;
//
//         public Translator(IHttpContextAccessor httpContextAccessor, IMemoryCache memoryCache)
//         {
//             _httpContextAccessor = httpContextAccessor;
//             _memoryCache = memoryCache;
//         }
//
//         public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
//         {
//             var lang = _httpContextAccessor.HttpContext.Request.Cookies["lang"] ?? "en";
//
//             if (lang.Equals("sw"))
//             {
//                 return _memoryCache.Get<IEnumerable<LocalizedString>>("sw");
//             }
//
//             return new List<LocalizedString>();
//         }
//
//         public LocalizedString this[string name] => throw new System.NotImplementedException();
//
//         public LocalizedString this[string name, params object[] arguments] => throw new System.NotImplementedException();
//     }
// }
