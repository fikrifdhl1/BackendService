using BackendService.Models.DTO;
using BackendService.Services;
using BackendService.Utils.Logger;
using BackendService.Validators.Transaction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace BackendService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly ICustomeLogger _logger;
        private readonly ITransactionValidator _validator;


        public TransactionController(ITransactionService transactionService, ICustomeLogger logger, ITransactionValidator validator)
        {
            _transactionService = transactionService;
            _logger = logger;
            _validator = validator; 
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactions()
        {
            _logger.Log($"Starting {this}.{nameof(GetTransactions)}", LogLevel.Information);
            var transactions = await  _transactionService.GetAllAsync();

            var response = transactions.Select(x => new TransactionDTO
            {
                CartId= x.CartId,
                Id= x.Id,
                UserId = x.UserId,
                TotalPrice  = x.TotalPrice,
            }).ToList();

            return Ok(new ApiResponse<List<TransactionDTO>>
            {
                Code = (int)HttpStatusCode.OK,
                Message = "Success get transactions",
                Data = response
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionDTO transaction)
        {
            _logger.Log($"Starting {this}.{nameof(CreateTransaction)}", LogLevel.Information);
            var validator = _validator.CreateTransaction().Validate(transaction);
            if (!validator.IsValid)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    Message = "Validation Error",
                    ErrorDetails = validator.ToString()
                });
            }

            await _transactionService.CreateAsync(transaction);
            return Ok(new ApiResponse<object>
            {
                Code = (int)HttpStatusCode.OK,
                Message = "Success to create transaction"
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionsById([FromRoute] int id)
        {
            _logger.Log($"Starting {this}.{nameof(GetTransactionsById)}", LogLevel.Information);
            var transaction = await _transactionService.GetByIdAsync(id);

            var response = new TransactionDTO
            {
                Id = transaction.Id,
                CartId = transaction.CartId,
                UserId = transaction.UserId,
                TotalPrice = transaction.TotalPrice
            };

            return Ok(new ApiResponse<TransactionDTO>
            {
                Code = (int)HttpStatusCode.OK,
                Message = "Success get transaction",
                Data = response
            });
        }
    }
}
