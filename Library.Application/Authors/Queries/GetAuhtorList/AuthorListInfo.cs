using AutoMapper;
using Library.Application.Common.Mappings;
using Library.Domain;

namespace Library.Application.Authors.Queries.GetAuhtorList;

public class AuthorListInfo: IMapWith<Author>
{
    public Guid author_id { get; set; }
    public string author_firstname { get; set; }
    public string author_lastname { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Author, AuthorListInfo>()
            .ForMember(authorVM => authorVM.author_id,
                opt
                    => opt.MapFrom(author => author.author_id))
            .ForMember(authorVM => authorVM.author_firstname,
                opt
                    => opt.MapFrom(author => author.author_firstname))
            .ForMember(authorVM => authorVM.author_lastname,
                opt
                    => opt.MapFrom(author => author.author_lastname));
    }
}