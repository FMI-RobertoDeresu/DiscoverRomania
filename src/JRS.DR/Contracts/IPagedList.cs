using Microsoft.AspNetCore.Http;

namespace JRS.DR.Contracts
{
    public interface IPagedList
    {
        int Page { get; set; }

        int PageSize { get; set; }

        int TotalCount { get; set; }

        string TotalDisplay();

        string PageInfoDisplay();

        string NextPageLink(HttpContext httpContext);

        string PrevPageLink(HttpContext httpContext);

        string FirstPageLink(HttpContext httpContext);

        string LastPageLink(HttpContext httpContext);
    }
}