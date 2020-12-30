using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Threading.Tasks;

namespace LoggingService.Binders
{
    public class LogLevelModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var key = bindingContext.ModelName;
            var val = bindingContext.ValueProvider.GetValue(key);
                var s = val.FirstValue;
                if (s != null && s.IndexOf(",", System.StringComparison.Ordinal) > 0)
                {
                    s = s.Replace(" ", string.Empty);
                    var stringArray = s.Split(new[] { "," }, StringSplitOptions.None);
                    bindingContext.Result = ModelBindingResult.Success(stringArray);
                }
                else
                {
                    bindingContext.Result = ModelBindingResult.Success(new string[] { s });
                }

            return Task.CompletedTask;
        }
    }
}