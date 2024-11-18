using AutoMapper;
using Library.Application.Common.Mappings;
using Library.Domain;

namespace Library.Application.BorrowRecords.Commands.CreateBorrowRecord;

public class BorrowRecordMapper: IMapWith<BorrowRecord>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateBorrowRecordCommand, BorrowRecord>()
            .ForMember(record => record.BookId, opt => opt.MapFrom(record => record.BookId))
            .ForMember(record => record.BookIssueDate, opt => opt.MapFrom(record => record.IssueDate))
            .ForMember(record => record.BookIssueExpirationDate, opt => opt.MapFrom(record => record.ReturnDate));
    }
}