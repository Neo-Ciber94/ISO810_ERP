
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ISO810_ERP.Dtos;
using ISO810_ERP.Models;
using ISO810_ERP.Repositories.Interfaces;
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


    public async Task<OrganizationDto> Create(OrganizationCreate organization)
    {
        var organizationEntity = mapper.Map<Organization>(organization);

        context.Organizations.Add(organizationEntity);
        await context.SaveChangesAsync();

        return mapper.Map<OrganizationDto>(organizationEntity);
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

    public async Task<OrganizationDto?> Update(int accountId, int organizationId, OrganizationUpdate organization)
    {
        var organizationToUpdate = await context.Organizations
            .Where(o => o.Id == organizationId && o.AccountId == accountId)
            .FirstOrDefaultAsync();

        if (organizationToUpdate == null)
        {
            return null;
        }

        mapper.Map(organization, organizationToUpdate);
        await context.SaveChangesAsync();
        return mapper.Map<OrganizationDto>(organizationToUpdate);
    }
}