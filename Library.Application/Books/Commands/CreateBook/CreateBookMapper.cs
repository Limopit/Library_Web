using AutoMapper;
using Library.Application.Common.Mappings;
using Library.Domain;

namespace Library.Application.Books.Commands.CreateBook;

public class CreateBookMapper: IMapWith<Book>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateBookCommand, Book>()
            .ForMember(book => book.AuthorId, opt => opt.MapFrom(book => book.AuthorId))
            .ForMember(book => book.BookName, opt => opt.MapFrom(book => book.BookName))
            .ForMember(book => book.BookGenre, opt => opt.MapFrom(book => book.BookGenre))
            .ForMember(book => book.BookDescription, opt => opt.MapFrom(book => book.BookDescription))
            .ForMember(book => book.ISBN, opt => opt.MapFrom(book => book.ISBN))
            .ForMember(book => book.ImageUrls, opt => opt.MapFrom(book => book.ImageUrls));
    }
}