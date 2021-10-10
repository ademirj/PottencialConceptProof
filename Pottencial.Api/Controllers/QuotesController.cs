using Microsoft.AspNetCore.Mvc;
using Pottencial.Domain.Dto;
using Pottencial.Domain.Interface;

namespace Pottencial.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuotesController : ControllerBase
    {
        private readonly IQuoteService _quoteService;
        public QuotesController(IQuoteService quoteService)
        {
            _quoteService = quoteService;
        }

        [HttpPost("insuranceLegalPerson")]
        public void QuotingInsuranceLegalPerson([FromBody] CostumerLegalPersonDto costumerLegalPerson)
        {
            _quoteService.QuotingInsuranceLegalPerson(null);
        }

        [HttpPost("insurancePhysicalPerson")]
        public void QuotingInsuranceLegalPerson([FromBody] CostumerPhysicalPersonDto costumerPhysicalPerson)
        {
            _quoteService.QuotingInsurancePhysicalPerson(null);
        }

        [HttpGet]
        public string Get()
        {
            return "Teste";
        }
    }
}
