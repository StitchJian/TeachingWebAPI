using grade_sheet_api.IServices;
using grade_sheet_api.Models;
using Microsoft.EntityFrameworkCore;

namespace grade_sheet_api.Services;

public class GradeService : IGradeService
{
    private readonly GradeContext _context;

    public GradeService(GradeContext context)
    {
        _context = context;
    }

    public async Task CreateGrade(Grade Grade)
    {
        _context.Grades.Add(Grade);
        await _context.SaveChangesAsync();
    }

    public async Task<Grade?> GetGrade(int index)
    {
        return await _context.Grades.FirstOrDefaultAsync(p => p.number == index);
    }

    public async Task<IEnumerable<Grade>> FindGrade(int pageSize, int currentPage, string sort, string order, string querySearch)
    {
        IQueryable<Grade> items;

        items = _context.Grades.AsQueryable();
        items = WhereHelps.setWhereStr(pageSize, currentPage, typeof(Grade).GetProperties(), items, sort, order, querySearch);

        return await items.ToListAsync();
    }

    public async Task<int> CountGrade(string querySearch)
    {
        var items = _context.Grades.AsQueryable();
        items = WhereHelps.setWhereStr(querySearch, typeof(Grade).GetProperties(), items);

        return await items.CountAsync();
    }

    public async Task UpdateGrade(Grade Grade)
    {
        _context.Grades.Update(Grade);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteGrade(Grade Grade)
    {
        _context.Grades.Remove(Grade);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Grade>> GetAllGrade()
    {
        return await _context.Grades.ToListAsync();
    }
}
