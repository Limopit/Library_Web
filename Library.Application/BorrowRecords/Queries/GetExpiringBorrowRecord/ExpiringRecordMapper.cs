using AutoMapper;
using Library.Application.Common.Mappings;
using Library.Domain;

namespace Library.Application.BorrowRecords.Queries.GetExpiringBorrowRecord;

public class ExpiringRecordMapper:IMapWith<BorrowRecord>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<BorrowRecord, ExpiringRecordDto>()
            .ForMember(dto => dto.UserID, opt => opt.MapFrom(recond => recond.UserId))
            .ForMember(dto => dto.IssueDate, opt => opt.MapFrom(recond => recond.BookIssueDate))
            .ForMember(dto => dto.ExpirationDate, opt => opt.MapFrom(recond => recond.BookIssueExpirationDate));
    }
}