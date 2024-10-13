using AutoMapper;
using Library.Application.Authors.Queries.GetAuthorBooksList;
using Library.Application.Common.Mappings;
using Library.Domain;

namespace Library.Application.Books.Queries;

public class BooksListDto: IMapWith<Book>
{
    public string ISBN { get; set; }
    public string book_name { get; set; }
    public string book_genre { get; set; }
    public string? book_description { get; set; }
    public IList<string> image_urls { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Book, BooksListDto>()
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
                    => opt.MapFrom(book => book.book_description))
            .ForMember(dto
                => dto.image_urls, opt
                => opt.MapFrom(book 
                    => book.imageUrls.Split(' ', StringSplitOptions.RemoveEmptyEntries)));
    }
}