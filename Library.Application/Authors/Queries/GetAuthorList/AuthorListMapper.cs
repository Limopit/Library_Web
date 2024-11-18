using AutoMapper;
using Library.Application.Common.Mappings;
using Library.Domain;

namespace Library.Application.Authors.Queries.GetAuthorList;

public class AuthorListMapper: IMapWith<Author>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Author, AuthorListDto>()
            .ForMember(authorVM => authorVM.AuthorId,
                opt => opt.MapFrom(author => author.AuthorId))
            .ForMember(authorVM => authorVM.AuthorFirstname,
                opt => opt.MapFrom(author => author.AuthorFirstname))
            .ForMember(authorVM => authorVM.AuthorLastname,
                opt => opt.MapFrom(author => author.AuthorLastname));
    }
}