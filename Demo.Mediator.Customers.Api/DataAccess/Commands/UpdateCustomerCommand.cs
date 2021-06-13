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
    public class UpdateCustomerCommand : CommandBase
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
    }

    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Result>
    {
        private readonly ITableStorageFactory _tableStorageFactory;
        private readonly IMapper _mapper;

        public UpdateCustomerCommandHandler(ITableStorageFactory tableStorageFactory, IMapper mapper)
        {
            _tableStorageFactory = tableStorageFactory;
            _mapper = mapper;
        }
        
        public async Task<Result> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customerDataModel = _mapper.Map<CustomerDataModel>(request);

            var customersTable = await _tableStorageFactory.GetTableAsync("Customers");
            var tableOperation = TableOperation.InsertOrMerge(customerDataModel);
            var tableOperationResult = await customersTable.ExecuteAsync(tableOperation, cancellationToken);

            if (tableOperationResult.HttpStatusCode == (int) (HttpStatusCode.NoContent))
            {
                return Result.Success();
            }

            return Result.Failure(ErrorCodes.UpdateCustomerError, ErrorMessages.UpdateCustomerError);
        }
    }
}