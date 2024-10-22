using BarberBossManagement.Domain.Entities;
using BarberBossManagement.Domain.Repositories.Barbers;
using Microsoft.EntityFrameworkCore;

namespace BarberBossManagement.Infrastructure.DataAccess.Repositories;
internal class BarbersRepository : IBarbersShopsWriteOnlyRepository, IBarbersShopsReadOnlyRepository, IBarbersShopsUpdateOnlyRepository
{
    private readonly BarberBossManagementDbContext _dbContext;
    public BarbersRepository(BarberBossManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Add(BarberShop barber)
    {
        await _dbContext.BarberShops.AddAsync(barber);
    }

    public async Task Delete(long id)
    {
        var result = await _dbContext.BarberShops.FindAsync(id);

        _dbContext.BarberShops.Remove(result!);
    }

    public async Task<List<BarberShop>> GetAll(User user)
    {
        return await _dbContext.BarberShops.AsNoTracking().Where(barber => barber.UserId == user.Id).ToListAsync();
    }

    async Task<BarberShop?> IBarbersShopsUpdateOnlyRepository.GetById(User user, long id)
    {
        return await _dbContext.BarberShops.FirstOrDefaultAsync(barber => barber.Id == id && barber.UserId == user.Id);
    }
    async Task<BarberShop?> IBarbersShopsReadOnlyRepository.GetById(User user, long id)
    {
        return await _dbContext.BarberShops
            .AsNoTracking()
            .Include(barber => barber.Revenues)
            .FirstOrDefaultAsync(barber => barber.Id == id && barber.UserId == user.Id);
    }

    public void Update(BarberShop barber)
    {
        _dbContext.BarberShops.Update(barber);
    }

}
