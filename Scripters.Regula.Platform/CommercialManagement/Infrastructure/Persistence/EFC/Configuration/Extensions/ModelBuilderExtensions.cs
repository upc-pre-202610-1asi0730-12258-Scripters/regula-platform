using Microsoft.EntityFrameworkCore;
using Scripters.Regula.Platform.CommercialManagement.Domain.Model.Aggregates;
using Scripters.Regula.Platform.CommercialManagement.Domain.Model.Entities;
using Scripters.Regula.Platform.CommercialManagement.Domain.Model.ValueObjects;

namespace Scripters.Regula.Platform.CommercialManagement.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyCommercialManagementConfiguration(this ModelBuilder builder)
    {
        builder.Entity<CommercialCustomer>().ToTable("commercial_customers");
        builder.Entity<CommercialCustomer>().HasKey(customer => customer.Id);
        builder.Entity<CommercialCustomer>().Property(customer => customer.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<CommercialCustomer>().Property(customer => customer.Name).IsRequired().HasMaxLength(100);
        builder.Entity<CommercialCustomer>().Property(customer => customer.ActiveDebtAmount).IsRequired().HasPrecision(10, 2);
        builder.Entity<CommercialCustomer>().Property(customer => customer.DebtCount).IsRequired();

        builder.Entity<CommercialCustomer>().HasData(
            new
            {
                Id = 1,
                Name = "Cliente de prueba",
                ActiveDebtAmount = 0m,
                DebtCount = 0
            });

        builder.Entity<CommercialDebt>().ToTable("commercial_debts");
        builder.Entity<CommercialDebt>().HasKey(debt => debt.Id);
        builder.Entity<CommercialDebt>().Property(debt => debt.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<CommercialDebt>().Property(debt => debt.Amount).IsRequired().HasPrecision(10, 2);
        builder.Entity<CommercialDebt>().Property(debt => debt.RemainingAmount).IsRequired().HasPrecision(10, 2);
        builder.Entity<CommercialDebt>().Property(debt => debt.Description).IsRequired().HasMaxLength(250);

        builder.Entity<CommercialDebt>()
            .Property(debt => debt.Status)
            .IsRequired()
            .HasConversion(
                status => status.ToString().ToUpperInvariant(),
                status => Enum.Parse<ECommercialDebtStatus>(status, true));

        builder.Entity<CommercialDebt>()
            .HasOne(debt => debt.Customer)
            .WithMany()
            .HasForeignKey(debt => debt.CustomerId);

        builder.Entity<CommercialDebtPayment>().ToTable("commercial_debt_payments");
        builder.Entity<CommercialDebtPayment>().HasKey(payment => payment.Id);
        builder.Entity<CommercialDebtPayment>().Property(payment => payment.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<CommercialDebtPayment>().Property(payment => payment.Amount).IsRequired().HasPrecision(10, 2);
        builder.Entity<CommercialDebtPayment>().Property(payment => payment.PreviousRemainingAmount).IsRequired().HasPrecision(10, 2);
        builder.Entity<CommercialDebtPayment>().Property(payment => payment.NewRemainingAmount).IsRequired().HasPrecision(10, 2);
        builder.Entity<CommercialDebtPayment>().Property(payment => payment.Note).HasMaxLength(250);

        builder.Entity<CommercialDebtPayment>()
            .HasOne(payment => payment.CustomerDebt)
            .WithMany()
            .HasForeignKey(payment => payment.CustomerDebtId);
    }
}