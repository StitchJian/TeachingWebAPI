
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace grade_sheet_api.Models;
public class Grade
{
    /// <summary>
    /// 流水號
    /// </summary>
    [Key]
    [Display(Name = "流水號")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Comment("流水號")]
    public int number { get; set; }

    [Comment("姓名")]
    public string name { get; set; } = null!;

    [Comment("成績")]
    public int grade { get; set; }
}