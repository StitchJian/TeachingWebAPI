using AutoMapper;
using grade_sheet_api.Models;
using grade_sheet_api.ViewModels;
namespace grade_sheet_api
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Grade, ViewGrade>().ReverseMap();
        }
    }
}