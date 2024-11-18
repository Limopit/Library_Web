using AutoMapper;
using Library.Application.Common.Mappings;
using Library.Domain;

namespace Library.Application.Authors.Queries.GetAuthorById;

public class BookListMapper: IMapWith<Book>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Book, BookListDto>()
            .ForMember(dto => dto.BookId, opt => opt.MapFrom(book => book.BookId))
            .ForMember(dto => dto.BookName, opt => opt.MapFrom(book => book.BookName))
            .ForMember(dto => dto.BookGenre, opt => opt.MapFrom(book => book.BookGenre));
    }
}