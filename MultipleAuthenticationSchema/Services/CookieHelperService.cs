namespace MultipleAuthenticationSchema.Services;

public class CookieHelperService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;

    public CookieHelperService(
        IHttpContextAccessor httpContextAccessor,
        IConfiguration configuration)
    {
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
    }

    public string GetCookie()
    {
        var cookieName = _configuration.GetValue<string>("CookieName");
        return _httpContextAccessor.HttpContext?.Request.Cookies[cookieName];
    }
}