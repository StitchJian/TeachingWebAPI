using System.Globalization;
using System.Linq.Dynamic.Core;
using System.Reflection;

public class WhereHelps
{

    /// <summary>
    /// 判斷及拼接where
    /// </summary>
    /// <param name="querySearch">搜尋字</param>
    /// <param name="piInfos">物件屬性</param>
    /// <param name="outResult">DB集合</param>
    /// <returns></returns>
    public static IQueryable<T> setWhereStr<T>(string? querySearch, PropertyInfo[] piInfos, IQueryable<T> outResult)
    {
        var queryList = new List<string>();
        DateTime dateTime = DateTime.Now;
        string format = "yyyyMMdd";

        foreach (var pi in piInfos)
        {
            if (DateTime.TryParseExact(querySearch, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
            {
                if (Type.Equals(pi.PropertyType, typeof(DateTime?)))
                {
                    queryList.Add($"({pi.Name} != null || {pi.Name}.Value.Date >= @0 && {pi.Name}.Value.Date <= @0)");
                }
            }
            if (Type.Equals(pi.PropertyType, typeof(int)) && querySearch != null && querySearch.All(char.IsNumber))
            {
                queryList.Add($"{pi.Name}.ToString().Contains(\"{querySearch}\")");
            }
            if (Type.Equals(pi.PropertyType, typeof(long)) && querySearch != null && querySearch.All(char.IsNumber))
            {
                queryList.Add($"{pi.Name}.ToString().Contains(\"{querySearch}\")");
            }

            Boolean parsedValue;
            if (Boolean.TryParse(querySearch, out parsedValue))
            {
                if (Type.Equals(pi.PropertyType, typeof(bool)))
                {
                    queryList.Add($"{pi.Name} == (\"{querySearch}\")");
                }
            }

            if (Type.Equals(pi.PropertyType, typeof(string)))
            {
                queryList.Add($"{pi.Name}.Contains(\"{querySearch}\")");
            }
        }
        //若有日期則擺上對應時間
        outResult = outResult.Where(String.Join(" || ", queryList.ToArray()), dateTime);
        return outResult;
    }

    /// <summary>
    /// 判斷及拼接where
    /// </summary>
    /// <param name="pageSize">單頁數量</param>
    /// <param name="currentPage">當前頁</param>
    /// <param name="piInfos">Class屬性</param>
    /// <param name="outResult">DB集合</param>
    /// <param name="Sort">排序</param>
    /// <param name="Order">正反序</param>
    /// <param name="querySearch">關鍵字</param>
    /// <typeparam name="T">類別</typeparam>
    /// <returns></returns>
    public static IQueryable<T> setWhereStr<T>(int pageSize, int currentPage, PropertyInfo[] piInfos, IQueryable<T> outResult, string Sort, string Order, string querySearch)
    {
        // 模糊搜尋關鍵字
        if (!String.IsNullOrEmpty(querySearch))
        {
            outResult = setWhereStr(querySearch, piInfos, outResult);
        }

        // 排序
        if (!String.IsNullOrEmpty(Sort))
        {
            outResult = outResult.OrderBy(Sort + " " + Order);
        }

        // 分頁&數量
        if (pageSize > 0 && currentPage > 0)
        {
            outResult = outResult.Skip(pageSize * (currentPage - 1)).Take(pageSize);
        }

        return outResult;
    }
}