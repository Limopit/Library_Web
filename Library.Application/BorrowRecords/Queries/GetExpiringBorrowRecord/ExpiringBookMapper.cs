using AutoMapper;
using Library.Application.Common.Mappings;
using Library.Domain;

namespace Library.Application.BorrowRecords.Queries.GetExpiringBorrowRecord;

public class ExpiringBookMapper: IMapWith<Book>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Book, ExpiringBookDto>()
            .ForMember(dto => dto.ISBN, opt => opt.MapFrom(book => book.ISBN))
            .ForMember(dto => dto.BookName, opt => opt.MapFrom(book => book.BookName))
            .ForMember(dto => dto.BookGenre, opt => opt.MapFrom(book => book.BookGenre))
            .ForMember(dto => dto.BookDescription, opt => opt.MapFrom(book => book.BookDescription))
            .ForMember(dto => dto.Record, opt => opt.MapFrom(book => book.BorrowRecords
                    .OrderByDescending(r => r.BookIssueExpirationDate)
                    .FirstOrDefault(r => r.BookIssueExpirationDate > DateTime.UtcNow)))
            .ForMember(dto => dto.ImageUrls, opt => opt.MapFrom(book => book.ImageUrls.Split(' ', StringSplitOptions.RemoveEmptyEntries)));
    }
}