using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JRS.DR.Contracts
{
    public interface IPagedList
    {
        int Page { get; set; }

        int PageSize { get; set; }

        int TotalCount { get; set; }

        string TotalDisplay();

        string PageInfoDisplay();

        string NextPageLink(HttpContext httpContext, IUrlHelper url);

        string PrevPageLink(HttpContext httpContext, IUrlHelper url);

        string FirstPageLink(HttpContext httpContext, IUrlHelper url);

        string LastPageLink(HttpContext httpContext, IUrlHelper url);
    }
}