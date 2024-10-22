using BarberBossManagement.Domain.Enums;

namespace BarberBossManagement.Domain.Extensions;
public static class PaymentTypeExtensions
{
    public static string PaymentTypeToString(this PaymentType paymentType)
    {
        return paymentType switch
        {
            Domain.Enums.PaymentType.Cash => "Dinheiro",
            Domain.Enums.PaymentType.CreditCard => "Cartão de Crédito",
            Domain.Enums.PaymentType.DebitCard => "Cartão de Débito",
            Domain.Enums.PaymentType.Pix => "Transferência Bancária",
            _ => string.Empty
        };
    }
}
