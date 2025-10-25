using BarberBoss.Domain.Enums;
using BarberBoss.Domain.Extensions;
using System.ComponentModel.DataAnnotations;

namespace BarberBoss.Domain.Tests;

public class BillingTests
{
    private readonly DateOnly _validDate = new DateOnly(2025, 10, 25);
    private const string _validBarberName = "John Doe";
    private const string _validClientName = "Jane Smith";
    private const string _validService = "Corte";
    private const decimal _validAmount = 50.0m;
    private const PaymentMethod _paymentMethod = PaymentMethod.Dinheiro;
    private const BillingStatus _validStatus = BillingStatus.Pago;

    [Fact]
    // "Implementar regra: Amount deve ser maior ou igual a zero"
    public void Should_ThrowValidationException_When_AmountIsNegative()
    {
        decimal invalidAmount = -10.0m;

        var exception = Assert.Throws<ValidationException>(() => 
            new Billing(
                _validDate, _validBarberName, _validClientName, _validService,
                invalidAmount, _paymentMethod, _validStatus, null)
            );

        Assert.Equal(ResourceErrorMessages.AMOUNT_MUST_BE_POSITIVE, exception.Message);
    }

    [Fact]
    public void Should_ThrowValidationException_When_BarberNameIsEmpty()
    {
        string invalidBarberName = "";
        var exception = Assert.Throws<ValidationException>(() =>
            new Billing(
                _validDate, invalidBarberName, _validClientName, _validService,
                _validAmount, _paymentMethod, _validStatus, null)
            );
        Assert.Equal(ResourceErrorMessages.BARBERNAME_IS_INVALID, exception.Message);
    }

    [Fact]
    public void Should_ThrowValidationException_When_ClientNameIsEmpty()
    {
        string invalidClientName = "";
        var exception = Assert.Throws<ValidationException>(() =>
            new Billing(
                _validDate, _validBarberName, invalidClientName, _validService,
                _validAmount, _paymentMethod, _validStatus, null)
            );
        Assert.Equal(ResourceErrorMessages.CLIENTNAME_IS_INVALID, exception.Message);
    }

    [Fact]
    public void Should_ThrowValidationException_When_PaidBillingHasZeroAmount()
    {
        decimal zeroAmount = 0.0m;
        var paidStatus = BillingStatus.Pago;

        var exception = Assert.Throws<ValidationException>(() =>
            new Billing(
                _validDate, _validBarberName, _validClientName, _validService,
                zeroAmount, _paymentMethod, paidStatus, null)
        );

        Assert.Equal(ResourceErrorMessages.PAID_BILLING_MUST_BE_POSITIVE, exception.Message);
    }

    [Fact]
    public void Should_ThrowValidationException_When_CanceledBillingHasNonZeroAmount()
    {
        decimal nonZeroAmount = 50.0m; // Valor > 0
        var canceledStatus = BillingStatus.Cancelado; // Status Cancelado

        var exception = Assert.Throws<ValidationException>(() =>
            new Billing(
                _validDate, _validBarberName, _validClientName, _validService,
                nonZeroAmount, _paymentMethod, canceledStatus, null)
        );

        Assert.Equal(ResourceErrorMessages.CANCELED_BILLING_MUST_BE_ZERO, exception.Message);
    }
}