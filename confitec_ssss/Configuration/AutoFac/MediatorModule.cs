using Autofac;
using MediatR;
using Services.Queries;
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
        builder.Register<ServiceFactory>(context =>
        {
            var componentContext = context.Resolve<IComponentContext>();
            return t => componentContext.TryResolve(t, out object? o) ? o : null;
        });
    }
    #endregion
}