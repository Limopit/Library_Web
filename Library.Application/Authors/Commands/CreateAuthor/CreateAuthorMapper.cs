using AutoMapper;
using Library.Application.Common.Mappings;
using Library.Domain;

namespace Library.Application.Authors.Commands.CreateAuthor;

public class CreateAuthorMapper: IMapWith<Author>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateAuthorCommand, Author>()
            .ForMember(author => author.AuthorFirstname, opt => opt.MapFrom(author => author.AuthorFirstname))
            .ForMember(author => author.AuthorLastname, opt => opt.MapFrom(author => author.AuthorLastname))
            .ForMember(author => author.AuthorCountry, opt => opt.MapFrom(author => author.AuthorCountry))
            .ForMember(author => author.AuthorBirthday, opt => opt.MapFrom(author => author.AuthorBirthday))
            .ForMember(author => author.Books, opt => opt.MapFrom(author => author.Books));
    }
}