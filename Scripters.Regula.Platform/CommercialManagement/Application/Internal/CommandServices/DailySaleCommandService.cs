using Scripters.Regula.Platform.CommercialManagement.Application.CommandServices;
using Scripters.Regula.Platform.CommercialManagement.Domain.Errors;
using Scripters.Regula.Platform.CommercialManagement.Domain.Model.Commands;
using Scripters.Regula.Platform.CommercialManagement.Domain.Model.Entities;
using Scripters.Regula.Platform.CommercialManagement.Domain.Model.ValueObjects;
using Scripters.Regula.Platform.CommercialManagement.Domain.Repositories;
using Scripters.Regula.Platform.CommercialManagement.Domain.Model.Aggregates;
using Scripters.Regula.Platform.Shared.Application.Model;
using Scripters.Regula.Platform.Shared.Domain.Repositories;

namespace Scripters.Regula.Platform.CommercialManagement.Application.Internal.CommandServices;

public class DailySaleCommandService(
    ICommercialDailySaleRepository dailySaleRepository,
    ICommercialCustomerRepository customerRepository,
    ICommercialDebtRepository debtRepository,
    IUnitOfWork unitOfWork) : IDailySaleCommandService
{
    public async Task<Result<CommercialDailySale>> Handle(
        CreateDailySaleCommand command,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(command.CylinderTypeId) || string.IsNullOrWhiteSpace(command.CylinderType))
            return Result<CommercialDailySale>.Failure(
                CommercialManagementErrors.InvalidCylinderType,
                "Cylinder type is required.");

        if (command.Quantity <= 0)
            return Result<CommercialDailySale>.Failure(
                CommercialManagementErrors.InvalidSaleQuantity,
                "Sale quantity must be greater than zero.");

        if (command.UnitPrice <= 0)
            return Result<CommercialDailySale>.Failure(
                CommercialManagementErrors.InvalidUnitPrice,
                "Unit price must be greater than zero.");

        if (!TryParsePaymentType(command.PaymentType, out var paymentType))
            return Result<CommercialDailySale>.Failure(
                CommercialManagementErrors.InvalidPaymentType,
                "Payment type is invalid.");

        CommercialCustomer? customer = null;

        if (command.CustomerId.HasValue)
        {
            customer = await customerRepository.FindByIdAsync(command.CustomerId.Value, cancellationToken);

            if (customer is null)
                return Result<CommercialDailySale>.Failure(
                    CommercialManagementErrors.CustomerNotFound,
                    $"Customer with id {command.CustomerId.Value} was not found.");
        }

        if (paymentType == ECommercialPaymentType.Debt && customer is null)
            return Result<CommercialDailySale>.Failure(
                CommercialManagementErrors.CustomerRequiredForDebtSale,
                "Customer is required when payment type is debt.");

        var sale = new CommercialDailySale(
            command.CylinderTypeId.Trim(),
            command.CylinderType.Trim(),
            command.Quantity,
            command.UnitPrice,
            paymentType,
            command.CustomerId,
            string.IsNullOrWhiteSpace(command.CustomerName) ? customer?.Name : command.CustomerName.Trim(),
            command.DistributorName);

        await dailySaleRepository.AddAsync(sale, cancellationToken);

        if (sale.IsDebtSale())
        {
            var debt = new CommercialDebt(
                customer!.Id,
                sale.TotalAmount,
                sale.BuildDebtDescription(),
                null);

            customer.IncreaseActiveDebt(sale.TotalAmount);

            await debtRepository.AddAsync(debt, cancellationToken);
            customerRepository.Update(customer);
        }

        await unitOfWork.CompleteAsync(cancellationToken);

        return Result<CommercialDailySale>.Success(sale);
    }

    private static bool TryParsePaymentType(string paymentType, out ECommercialPaymentType result)
    {
        result = ECommercialPaymentType.Cash;

        if (string.IsNullOrWhiteSpace(paymentType))
            return false;

        var normalized = paymentType.Trim().ToUpperInvariant()
            .Replace(" ", string.Empty)
            .Replace("/", string.Empty)
            .Replace("_", string.Empty)
            .Replace("-", string.Empty);

        return normalized switch
        {
            "CASH" or "EFECTIVO" => Set(ECommercialPaymentType.Cash, out result),
            "YAPEPLIN" or "YAPE" or "PLIN" or "DIGITAL" => Set(ECommercialPaymentType.YapePlin, out result),
            "TRANSFER" or "TRANSFERENCIA" or "BANKTRANSFER" => Set(ECommercialPaymentType.Transfer, out result),
            "DEBT" or "DEUDA" or "FIADO" or "CREDIT" => Set(ECommercialPaymentType.Debt, out result),
            _ => false
        };
    }

    private static bool Set(ECommercialPaymentType value, out ECommercialPaymentType result)
    {
        result = value;
        return true;
    }
}