using BuildingBlocks.CrossCutting.CQRS;
using FluentValidation;
using Medicine.Application.Handlers.Commands.CreateMedicine;
using Medicine.Application.Handlers.Commands.DeleteMedicineById;
using Medicine.Application.Handlers.Commands.UpdateMedicine;
using Medicine.Application.Handlers.Queries.GetAllMedicines;
using Medicine.Application.Handlers.Queries.GetMedicineById;
using Medicine.Application.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Medicine.Application;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Register application services here
        /*services.Scan(scan => scan.FromAssembliesOf(Assembly.GetExecutingAssembly().GetType())
        .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)), publicOnly: false)
        .AsImplementedInterfaces()
        .WithScopedLifetime()
        .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)), publicOnly: false)
        .AsImplementedInterfaces()
        .WithScopedLifetime());*/
        services.AddScoped<IMedicineRepository, MedicineRepository>();
        services.AddScoped<ICommandHandler<CreateMedicineCommand, CreateMedicineResult>, CreateMedicineHandler>();
        services.AddScoped<ICommandHandler<UpdateMedicineCommand, UpdateMedicineResult>, UpdateMedicineHandler>();
        services.AddScoped<ICommandHandler<DeleteMedicineByIdCommand, DeleteMedicineByIdResult>, DeleteMedicineByIdHandler>();
        services.AddScoped<IQueryHandler<GetAllMedicinesQuery,GetAllMedicinesResult>, GetAllMedicinesHandler>();
        services.AddScoped<IQueryHandler<GetMedicineByIdQuery, GetMedicineByIdResult>, GetMedicineByIdHandler>();
        services.AddScoped<IDispatcher, Dispatcher>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true);
        return services;
    }
}
