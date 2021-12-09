
using ISO810_ERP.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ISO810_ERP.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyRepository currencyRepository;
        public CurrencyController(ICurrencyRepository currencyRepository)
        {
            this.currencyRepository = currencyRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(currencyRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var currency = currencyRepository.GetById(id);

            if (currency == null)
            {
                return NotFound();
            }

            return Ok(currency);
        }
    }
}