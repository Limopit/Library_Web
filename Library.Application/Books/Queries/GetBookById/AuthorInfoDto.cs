using AutoMapper;
using Library.Application.Common.Mappings;
using Library.Domain;

namespace Library.Application.Books.Queries.GetBookById;

public class AuthorInfoDto: IMapWith<Author>
{
    public string author_firstname { get; set; }
    public string author_lastname { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Author, AuthorInfoDto>()
            .ForMember(dto
                => dto.author_firstname, opt
                => opt.MapFrom(auth => auth.author_firstname))
            .ForMember(dto
                => dto.author_lastname, opt
                => opt.MapFrom(auth => auth.author_lastname));

    }
}