using AutoMapper;
using Library.Application.Common.Mappings;
using Library.Domain;

namespace Library.Application.Authors.Commands.UpdateAuthor;

public class UpdateAuthorMapping: IMapWith<Author>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateAuthorCommand, Author>()
            .ForMember(author => author.AuthorFirstname, opt => opt.MapFrom(author => author.AuthorFirstname))
            .ForMember(author => author.AuthorLastname, opt => opt.MapFrom(author => author.AuthorLastname))
            .ForMember(author => author.AuthorCountry, opt => opt.MapFrom(author => author.AuthorCountry))
            .ForMember(author => author.AuthorBirthday, opt => opt.MapFrom(author => author.AuthorBirthday));
    }
}