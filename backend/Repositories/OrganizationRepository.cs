
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ISO810_ERP.Dtos;
using ISO810_ERP.Extensions;
using ISO810_ERP.Models;
using ISO810_ERP.Repositories.Interfaces;
using ISO810_ERP.Utils;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace ISO810_ERP.Repositories;

public class OrganizationRepository : IOrganizationRepository
{
    private readonly ErpDbContext context;
    private readonly IMapper mapper;

    public OrganizationRepository(ErpDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public IQueryable<OrganizationDto> GetAll()
    {
        return context.Organizations.Select(o => mapper.Map<OrganizationDto>(o));
    }

    public async Task<OrganizationDto?> GetById(int accountId, int organizationId)
    {
        var organization = await context.Organizations
            .Where(o => o.Id == organizationId && o.AccountId == accountId)
            .FirstOrDefaultAsync();

        if (organization == null)
        {
            return null;
        }

        return mapper.Map<OrganizationDto>(organization);
    }

    public async Task<OrganizationDto> Create(int accountId, OrganizationInput organization)
    {
        var organizationEntity = mapper.Map<Organization>(organization);
        organizationEntity.AccountId = accountId;

        var result = context.Organizations.Add(organizationEntity);
        await context.SaveChangesAsync();

        return mapper.Map<OrganizationDto>(result.Entity);
    }

    public async Task<OrganizationDto?> Update(int accountId, int organizationId, OrganizationInput organization)
    {
        var organizationToUpdate = await context.Organizations
            .Where(o => o.Id == organizationId && o.AccountId == accountId)
            .FirstOrDefaultAsync();

        if (organizationToUpdate == null)
        {
            return null;
        }

        ObjectUtils.UpdateNonNullProperties(organization, organizationToUpdate);
        var result = context.Update(organizationToUpdate);

        Trace.WriteLine(organizationToUpdate.ToJson());

        await context.SaveChangesAsync();
        return mapper.Map<OrganizationDto>(result.Entity);
    }

    public async Task<OrganizationDto?> Delete(int accountId, int organizationId)
    {
        var organizationToDelete = await context.Organizations
            .Where(o => o.Id == organizationId && o.AccountId == accountId)
            .FirstOrDefaultAsync();

        if (organizationToDelete == null)
        {
            return null;
        }

        context.Organizations.Remove(organizationToDelete);
        await context.SaveChangesAsync();

        return mapper.Map<OrganizationDto>(organizationToDelete);
    }
}