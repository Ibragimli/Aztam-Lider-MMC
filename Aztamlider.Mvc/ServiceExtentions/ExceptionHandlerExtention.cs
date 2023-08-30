using Aztamlider.Services.CustomExceptions;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;

namespace Aztamlider.Mvc.ServiceExtentions
{
    public static class ExceptionHandlerExtention
    {
        public static void AddExceptionHandlerService(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var code = 500;
                    string message = "Inter Server Error. Please Try Again Later!";

                    if (contextFeature != null)
                    {
                        //Exception codes
                        //null - 1,
                        //Format - 2,
                        //Already - 3,
                        //Notfound - 404,
                        //Use - 5,
                        //Count - 6,
                        //PasswordReset - 7,
                        //Count - 6,


                        message = contextFeature.Error.Message;
                        if (contextFeature.Error is NotFoundException)
                            code = 404;

                        //Item
                        if (contextFeature.Error is ItemNotFoundException)
                            code = 404;
                        if (contextFeature.Error is ItemNullException)
                            code = 1001;
                        if (contextFeature.Error is ItemFormatException)
                            code = 1002;
                        if (contextFeature.Error is ItemAlreadyException)
                            code = 1003;
                        if (contextFeature.Error is ValueAlreadyExpception)
                            code = 1004;
                        if (contextFeature.Error is UserNotFoundException)
                            code = 1004;
                        if (contextFeature.Error is ItemUseException)
                            code = 1005;

                        if (contextFeature.Error is ValueFormatExpception)
                            code = 1042;
                        if (contextFeature.Error is UserPasswordResetException)
                            code = 1007;
                        if (contextFeature.Error is UserLoginAttempCountException)
                            code = 1015;

                        //Image
                        if (contextFeature.Error is ImageNullException)
                            code = 1041;
                        if (contextFeature.Error is ImageFormatException)
                            code = 1042;
                        if (contextFeature.Error is ImageCountException)
                            code = 1045;


                    }

                    context.Response.StatusCode = code;

                    var errprJsonStr = JsonConvert.SerializeObject(new { code = code, message = message });

                    await context.Response.WriteAsync(errprJsonStr);
                });

            });

        }
    }
}
