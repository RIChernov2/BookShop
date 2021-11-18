using System;
using System.Collections.Generic;
using System.Linq;

namespace BookShop.Helpers
{
    /// <summary>
    /// Helper for parsing collection from url query
    /// </summary>
    public static class QueryParser
    {
        /// <summary>
        /// Parse concatenated string of Ids to Array
        /// </summary>
        /// <param name="ids">Concatenation of Ids, with ',' as separator</param>
        /// <returns></returns>
        public static long[] ParseIds(string ids)
        {
            var logger = NLog.LogManager.GetCurrentClassLogger();
            if (String.IsNullOrEmpty(ids) || String.IsNullOrWhiteSpace(ids))
            {
                logger.Info("ParseIds method. Empty ids.");
                return Array.Empty<long>();
            }

            return ids.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(long.Parse)
                .ToArray();
        }
    }
}