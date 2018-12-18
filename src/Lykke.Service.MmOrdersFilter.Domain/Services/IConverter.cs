namespace Lykke.Service.MmOrdersFilter.Domain.Services
{
    public interface IConverter<TFrom, TTo>
    {
        TTo Convert(TFrom data);
    }
}
