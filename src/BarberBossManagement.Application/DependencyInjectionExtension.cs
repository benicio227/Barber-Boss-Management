using BarberBossManagement.Application.AutoMapper;
using BarberBossManagement.Application.UseCases.Barbers.Delete;
using BarberBossManagement.Application.UseCases.Barbers.GetAll;
using BarberBossManagement.Application.UseCases.Barbers.GetById;
using BarberBossManagement.Application.UseCases.Barbers.Register;
using BarberBossManagement.Application.UseCases.Barbers.Update;
using BarberBossManagement.Application.UseCases.Login.DoLogin;
using BarberBossManagement.Application.UseCases.Revenues.Delete;
using BarberBossManagement.Application.UseCases.Revenues.GetAll;
using BarberBossManagement.Application.UseCases.Revenues.GetById;
using BarberBossManagement.Application.UseCases.Revenues.Register;
using BarberBossManagement.Application.UseCases.Revenues.Reports.Excel;
using BarberBossManagement.Application.UseCases.Revenues.Update;
using BarberBossManagement.Application.UseCases.Users.ChangePassword;
using BarberBossManagement.Application.UseCases.Users.Delete;
using BarberBossManagement.Application.UseCases.Users.Profile;
using BarberBossManagement.Application.UseCases.Users.Register;
using BarberBossManagement.Application.UseCases.Users.Update;
using Microsoft.Extensions.DependencyInjection;

namespace BarberBossManagement.Application;
public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddAutoMapper(services);
        AddUseCases(services);
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapping));
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterRevenueUseCase, RegisterRevenueUseCase>();
        services.AddScoped<IGetAllRevenueUseCase, GetAllRevenueUseCase>();
        services.AddScoped<IGetRevenueByIdUseCase, GetRevenueByIdUseCase>();
        services.AddScoped<IDeleteRevenueUseCase, DeleteRevenueUseCase>();
        services.AddScoped<IUpdateRevenueUseCase, UpdateRevenueUseCase>();
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
        services.AddScoped<IGetUserProfileUseCase, GetUserProfileUseCase>();
        services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
        services.AddScoped<IChangePasswordUseCase, ChangePasswordUserCase>();
        services.AddScoped<IDeleteUserAccountUseCase, DeleteUserAccountUseCase>();
        services.AddScoped<IRegisterBarberShopUseCase, RegisterBarberShopUseCase>();
        services.AddScoped<IGetAllBarberUseCase, GetAllBarberUseCase>();
        services.AddScoped<IGetRevenueByIdUseCase, GetRevenueByIdUseCase>();
        services.AddScoped<IDeleteBarberUseCase, DeleteBarberUseCase>();
        services.AddScoped<IUpdateBarberUseCase, UpdateBarberUseCase>();
        services.AddScoped<IGetBarberByIdUseCase, GetBarberByIdUseCase>();
        
        services.AddScoped<IGenerateRevenuesReportExcelUseCase, GenerateRevenuesReportExcelUseCase>();
        
    }
}
