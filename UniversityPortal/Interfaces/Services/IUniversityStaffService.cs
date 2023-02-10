﻿using UniversityPortal.Common;
using UniversityPortal.Models;

namespace UniversityPortal.Interfaces.Services
{
    public interface IUniversityStaffService
    {
        Task<UniversityStaffViewModel> Get(Guid id);
        Task<AppResponse> Save(UniversityStaffViewModel model);
        Task<AppResponse> Create(UniversityStaffViewModel model, bool isAdd);
    }
}