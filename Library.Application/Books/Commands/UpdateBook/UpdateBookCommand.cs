﻿using MediatR;

namespace Library.Application.Books.Commands.UpdateBook;

public class UpdateBookCommand: IRequest
{
    public Guid book_id { get; set; }
    public string ISBN { get; set; }
    public string book_name { get; set; }
    public string book_genre { get; set; }
    public string? book_description { get; set; }
    public string? imageUrls { get; set; }
    public Guid author_id { get; set; }
    
}