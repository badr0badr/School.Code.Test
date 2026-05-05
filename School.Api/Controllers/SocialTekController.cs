﻿using Application.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace School.Api.Controllers
{
    public class SocialTekController : BaseApiController
    {
        private readonly ISocialTekService socialTekService;

        public SocialTekController(ISocialTekService _socialTekService)
        {
            socialTekService = _socialTekService;
        }
    }
}