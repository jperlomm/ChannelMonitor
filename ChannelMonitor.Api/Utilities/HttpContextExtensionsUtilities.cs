﻿using Microsoft.IdentityModel.Tokens;

namespace ChannelMonitor.Api.Utilities
{
    public static class HttpContextExtensionsUtilities
    {
        public static T ExtraerValorODefecto<T>(this HttpContext context, string nombreDelCampo,
            T valorPorDefecto)
            where T : IParsable<T>
        {
            var valor = context.Request.Query[nombreDelCampo];

            if (valor.IsNullOrEmpty())
            {
                return valorPorDefecto;
            }

            return T.Parse(valor!, null);
        }
    }
}
