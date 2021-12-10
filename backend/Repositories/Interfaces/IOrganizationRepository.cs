using System.Linq;
using System.Threading.Tasks;
using ISO810_ERP.Dtos;
using ISO810_ERP.Models;

namespace ISO810_ERP.Repositories.Interfaces
{
    public interface IOrganizationRepository
    {
        public IQueryable<OrganizationDto> GetAll(int accountId);
        public Task<OrganizationDto?> GetById(int accountId, int organizationId);
        public Task<OrganizationDto> Create(int accountId, OrganizationCreate organization);
        public Task<OrganizationDto?> Update(int accountId, int organizationId, OrganizationUpdate organization);
        public Task<OrganizationDto?> Delete(int accountId, int organizationId);
    }
}