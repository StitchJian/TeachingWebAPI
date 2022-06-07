
using Microsoft.EntityFrameworkCore;
namespace grade_sheet_api.ViewModels;
public class ViewGrade
{
    /// <summary>
    /// 流水號
    /// </summary>
    [Comment("流水號")]
    public int number { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    [Comment("姓名")]
    public string name { get; set; } = null!;

    /// <summary>
    /// 成績
    /// </summary>
    [Comment("成績")]
    public int grade { get; set; }
}