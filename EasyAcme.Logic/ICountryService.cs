using EasyAcme.Model;

namespace EasyAcme.Logic;

public interface ICountryService
{
    Task<List<Country>> GetCountriesAsync();
}