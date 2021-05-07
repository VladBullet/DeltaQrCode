namespace DeltaQrCode.HelpersAndExtensions
{

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class HttpHelper : IHttpHelper
    {
        private readonly IUrlHelper _urlHelper;
        private readonly HttpRequest _request;

        public HttpHelper(IUrlHelper urlHelper, IHttpContextAccessor httpContextAccessor)
        {
            _urlHelper = urlHelper;
            _request = httpContextAccessor.HttpContext.Request;
        }

        public string GetGuestClientLink(string code)
        {
            var scheme = _request.Scheme;
            var host = _request.Host;
            string path = _urlHelper.Action("Details", "Guest");
            string query = "?guid=" + code;
            string url = scheme + @"://" + host + path + query;

            return url;
        }
    }
}
