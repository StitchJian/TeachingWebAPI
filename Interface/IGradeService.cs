using grade_sheet_api.Models;

namespace grade_sheet_api.IServices
{
    public interface IGradeService
    {
        Task<Grade?> GetGrade(int index);
        Task CreateGrade(Grade Grade);
        Task UpdateGrade(Grade Grade);
        Task DeleteGrade(Grade Grade);
        Task<List<Grade>> GetAllGrade();

        Task<IEnumerable<Grade>> FindGrade(int pageSize, int currentPage, string Sort, string Order, string querySearch);
        Task<int> CountGrade(string querySearch);
    }
}