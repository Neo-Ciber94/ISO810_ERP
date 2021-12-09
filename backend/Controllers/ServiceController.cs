
using AutoMapper;
using ISO810_ERP.Controllers.Abstract;
using ISO810_ERP.Models;
using ISO810_ERP.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ISO810_ERP.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ServiceController : ReadOnlyController<Service>
{
    public ServiceController(IServiceRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }
}