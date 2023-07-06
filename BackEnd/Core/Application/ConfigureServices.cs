using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;
using System;
using AutoMapper;
using SampleGeneratedCodeApplication.Commons.Interfaces.Infrastructure;
using SampleGeneratedCodeApplication.Commons.Interfaces.Repositories;
using Castle.DynamicProxy;

namespace SampleGeneratedCodeApplication
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddSampleGeneratedCodeApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });
            return services;
        }

        //Website 
        //http://codethug.com/2021/03/17/Caching-with-Attributes-in-DotNet-Core5/
        //service collection extension
        public static void AddProxiedTransient<TInterface, TImplementation>
            (this IServiceCollection services)
            where TInterface : class
            where TImplementation : class, TInterface
        {
            // This registers the underlying class
            services.AddTransient<TImplementation>();
            services.AddTransient(typeof(TInterface), serviceProvider =>
            {
                // Get an instance of the Castle Proxy Generator
                var proxyGenerator = serviceProvider
                    .GetRequiredService<ProxyGenerator>();
                // Have DI build out an instance of the class that has methods
                // you want to cache (this is a normal instance of that class 
                // without caching added)
                var actual = serviceProvider
                    .GetRequiredService<TImplementation>();
                // Find all of the interceptors that have been registered, 
                // including our caching interceptor.  (you might later add a 
                // logging interceptor, etc.)
                var interceptors = serviceProvider
                    .GetServices<IInterceptor>().ToArray();
                // Have Castle Proxy build out a proxy object that implements 
                // your interface, but adds a caching layer on top of the
                // actual implementation of the class.  This proxy object is
                // what will then get injected into the class that has a 
                // dependency on TInterface
                return proxyGenerator.CreateInterfaceProxyWithTarget(
                    typeof(TInterface), actual, interceptors);
            });
        }
    }
}

