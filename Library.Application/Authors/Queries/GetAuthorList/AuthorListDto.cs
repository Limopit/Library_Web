using AutoMapper;
using Library.Application.Common.Mappings;
using Library.Domain;

namespace Library.Application.Authors.Queries.GetAuthorList;

public class AuthorListDto: IMapWith<Author>
{
    public Guid AuthorId { get; set; }
    public string AuthorFirstname { get; set; }
    public string AuthorLastname { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Author, AuthorListDto>()
            .ForMember(authorVM => authorVM.AuthorId,
                opt
                    => opt.MapFrom(author => author.author_id))
            .ForMember(authorVM => authorVM.AuthorFirstname,
                opt
                    => opt.MapFrom(author => author.author_firstname))
            .ForMember(authorVM => authorVM.AuthorLastname,
                opt
                    => opt.MapFrom(author => author.author_lastname));
    }
}