﻿using System;
using System.Collections.Generic;
using System.Linq;
using JRS.DR.Contracts;
using Microsoft.AspNetCore.Http;

namespace JRS.DR.Models
{
    public class PagedList<T> : IPagedList
    {
        public PagedList(int page, int totalCount, IList<T> items)
        {
            Page = page;
            PageSize = PagedList.DefaultPageSize;
            TotalCount = totalCount;
            Items = items;
        }

        public IList<T> Items { get; set; }

        public int TotalPages => TotalCount / PageSize + (TotalCount % PageSize != 0 ? 1 : 0);

        public int Page { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public string TotalDisplay()
        {
            return $"<strong>{TotalCount}</strong> {(TotalCount == 1 ? "rezultat" : "de rezultate")}";
        }

        public string PageInfoDisplay()
        {
            return $"Pagina <strong>{Page}</strong> din <strong>{TotalPages}</strong>";
        }

        public string NextPageLink(HttpContext httpContext)
        {
            var page = GetPageOrDefault(httpContext.Request.Query);
            var path = httpContext.Request.Path;
            return page < TotalPages
                ? $"{path}?page={page + 1}&persistentCount={TotalCount}"
                : "#";
        }

        public string PrevPageLink(HttpContext httpContext)
        {
            var page = GetPageOrDefault(httpContext.Request.Query);
            var path = httpContext.Request.Path;
            return page > 1
                ? $"{path}?page={page - 1}&persistentCount={TotalCount}"
                : "#";
        }

        public string FirstPageLink(HttpContext httpContext)
        {
            var page = GetPageOrDefault(httpContext.Request.Query);
            var path = httpContext.Request.Path;
            return page > 1
                ? $"{path}?page={1}&persistentCount={TotalCount}"
                : "#";
        }

        public string LastPageLink(HttpContext httpContext)
        {
            var page = GetPageOrDefault(httpContext.Request.Query);
            var path = httpContext.Request.Path;
            return page < TotalPages
                ? $"{path}?page={TotalPages}&persistentCount={TotalCount}"
                : "#";
        }

        public int PagedIndexOf(T item)
        {
            return (Page - 1) * PageSize + Items.IndexOf(item) + 1;
        }

        //utils
        private int GetPageOrDefault(IQueryCollection query)
        {
            var pageParam = query["page"];
            var page = pageParam.Any() ? Convert.ToInt32(pageParam.First()) : 0;
            return Math.Max(page, 1);
        }
    }
}