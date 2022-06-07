namespace grade_sheet_api.ViewModels;

public class ViewGradeList
{
    public int Total { get; set; }
    public IEnumerable<ViewGrade>? ViewGrades { get; set; }
}