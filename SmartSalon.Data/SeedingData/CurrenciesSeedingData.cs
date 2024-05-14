
using SmartSalon.Application.Domain.Salons;

namespace SmartSalon.Data.SeedingData;

internal static class CurrenciesSeedingData
{
    public static Id BulgarianLevId = Id.NewGuid();

    public static Currency[] Data = [
        new()
        {
            Name = "Bitcoin",
            Code = "BTC",
            Country = null,
            LogoUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/46/Bitcoin.svg/2048px-Bitcoin.svg.png"
        },
        new()
        {
            Name = "Ethereum",
            Code = "ETH",
            Country = null,
            LogoUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/05/Ethereum_logo_2014.svg/1257px-Ethereum_logo_2014.svg.png"
        },
        new()
        {
            Name = "United States Dollar",
            Code = "USD",
            Country = "United States",
            LogoUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a9/Flag_of_the_United_States_%28DoS_ECA_Color_Standard%29.svg/255px-Flag_of_the_United_States_%28DoS_ECA_Color_Standard%29.svg.png"
        },
        new()
        {
            Name = "Euro",
            Code = "EUR",
            Country = "Eurozone",
            LogoUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b7/Flag_of_Europe.svg/255px-Flag_of_Europe.svg.png"
        },
        new()
        {
            Id = BulgarianLevId,
            Name = "Bulgarian Lev",
            Code = "BGN",
            Country = "Bulgaria",
            LogoUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/9a/Flag_of_Bulgaria.svg/255px-Flag_of_Bulgaria.svg.png"
        }
    ];
}