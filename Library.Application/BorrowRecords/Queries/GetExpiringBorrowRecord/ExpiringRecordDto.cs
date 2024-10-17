using AutoMapper;
using Library.Application.Common.Mappings;
using Library.Domain;

namespace Library.Application.BorrowRecords.Queries.GetExpiringBorrowRecord;

public class ExpiringRecordDto: IMapWith<BorrowRecord>
{
    public string UserID { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime ExpirationDate { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<BorrowRecord, ExpiringRecordDto>()
            .ForMember(dto
                => dto.UserID, opt
                => opt.MapFrom(recond => recond.userId))
            .ForMember(dto
                => dto.IssueDate, opt
                => opt.MapFrom(recond => recond.book_issue_date))
            .ForMember(dto
                => dto.ExpirationDate, opt
                => opt.MapFrom(recond => recond.book_issue_expiration_date));

    }
}