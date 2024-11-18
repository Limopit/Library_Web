using AutoMapper;
using Library.Application.Common.Mappings;
using Library.Domain;

namespace Library.Application.Books.Queries.GetBooksList;

public class BookListMapper: IMapWith<Book>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Book, BooksListDto>()
            .ForMember(bookVm => bookVm.BookId, opt => opt.MapFrom(book => book.BookId))
            .ForMember(bookVm => bookVm.ISBN, opt => opt.MapFrom(book => book.ISBN))
            .ForMember(bookVm => bookVm.BookName, opt => opt.MapFrom(book => book.BookName))
            .ForMember(bookVm => bookVm.BookGenre, opt => opt.MapFrom(book => book.BookGenre))
            .ForMember(bookVm => bookVm.BookDescription, opt => opt.MapFrom(book => book.BookDescription))
            .ForMember(dto => dto.ImageUrls, opt => opt.MapFrom(book => book.ImageUrls.Split(' ', StringSplitOptions.RemoveEmptyEntries)));
    }
}