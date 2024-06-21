using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace P6Travels_API_KeirynSandi.Attributes
{
    [AttributeUsage(validOn: AttributeTargets.All)]
    public sealed class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        private readonly string _apiKey = "ApiKey";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.Request.Headers.TryGetValue(_apiKey, out var ApiSalida))
            {

                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "LLAMADA AL API NO CONTIENE INFORMACIÓN DE SEGURIDAD"
                };
                return;
            }

            var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var ApiKeyValue = appSettings.GetValue<string>(_apiKey);

            if (ApiKeyValue != null && !ApiKeyValue.Equals(ApiSalida))
            {

                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "EL VALOR DE LLAVE DE SEGURIDAD ES INCORRECTO"
                };
                    return;
            }



        }

    }
}
