﻿using Library.Domain;
using Microsoft.AspNetCore.Identity;

namespace Library.Persistance.Interfaces;

public interface ITokenService
{
    Task<(string accessToken, string refreshToken)> GenerateTokens(User user,
        UserManager<User> userManager, CancellationToken token);
}