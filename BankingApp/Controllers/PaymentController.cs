using BankingApp.Core.Application.DTOs.Account;
using BankingApp.Core.Application.Helpers;
using BankingApp.Core.Application.Interfaces.Services;
using BankingApp.Core.Application.Services;
using BankingApp.Core.Application.ViewModels.Payment;
using BankingApp.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse user;
        private readonly IPaymentService _paymentService;
        private readonly ISavingsAccountService _savingsAccountService;
        private readonly ICreditCardService _creditCardService;
        private readonly ILoanService _loanService;
        private readonly IBeneficiaryService _beneficiaryService;
        private readonly IAccountService _accountService;
        private readonly IClientService _clientService;

        public PaymentController(IHttpContextAccessor httpContextAccessor, AuthenticationResponse user, IPaymentService paymentService,
            ISavingsAccountService savingsAccountService, ICreditCardService creditCardService, ILoanService loanService,
            IBeneficiaryService beneficiaryService, IClientService clientService, IAccountService accountService)
        {
            _savingsAccountService = savingsAccountService;
            _accountService = accountService;
            _clientService = clientService;
            _creditCardService = creditCardService;
            _loanService = loanService;
            _beneficiaryService = beneficiaryService;
            _httpContextAccessor = httpContextAccessor;
            _paymentService = paymentService;
            user = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public async Task<IActionResult> ExpressPayment()
        {
            var loggedClient = await _clientService.GetByUserIdViewModel(user.Id);
            ViewBag.Accounts = await _savingsAccountService.GetAllByClientId(loggedClient.Id);
            return View(ViewBag.Accounts);
        }

        [HttpPost]
        public async Task<IActionResult> ExpressPayment(ExpressPaymentViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                var loggedClient = await _clientService.GetByUserIdViewModel(user.Id);
                vm.LoggedUserAccounts = await _savingsAccountService.GetAllByClientId(loggedClient.Id);
                return View("ExpressPayment", vm);
            }

            await _paymentService.ExpressPayment(vm);
            return RedirectToRoute(new { controller = "Payment", action = "ExpressPayment" });
        }

        public async Task<IActionResult> BeneficiaryPayment()
        {
            var loggedClient = await _clientService.GetByUserIdViewModel(user.Id);
            ViewBag.Accounts = await _savingsAccountService.GetAllByClientId(loggedClient.Id);

            return View(await _beneficiaryService.GetAllByClientId(loggedClient.Id));

        }

        [HttpPost]
        public async Task<IActionResult> BeneficiaryPayment(BeneficiaryPaymentViewModel vm)
        {
            await _paymentService.BeneficiaryPayment(vm);
            return View("BeneficiaryPayment", vm);
        }
        public async Task<IActionResult> LoanPayment()
        {
            var loggedClient = await _clientService.GetByUserIdViewModel(user.Id);
            ViewBag.Accounts = await _savingsAccountService.GetAllByClientId(loggedClient.Id);
            return View(await _loanService.GetAllByClientId(loggedClient.Id));
        }

        [HttpPost]
        public async Task<IActionResult> LoanPayment(LoanPaymentViewModel vm)
        {
            await _paymentService.LoanPayment(vm);
            return View("LoanPayment", vm);
        }
        public async Task<IActionResult> CreditCardPayment()
        {
            var loggedClient = await _clientService.GetByUserIdViewModel(user.Id);
            ViewBag.Accounts = await _savingsAccountService.GetAllByClientId(loggedClient.Id);
            return View(await _creditCardService.GetAllByClientId(loggedClient.Id));
        }

        [HttpPost]
        public async Task<IActionResult> CreditCardPayment(CreditCardPaymentViewModel vm)
        {
            await _paymentService.CreditCardPayment(vm);
            return View("CreditCardPayment", vm);
        }
    }
}
