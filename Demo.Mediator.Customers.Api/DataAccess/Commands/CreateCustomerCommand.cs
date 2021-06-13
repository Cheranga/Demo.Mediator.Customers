using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Demo.Mediator.Customers.Api.Constants;
using Demo.Mediator.Customers.Api.Core;
using Demo.Mediator.Customers.Api.DataAccess.Models;
using MediatR;
using Microsoft.Azure.Cosmos.Table;

namespace Demo.Mediator.Customers.Api.DataAccess.Commands
{
    public class CreateCustomerCommand : CommandBase
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string UserName { get; set; }
    }

    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Result>
    {
        private readonly IMapper _mapper;
        private readonly ITableStorageFactory _tableStorageFactory;

        public CreateCustomerCommandHandler(IMapper mapper, ITableStorageFactory tableStorageFactory)
        {
            _mapper = mapper;
            _tableStorageFactory = tableStorageFactory;
        }
        
        public async Task<Result> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customerDataModel = _mapper.Map<CustomerDataModel>(request);

            var customersTable = await _tableStorageFactory.GetTableAsync("Customers");
            var tableOperation = TableOperation.Insert(customerDataModel);
            var tableOperationResult = await customersTable.ExecuteAsync(tableOperation, cancellationToken);

            if (tableOperationResult.HttpStatusCode == (int) (HttpStatusCode.NoContent))
            {
                return Result.Success();
            }

            return Result.Failure(ErrorCodes.CreateCustomerError, ErrorMessages.CreateCustomerError);
        }
    }
}