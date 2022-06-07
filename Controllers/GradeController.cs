using AutoMapper;
using grade_sheet_api.IServices;
using grade_sheet_api.Models;
using grade_sheet_api.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace grade_sheet_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GradeController : ControllerBase
{
    private IGradeService _service;
    private IMapper _mapper;
    private readonly IConfiguration _config;

    public GradeController(IGradeService service, IMapper mapper, IConfiguration config)
    {
        _service = service;
        _mapper = mapper;
        _config = config;
    }


    /// <summary>
    /// 取得成績
    /// </summary>
    /// <param name="index">流水號</param>
    /// <returns></returns>
    [HttpGet("GetGrade/{index}")]
    public async Task<ActionResult<ViewGrade>> GetGrade(int index)
    {
        try
        {
            // 搜尋是否存在，若無則回報錯誤
            Grade? grade = await _service.GetGrade(index);
            if (grade == null)
                return BadRequest("No Data!");

            ViewGrade vPintTemplate = _mapper.Map<ViewGrade>(grade);
            return Ok(vPintTemplate);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }

    /// <summary>
    /// 保存成績
    /// </summary>
    /// <param name="ViewGrade">成績</param>
    /// <returns></returns>
    [HttpPost("SaveGrade")]
    public async Task<ActionResult> SaveGrade(ViewGrade ViewGrade)
    {
        try
        {
            Grade grade = _mapper.Map<Grade>(ViewGrade);

            // 搜尋是否存在，若無新增有則修改
            var original = await _service.GetGrade(ViewGrade.number);
            if (original == null)
                await _service.CreateGrade(grade);
            else
                await _service.UpdateGrade(grade);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }

    /// <summary>
    /// 刪除成績
    /// </summary>
    /// <param name="index">流水號</param>
    /// <returns></returns>
    [HttpDelete("DeleteGrade/{index}")]
    public async Task<ActionResult> DeleteGrade(int index)
    {
        try
        {
            // 搜尋是否存在，若有則刪除若無則回報錯誤
            var original = await _service.GetGrade(index);
            if (original == null)
                return BadRequest("No Data!");
            else
                await _service.DeleteGrade(original);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }

    /// <summary>
    /// 批量刪除成績
    /// </summary>
    /// <param name="index">流水號</param>
    /// <returns></returns>
    [HttpPost("BatchDeleteGrade/{index}")]
    public async Task<ActionResult> BatchDeleteGrade(int index)
    {
        try
        {
            // 搜尋是否存在，若有則刪除若無則回報錯誤
            var original = await _service.GetGrade(index);
            if (original == null)
                return BadRequest("No Data!");
            else
                await _service.DeleteGrade(original);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }

    /// <summary>
    /// 成績列表
    /// </summary>
    /// <param name="pageSize">單頁顯示數量</param>
    /// <param name="currentPage">當前頁</param>
    /// <param name="sort">排序欄位</param>
    /// <param name="order">正反序</param>
    /// <param name="querySearch">關鍵字(全部)</param>
    /// <returns></returns>
    [HttpGet("FindGrade/{pageSize}/{currentPage}")]
    public async Task<ActionResult<ViewGradeList>> FindGrade(int pageSize, int currentPage, string? sort, string? order, string? querySearch)
    {
        try
        {
            var result = new ViewGradeList();
            var items = await _service.FindGrade(pageSize, currentPage, sort, order, querySearch);
            result.ViewGrades = items.Select(x => _mapper.Map<ViewGrade>(x)).ToList();
            result.Total = await _service.CountGrade(querySearch);

            return result;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }

    /// <summary>
    /// 取得全部成績(測試用平常勿使用)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetAllGrade")]
    public async Task<ActionResult<ViewGrade>> GetAllGrade()
    {
        try
        {
            // 搜尋是否存在，若無則回報錯誤
            List<Grade> grades = await _service.GetAllGrade();
            List<ViewGrade> viewGrades = grades.Select(x => _mapper.Map<ViewGrade>(x)).ToList();
            return new ObjectResult(viewGrades);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }
}
