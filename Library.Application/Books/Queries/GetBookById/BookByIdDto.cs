﻿using AutoMapper;
using Library.Application.Common.Mappings;
using Library.Domain;

namespace Library.Application.Books.Queries.GetBookById;

public class BookByIdDto: IMapWith<Book>
{
    public string ISBN { get; set; }
    public string book_name { get; set; }
    public string book_genre { get; set; }
    public string? book_description { get; set; }
    public IList<string> image_urls { get; set; }
    public AuthorInfoDto author { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Book, BookByIdDto>()
            .ForMember(dto
                => dto.ISBN, opt
                => opt.MapFrom(book => book.ISBN))
            .ForMember(dto
                => dto.book_name, opt
                => opt.MapFrom(book => book.book_name))
            .ForMember(dto
                => dto.book_genre, opt
                => opt.MapFrom(book => book.book_genre))
            .ForMember(dto
                => dto.book_description, opt
                => opt.MapFrom(book => book.book_description))
            .ForMember(dto
                => dto.author, opt
                => opt.MapFrom(book => book.author))
            .ForMember(dto
                => dto.image_urls, opt
                => opt.MapFrom(book 
                    => book.imageUrls.Split(' ', StringSplitOptions.RemoveEmptyEntries)));
    }
}