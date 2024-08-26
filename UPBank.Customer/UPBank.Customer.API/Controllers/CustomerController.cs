using MediatR;
using Microsoft.AspNetCore.Mvc;
using UPBank.Customer.Domain.Commands.CreateCustomer;
using UPBank.Customer.Domain.Commands.DeleteCustomer;
using UPBank.Customer.Domain.Queries.GetAllCustomers;
using UPBank.Customer.Domain.Queries.GetCustomerByCPF;
using UPBank.Utils.CrossCutting.Exception.Contracts;

namespace UPBank.Customer.API.Controllers
{
    public class CustomerController : Controller
    {
        //private readonly ILogger<CustomerController> _logger;
        private readonly IDomainNotificationService _domainNotificationService;
        private readonly IMediator _bus;

        public CustomerController(IDomainNotificationService domainNotificationService, IMediator bus)
        {
            _domainNotificationService = domainNotificationService;
            _bus = bus;
        }

        [HttpPost("api/customers")]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand createCustomerCommand)
        {
            var customer = await _bus.Send(createCustomerCommand);

            if (_domainNotificationService.HasNotification)
                return BadRequest(_domainNotificationService.Get());

            return Ok(customer);
        }

        [HttpGet("api/customers/{cpf}")]
        public async Task<IActionResult> GetCustomer(string cpf)
        {
            var response = await _bus.Send(new GetCustomerByCPFQuery(cpf));

            if (_domainNotificationService.HasNotification)
                return BadRequest(_domainNotificationService.Get());

            return Ok(response);
        }

        [HttpDelete("api/customers/{cpf}")]
        public async Task<IActionResult> DeleteCustomer(string cpf)
        {
            var ok = await _bus.Send(new DeleteCustomerCommand(cpf));
            if (_domainNotificationService.HasNotification)
                return BadRequest(_domainNotificationService.Get());

            return Ok("Cliente deletado com sucesso!");
        }

        [HttpGet("api/customers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var response = await _bus.Send(new GetAllCustomerQuery());

            if (_domainNotificationService.HasNotification)
            {
                var notifications = _domainNotificationService.Get();
                if (notifications.Equals("Não há clientes para mostrar"))
                    return Ok(notifications);
                else
                    return BadRequest(notifications);
            }

            return Ok(response);

        }

        [HttpPatch("api/customers/{cpf}")]
        public async Task<IActionResult> UpdateCustomer(string cpf, [FromBody] PersonPatchDTO personPatchDTO)
        {
            var customer = await _customerService.UpdateCustomer(cpf, personPatchDTO);

            if (customer.message == "cliente com restrição!")
                return Forbid(customer.message);

            if (customer.message != null)
                return BadRequest(customer.message);

            return Ok(customer);
        }


        //[HttpPatch("api/customers/patchRestriction/{cpf}")]
        //public async Task<IActionResult> CustomerPatchRestriction(string cpf)
        //{
        //    var customer = await _customerService.CustomerPatchRestriction(cpf);

        //    if (customer.customerOutputodel == null)
        //        return BadRequest(customer.message);

        //    return Ok(customer);
        //}

        //[HttpGet("api/customers/restriction")]
        //public async Task<IActionResult> GetAllCustomerWithRestriction()
        //{
        //    var customers = await _customerService.GetAllCustomersWithRestriction();

        //    if (customers.customers == null)
        //        return BadRequest(customers.message);

        //    return Ok(customers);
        //}

        //[HttpPost("api/customers/accountOpening")]
        //public async Task<IActionResult> AccountOpening(List<string> cpfs)
        //{
        //    var account = await _customerService.AccountOpening(cpfs);

        //    if (account.ok == null)
        //    {
        //        if (account.message == "cliente com restrição!")
        //            return Forbid(account.message);
        //        else
        //            return BadRequest(account.message);
        //    }

        //    return Ok("Conta solicitada com sucesso! Aguarde para ter mais informações");
        //}


    }
}