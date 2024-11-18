using AutoMapper;
using Library.Application.Common.Mappings;
using Library.Domain;

namespace Library.Application.Books.Queries.GetBookByISBN;

public class BookByISBNMapper: IMapWith<Book>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Book, BookByISBNDto>()
            .ForMember(dto => dto.ISBN, opt => opt.MapFrom(book => book.ISBN))
            .ForMember(dto => dto.BookName, opt => opt.MapFrom(book => book.BookName))
            .ForMember(dto => dto.BookGenre, opt => opt.MapFrom(book => book.BookGenre))
            .ForMember(dto => dto.BookDescription, opt => opt.MapFrom(book => book.BookDescription))
            .ForMember(dto => dto.Author, opt => opt.MapFrom(book => book.Author))
            .ForMember(dto => dto.ImageUrls, opt => opt.MapFrom(book => book.ImageUrls.Split(' ', StringSplitOptions.RemoveEmptyEntries)));
    }
}