using Scripters.Regula.Platform.CommercialManagement.Application.CommandServices;
using Scripters.Regula.Platform.CommercialManagement.Domain.Errors;
using Scripters.Regula.Platform.CommercialManagement.Domain.Model.Commands;
using Scripters.Regula.Platform.CommercialManagement.Domain.Model.Entities;
using Scripters.Regula.Platform.CommercialManagement.Domain.Repositories;
using Scripters.Regula.Platform.Shared.Application.Model;
using Scripters.Regula.Platform.Shared.Domain.Repositories;

namespace Scripters.Regula.Platform.CommercialManagement.Application.Internal.CommandServices;

public class CustomerDebtCommandService(
    ICommercialCustomerRepository customerRepository,
    ICommercialDebtRepository debtRepository,
    IUnitOfWork unitOfWork) : ICustomerDebtCommandService
{
    public async Task<Result<CommercialDebt>> Handle(
        CreateCustomerDebtCommand command,
        CancellationToken cancellationToken = default)
    {
        if (command.Amount <= 0)
            return Result<CommercialDebt>.Failure(
                CommercialManagementErrors.InvalidDebtAmount,
                "Debt amount must be greater than zero.");

        if (string.IsNullOrWhiteSpace(command.Description))
            return Result<CommercialDebt>.Failure(
                CommercialManagementErrors.InvalidDebtDescription,
                "Debt description is required.");

        var customer = await customerRepository.FindByIdAsync(command.CustomerId, cancellationToken);

        if (customer is null)
            return Result<CommercialDebt>.Failure(
                CommercialManagementErrors.CustomerNotFound,
                $"Customer with id {command.CustomerId} was not found.");

        var debt = new CommercialDebt(
            command.CustomerId,
            command.Amount,
            command.Description.Trim(),
            command.DueDate);

        customer.IncreaseActiveDebt(command.Amount);

        await debtRepository.AddAsync(debt, cancellationToken);
        customerRepository.Update(customer);

        await unitOfWork.CompleteAsync(cancellationToken);

        return Result<CommercialDebt>.Success(debt);
    }
}