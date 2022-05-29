using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using DIS_data.Repository;
using DIS_Server.Services;

namespace DIS_Server.Configuration
{
    public class AutofacServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>()
                .As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<UserRepository>()
                .As<IUserRepository>().InstancePerLifetimeScope();

            builder.RegisterType<HistoryRepository>()
                .As<IHistoryRepository>().InstancePerLifetimeScope();
            builder.RegisterType<HistoryService>()
                .As<IHistoryService>().InstancePerLifetimeScope();

            builder.RegisterType<SendEmailService>()
                .As<ISendEmailService>().InstancePerLifetimeScope();
        }
    }
}
