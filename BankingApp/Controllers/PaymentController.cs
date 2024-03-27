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

        public PaymentController(IHttpContextAccessor httpContextAccessor, IPaymentService paymentService,
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
            ExpressPaymentViewModel vm = new();

            vm.LoggedUserAccounts = await _savingsAccountService.GetAllByClientId(loggedClient.Id);
            return View("ExpressPayment", vm);
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

            return View("ExpressPaymentConfirm", vm);
        }

        public async Task<IActionResult> ExpressPaymentConfirm(ExpressPaymentViewModel vm)
        {
            var loggedClient = await _clientService.GetByUserIdViewModel(user.Id);
            var destinyClient = await _clientService.GetByAccountNumber(vm.Destination);
            var destinyUser = await _accountService.FindByIdAsync(destinyClient.UserId);

            vm.ClientName = destinyUser.Name;
            vm.ClientLastName = destinyUser.LastName;
            return View("ExpressPaymentConfirm", vm);
        }

        [HttpPost]

        public async Task<IActionResult> ExpressPaymentConfirmPost(ExpressPaymentViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                var loggedClient = await _clientService.GetByUserIdViewModel(user.Id);
                vm.LoggedUserAccounts = await _savingsAccountService.GetAllByClientId(loggedClient.Id);
                return View("ExpressPaymentConfirm", vm);
            }

            await _paymentService.ExpressPayment(vm);
            return RedirectToRoute(new { controller = "Payment", action = "ExpressPayment" });
        }

        public async Task<IActionResult> BeneficiaryPayment()
        {
            var loggedClient = await _clientService.GetByUserIdViewModel(user.Id);
            BeneficiaryPaymentViewModel vm = new();
            vm.LoggedUserAccounts = await _savingsAccountService.GetAllByClientId(loggedClient.Id);
            vm.LoggedUserBeneficiaries = await _beneficiaryService.GetAllByClientId(loggedClient.Id);
            return View("BeneficiaryPayment", vm);

        }

        [HttpPost]
        public async Task<IActionResult> BeneficiaryPayment(BeneficiaryPaymentViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                var loggedClient = await _clientService.GetByUserIdViewModel(user.Id);
                vm.LoggedUserAccounts = await _savingsAccountService.GetAllByClientId(loggedClient.Id);
                vm.LoggedUserBeneficiaries = await _beneficiaryService.GetAllByClientId(loggedClient.Id);
                return View("BeneficiaryPayment", vm);
            }


            return View("BeneficiaryPaymentConfirm", vm);
        }

        public async Task<IActionResult> BeneficiaryPaymentConfirm(BeneficiaryPaymentViewModel vm)
        {
            var loggedClient = await _clientService.GetByUserIdViewModel(user.Id);
            var destinyClient = await _clientService.GetByAccountNumber(vm.Destination);
            var destinyUser = await _accountService.FindByIdAsync(destinyClient.UserId);

            vm.BeneficiaryFirstName = destinyUser.Name;
            vm.BeneficiaryLastName = destinyUser.LastName;

            return View("BeneficiaryPaymentConfirm", vm);
        }

        [HttpPost]

        public async Task<IActionResult> BeneficiaryPaymentConfirmPost(BeneficiaryPaymentViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                var loggedClient = await _clientService.GetByUserIdViewModel(user.Id);
                vm.LoggedUserAccounts = await _savingsAccountService.GetAllByClientId(loggedClient.Id);
                vm.LoggedUserBeneficiaries = await _beneficiaryService.GetAllByClientId(loggedClient.Id);
                return View("BeneficiaryPaymentConfirm", vm);
            }



            await _paymentService.BeneficiaryPayment(vm);
            return RedirectToRoute(new { controller = "Payment", action = "BeneficiaryPayment" });
        }
        public async Task<IActionResult> LoanPayment()
        {
            var loggedClient = await _clientService.GetByUserIdViewModel(user.Id);
            LoanPaymentViewModel vm = new();
            vm.LoggedUserAccounts = await _savingsAccountService.GetAllByClientId(loggedClient.Id);
            vm.LoggedUserLoans = await _loanService.GetAllByClientId(loggedClient.Id);
            return View("LoanPayment", vm);
        }

        [HttpPost]
        public async Task<IActionResult> LoanPayment(LoanPaymentViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                var loggedClient = await _clientService.GetByUserIdViewModel(user.Id);
                vm.LoggedUserAccounts = await _savingsAccountService.GetAllByClientId(loggedClient.Id);
                vm.LoggedUserLoans = await _loanService.GetAllByClientId(loggedClient.Id);
                return View("LoanPayment", vm);
            }
            await _paymentService.LoanPayment(vm);
            return RedirectToRoute(new { controller = "Payment", action = "LoanPayment" });
        }
        public async Task<IActionResult> CreditCardPayment()
        {
            var loggedClient = await _clientService.GetByUserIdViewModel(user.Id);
            CreditCardPaymentViewModel vm = new();
            vm.LoggedUserAccounts = await _savingsAccountService.GetAllByClientId(loggedClient.Id);
            vm.LoggedUserCreditCards = await _creditCardService.GetAllByClientId(loggedClient.Id);
            return View("CreditCardPayment", vm);
        }

        [HttpPost]
        public async Task<IActionResult> CreditCardPayment(CreditCardPaymentViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                var loggedClient = await _clientService.GetByUserIdViewModel(user.Id);

                vm.LoggedUserAccounts = await _savingsAccountService.GetAllByClientId(loggedClient.Id);
                vm.LoggedUserCreditCards = await _creditCardService.GetAllByClientId(loggedClient.Id);
                return View("CreditCardPayment", vm);
            }



            return View("CreditCardConfirm", vm);
        }

    }
}
