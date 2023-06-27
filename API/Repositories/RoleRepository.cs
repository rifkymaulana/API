﻿using API.Contracts;
using API.Contracts.IRepositories;
using API.Data;
using API.Models;

namespace API.Repositories;

public class RoleRepository : BaseRepository<Role>, IRoleRepository
{
    public RoleRepository(ApplicationDbContext context) : base(context)
    {
    }
}