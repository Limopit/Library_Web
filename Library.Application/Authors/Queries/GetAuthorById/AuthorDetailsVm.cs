using AutoMapper;
using Library.Application.Common.Mappings;
using Library.Domain;

namespace Library.Application.Authors.Queries.GetAuthorById;

public class AuthorDetailsVm: IMapWith<Author>
{
    public string author_firstname { get; set; }
    public string author_lastname { get; set; }
    public DateTime? author_birthday { get; set; }
    public string? author_country { get; set; }
    public ICollection<BookListDto> books { get; set; } = new List<BookListDto>();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Author, AuthorDetailsVm>()
            .ForMember(authorVM => authorVM.author_firstname,
                opt
                    => opt.MapFrom(author => author.author_firstname))
            .ForMember(authorVM => authorVM.author_lastname,
                opt
                    => opt.MapFrom(author => author.author_lastname))
            .ForMember(authorVM => authorVM.author_birthday,
                opt
                    => opt.MapFrom(author => author.author_birthday))
            .ForMember(authorVM => authorVM.author_country,
                opt
                    => opt.MapFrom(author => author.author_country))
            .ForMember(authorVM => authorVM.books,
                opt
                    => opt.MapFrom(author => author.books));
    }
}