using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;

namespace Configuration.Yaml;

public static class YamlConfigurationBuilderExtensions
{
    public static IConfigurationBuilder AddYamlFile(this IConfigurationBuilder builder, string path)
    {
        return AddYamlFile(builder, provider: null, path: path, optional: false, reloadOnChange: false);
    }

    public static IConfigurationBuilder AddYamlFile(this IConfigurationBuilder builder, string path, bool optional)
    {
        return AddYamlFile(builder, provider: null, path: path, optional: optional, reloadOnChange: false);
    }

    public static IConfigurationBuilder AddYamlFile(this IConfigurationBuilder builder, string path, bool optional, bool reloadOnChange)
    {
        return AddYamlFile(builder, provider: null, path: path, optional: optional, reloadOnChange: reloadOnChange);
    }
    
    public static IConfigurationBuilder AddYamlFile(this IConfigurationBuilder builder, IFileProvider? provider, string path, bool optional, bool reloadOnChange)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentException.ThrowIfNullOrWhiteSpace(path);

        return  builder.AddYamlFile(s =>
        {
            s.FileProvider = provider;
            s.Path = path;
            s.Optional = optional;
            s.ReloadOnChange = reloadOnChange;
            s.ResolveFileProvider();
        });
    }
    
    public static IConfigurationBuilder AddYamlFile(this IConfigurationBuilder builder, 
        Action<YamlConfigurationSource> configureSource)
    {
        return builder.Add(configureSource);
    }
}