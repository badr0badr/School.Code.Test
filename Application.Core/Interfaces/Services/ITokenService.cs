﻿using Application.Core.Entities;
using System;
using System.Linq;

namespace Application.Core.Interfaces.Services
{
    public interface ITokenService
    {
        string CreateToken(Teacher Teacher);
        Teacher? GetTeacherFromToken(string token);
        string CreateToken(long Id);
        long? GetPerantFromToken(string token);
    }
}