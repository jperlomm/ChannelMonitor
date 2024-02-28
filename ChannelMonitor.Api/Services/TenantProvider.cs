namespace ChannelMonitor.Api.Services
{
    public class TenantProvider : ITenantProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TenantProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid? GetTenantId()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext != null && httpContext.User != null && httpContext.User.Identity != null && httpContext.User.Identity.IsAuthenticated)
            {
                // Verificar si el usuario está autenticado
                if (httpContext!.User.Identity!.IsAuthenticated)
                {
                    // Buscar el claim "tenant_id" en el token JWT
                    var tenantIdClaim = httpContext.User.FindFirst("tenant_id");

                    if (tenantIdClaim != null && Guid.TryParse(tenantIdClaim.Value, out Guid tenantId))
                    {
                        return tenantId;
                    }
                }
            }

            // Si el usuario no está autenticado o el claim no está presente, devolver null
            return null;
        }


    }
}
