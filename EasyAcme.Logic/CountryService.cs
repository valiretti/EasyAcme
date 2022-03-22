using EasyAcme.DataAccess;
using EasyAcme.Model;
using Microsoft.EntityFrameworkCore;

namespace EasyAcme.Logic;

public class CountryService : ICountryService
{
    private readonly ApplicationContext _applicationContext;

    public CountryService(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    public Task<List<Country>> GetCountriesAsync()
    {
        return _applicationContext.Countries.OrderBy(c => c.Name).ToListAsync();
    }
}