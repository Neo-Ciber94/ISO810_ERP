
using ISO810_ERP.Controllers.Abstract;
using ISO810_ERP.Models;
using ISO810_ERP.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ISO810_ERP.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController : ReadOnlyController<Currency>
    {
        public CurrencyController(ICurrencyRepository currencyRepository) : base(currencyRepository) { }
    }
}