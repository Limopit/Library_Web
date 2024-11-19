using Library.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistance;

public class DBInitializer
{
    public static async Task Initialize(LibraryDBContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        context.BorrowRecords.RemoveRange(context.BorrowRecords);
        context.RefreshTokens.RemoveRange(context.RefreshTokens);
        context.Books.RemoveRange(context.Books);
        context.Authors.RemoveRange(context.Authors);
        context.Users.RemoveRange(context.Users);
        await context.Database.MigrateAsync();

        SeedDatabase(context);
        await SeedUsersAndRolesAsync(userManager, roleManager);
    }

    private static void SeedDatabase(LibraryDBContext context)
    {
        var AuthorGuid1 = Guid.NewGuid();
        var AuthorGuid2 = Guid.NewGuid();

        var authors = new[]
        {
            new Author
            {
                AuthorId = AuthorGuid1, AuthorFirstname = "Leo", AuthorLastname = "Tolstoy",
                AuthorCountry = "RE", AuthorBirthday = new DateTime(1828, 9, 9)
            },
            new Author
            {
                AuthorId = AuthorGuid2, AuthorFirstname = "Fyodor", AuthorLastname = "Dostoevsky",
                AuthorCountry = "-", AuthorBirthday = new DateTime(1821, 11, 11)
            }
        };
        context.Authors.AddRange(authors);


        var books = new[]
        {
            new Book
            {
                ISBN = "111-111-111-1111", BookName = "War and Peace", BookGenre = "Novel",
                BookDescription = "some description", AuthorId = AuthorGuid1
            },
            new Book
            {
                ISBN = "111-111-111-1112", BookName = "Crime and Punishment", BookGenre = "Novel",
                BookDescription = "some description", AuthorId = AuthorGuid2
            }
        };
        context.Books.AddRange(books);

        context.SaveChanges();
    }
    
    private static async Task SeedUsersAndRolesAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        string[] roles = { "Admin", "User" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        if (await userManager.FindByEmailAsync("admin@email.com") == null)
        {
            var admin = new User
            {
                FirstName = "Admin",
                LastName = "Admin",
                UserName = "admin",
                Email = "admin@gmail.com",
                Birthday = new DateTime(1999, 2, 20),
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var result = await userManager.CreateAsync(admin, "Admin123#");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }

        if (await userManager.FindByEmailAsync("user@email.com") == null)
        {
            var user = new User
            {
                FirstName = "User",
                LastName = "User",
                UserName = "user",
                Email = "user@gmail.com",
                Birthday = new DateTime(1999, 2, 20),
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var result = await userManager.CreateAsync(user, "Password2#");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "User");
            }
        }
        
    }
}