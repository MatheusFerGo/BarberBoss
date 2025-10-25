using BarberBoss.Domain.Enums;
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

        Assert.Equal("Amount deve ser amior ou igual a zero.", exception.Message);
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
        Assert.Equal("BarberName não pode ser vazio.", exception.Message);
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
        Assert.Equal("ClientName não pode ser vazio.", exception.Message);
    }

    [Fact]
    public void Should_SetAmountToZero_When_AmountIsZero()
    {
        decimal zeroAmount = 0.0m;
        var billing = new Billing(
            _validDate, _validBarberName, _validClientName, _validService,
            zeroAmount, _paymentMethod, _validStatus, null);
        Assert.Equal(BillingStatus.Cancelado, billing.Status);
        Assert.Equal(0.0m, billing.Amount);
    }
}