using AutoMapper;
using Library.Application.Common.Mappings;
using Library.Domain;

namespace Library.Application.Authors.Queries.GetAuthorBooksList;

public class AuthorBookListMapper:IMapWith<Book>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Book, AuthorBooksListDto>()
            .ForMember(bookVm => bookVm.ISBN, opt => opt.MapFrom(book => book.ISBN))
            .ForMember(bookVm => bookVm.BookName, opt => opt.MapFrom(book => book.BookName))
            .ForMember(bookVm => bookVm.BookGenre, opt => opt.MapFrom(book => book.BookGenre))
            .ForMember(bookVm => bookVm.BookDescription, opt => opt.MapFrom(book => book.BookDescription));
    }
}