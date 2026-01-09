using System;
using System.Collections.Generic;
using System.Data;
using MongoDB.Bson;

namespace AplikasiSampahJabar
{
    public static class PerformanceManager
    {
        private static DataTable _cachedData;
        private static DateTime _lastCacheUpdate = DateTime.MinValue;
        private static readonly TimeSpan CACHE_DURATION = TimeSpan.FromMinutes(5);
        
        public static bool IsCacheValid()
        {
            return _cachedData != null && DateTime.Now - _lastCacheUpdate < CACHE_DURATION;
        }
        
        public static void UpdateCache(DataTable data)
        {
            _cachedData = data?.Copy();
            _lastCacheUpdate = DateTime.Now;
        }
        
        public static DataTable GetCachedData()
        {
            return _cachedData?.Copy();
        }
        
        public static void ClearCache()
        {
            _cachedData = null;
            _lastCacheUpdate = DateTime.MinValue;
        }
        
        public static DataTable GetPagedData(DataTable source, int pageNumber, int pageSize)
        {
            if (source == null || source.Rows.Count == 0)
                return source;
                
            int startIndex = (pageNumber - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize, source.Rows.Count);
            
            DataTable pagedTable = source.Clone();
            
            for (int i = startIndex; i < endIndex; i++)
            {
                pagedTable.ImportRow(source.Rows[i]);
            }
            
            return pagedTable;
        }
        
        public static int GetTotalPages(int totalRecords, int pageSize)
        {
            return (int)Math.Ceiling((double)totalRecords / pageSize);
        }
    }
    
    public class PaginationInfo
    {
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 50;
        public int TotalRecords { get; set; }
        public int TotalPages => PerformanceManager.GetTotalPages(TotalRecords, PageSize);
        
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
    }
}