using Microsoft.Extensions.DependencyInjection;

namespace Framework;

public interface IServiceConfiguration
{
    static abstract void Configuration(IServiceCollection services,string cs);
}