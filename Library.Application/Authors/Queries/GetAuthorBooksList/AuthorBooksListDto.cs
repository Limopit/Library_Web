using AutoMapper;
using Library.Application.Common.Mappings;
using Library.Domain;

namespace Library.Application.Authors.Queries.GetAuthorBooksList;

public class AuthorBooksListDto: IMapWith<Book>
{
    public string ISBN { get; set; }
    public string book_name { get; set; }
    public string book_genre { get; set; }
    public string? book_description { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Book, AuthorBooksListDto>()
            .ForMember(bookVm => bookVm.ISBN,
                opt
                    => opt.MapFrom(book => book.ISBN))
            .ForMember(bookVm => bookVm.book_name,
                opt
                    => opt.MapFrom(book => book.book_name))
            .ForMember(bookVm => bookVm.book_genre,
                opt
                    => opt.MapFrom(book => book.book_genre))
            .ForMember(bookVm => bookVm.book_description,
                opt
                    => opt.MapFrom(book => book.book_description));
    }
}