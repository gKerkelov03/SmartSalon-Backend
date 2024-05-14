
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
            LogoUrl = "https://flagpedia.net/data/flags/w580/us.webp"
        },
        new()
        {
            Name = "Euro",
            Code = "EUR",
            Country = "Eurozone",
            LogoUrl = "https://ec.europa.eu/regional_policy/images/information-sources/logo-download-center/eu_flag.jpg"
        },
        new()
        {
            Name = "Turkish Lira",
            Code = "TRY",
            Country = "Turkey",
            LogoUrl = "https://flagpedia.net/data/flags/w580/tr.webp"
        },
        new()
        {
            Id = BulgarianLevId,
            Name = "Bulgarian Lev",
            Code = "BGN",
            Country = "Bulgaria",
            LogoUrl = "https://flagpedia.net/data/flags/w580/bg.webp"
        }
    ];
}