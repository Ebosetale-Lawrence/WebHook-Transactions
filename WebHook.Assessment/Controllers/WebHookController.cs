using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebHook.Assessment.Application.DTO;
using WebHook.Assessment.Application.Interface;
using WebHook.Assessment.Application.Models;

namespace WebHook.Assessment.Presentation.Controllers
{



    [Route("api/")]
    [ApiController]

    //[ApiController]
    //[Route("webhooks/transactions")]
    public class WebHookController : ControllerBase
    {
        public readonly ITransactionService _iTransactionService;
        public WebHookController(ITransactionService iTransactionService)
        {
            _iTransactionService = iTransactionService;
        }
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ServerResponse<DerivedResult>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(ErrorResponse))]
        [HttpPost("webhooks/transactions")]
        public async Task<IActionResult> OnboardMerchantCyberSourceCard(TransactionDto request)
        {
            var result = await _iTransactionService.ProcessWebHookAsync(request);
            if (result.IsSuccessful)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
