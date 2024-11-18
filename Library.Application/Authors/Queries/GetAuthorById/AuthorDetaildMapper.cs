using AutoMapper;
using Library.Application.Common.Mappings;
using Library.Domain;

namespace Library.Application.Authors.Queries.GetAuthorById;

public class AuthorDetaildMapper:IMapWith<Author>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Author, AuthorDetailsDto>()
            .ForMember(authorVM => authorVM.AuthorId, opt => opt.MapFrom(author => author.AuthorId))
            .ForMember(authorVM => authorVM.AuthorFirstname, opt => opt.MapFrom(author => author.AuthorFirstname))
            .ForMember(authorVM => authorVM.AuthorLastname, opt => opt.MapFrom(author => author.AuthorLastname))
            .ForMember(authorVM => authorVM.AuthorBirthday, opt => opt.MapFrom(author => author.AuthorBirthday))
            .ForMember(authorVM => authorVM.AuthorCountry, opt => opt.MapFrom(author => author.AuthorCountry))
            .ForMember(authorVM => authorVM.Books, opt => opt.MapFrom(author => author.Books));
    }
}