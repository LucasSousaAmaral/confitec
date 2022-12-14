using Autofac;
using FluentValidation;
using MediatR;
using Services.Queries;
using Services.Validations.UserValidations;
using System.Reflection;

namespace confitec_ssss.Configuration.AutoFac;

public class MediatorModule : Autofac.Module
{
    #region Methods
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
            .AsImplementedInterfaces();
        builder.RegisterAssemblyTypes(typeof(GetUsersQueryHandler).GetTypeInfo().Assembly)
            .AsClosedTypesOf(typeof(IRequestHandler<,>));
        builder.RegisterAssemblyTypes(typeof(CreateUserCommandValidator).GetTypeInfo().Assembly)
            .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)));
        builder.Register<ServiceFactory>(context =>
        {
            var componentContext = context.Resolve<IComponentContext>();
            return t => componentContext.TryResolve(t, out object? o) ? o : null;
        });
    }
    #endregion
}