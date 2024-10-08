using AutoMapper;
using Library.Application.Common.Mappings;
using Library.Domain;

namespace Library.Application.Authors.Queries.GetAuthorDetails;

public class BookListDto: IMapWith<Book>
{
    public Guid book_id { get; set; }
    public string book_name { get; set; }
    public string book_genre { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Book, BookListDto>()
            .ForMember(dto => dto.book_id, opt => opt.MapFrom(book => book.book_id))
            .ForMember(dto => dto.book_name, opt => opt.MapFrom(book => book.book_name))
            .ForMember(dto => dto.book_genre, opt => opt.MapFrom(book => book.book_genre));
    }
}