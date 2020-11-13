using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using Shared.Extensions.Helpers;

namespace ShopperAdmin.Extensions.Helpers
{
    public static class TempDataExtensions
    {
        public static string SerializeModelStater(this ModelStateDictionary modelStateDictionary)
        {
            var errorList = modelStateDictionary
                .Select(kvp => new ModelStateTransfer
                {
                    Key = kvp.Key,
                    AttemptedValue = kvp.Value.AttemptedValue,
                    RawValue = kvp.Value.RawValue,
                    ErrorMessages = kvp.Value.Errors.Select(err => err.ErrorMessage).ToList(),
                });

            return JsonConvert.SerializeObject(errorList, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }

        public static ModelStateDictionary DeserializeModelState(this string serialisedErrorList)
        {
            var errorList = JsonConvert.DeserializeObject<List<ModelStateTransfer>>(serialisedErrorList);
            var modelState = new ModelStateDictionary();

            foreach (var item in errorList)
            {
                modelState.SetModelValue(item.Key, item.RawValue, item.AttemptedValue);
                foreach (var error in item.ErrorMessages)
                {
                    modelState.AddModelError(item.Key, error);
                }
            }

            return modelState;
        }


        public static void Put<T>(this ITempDataDictionary tempData, string key, T value) where T : class
        {
            tempData[key] = JsonConvert.SerializeObject(value, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }

        public static T Get<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            object o;
            tempData.TryGetValue(key, out o);
            return o == null ? null : JsonConvert.DeserializeObject<T>((string) o);
        }

        public static void GetModelErrorsFromTempData(this ModelStateDictionary modelStateDictionary,
            TempDataDictionary tempData)
        {
            Console.WriteLine("temp data is null: " + tempData["ModelState"].IsNull());
            // var tempDateModelState = JsonConvert.DeserializeObject(tempData["ModelState"].ToString()) as ModelStateEntry;
            //
            // if (tempDateModelState == null)
            // {
            //     return;
            // }
            //
            // tempDateModelState.Errors.ToList().ForEach(me =>
            // {
            //     modelStateDictionary.AddModelError("", me.ErrorMessage);
            // });
        }
    }
}