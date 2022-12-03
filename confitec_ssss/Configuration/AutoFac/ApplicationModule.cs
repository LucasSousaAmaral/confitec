using Autofac;
using Domain.IRepository;
using Infrastructure.Repositories;

namespace confitec_ssss.Configuration.AutoFac;

public class ApplicationModule : Module
{
    #region Methods
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<UserRepository>()
               .As<IUserRepository>()
               .InstancePerLifetimeScope();
    }
    #endregion
}