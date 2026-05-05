﻿using Application.Core.Interfaces.Services;
using Application.Core.Interfaces.UnitOfWork;
using Application.Repository;
using Application.Repository.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Services
{
    public class SocialTekService : ISocialTekService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly AppDbContext context;
        public SocialTekService(IUnitOfWork _unitOfWork, AppDbContext _context)
        {
            unitOfWork = _unitOfWork;
            context = _context;
        }
    }
}