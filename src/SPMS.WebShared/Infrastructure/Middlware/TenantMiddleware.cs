using SPMS.WebShared.Infrastructure.Services;

namespace SPMS.WebShared.Infrastructure.Middlware
{
    internal class TenantMiddleware<T> where T : TenantDto
    {
        private readonly RequestDelegate next;

        public TenantMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Items.ContainsKey(StaticValues.HttpContextTenantKey))
            {
                var tenantService = context.RequestServices.GetService(typeof(TenantAccessService<T>)) as TenantAccessService<T>;
                context.Items.Add(StaticValues.HttpContextTenantKey, await tenantService.GetTenantAsync());
            }

            //Continue processing
            if (next != null)
                await next(context);
        }
    }
}