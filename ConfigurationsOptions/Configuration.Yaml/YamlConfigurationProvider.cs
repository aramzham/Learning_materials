using Microsoft.Extensions.Configuration;

namespace Configuration.Yaml;

public sealed class YamlConfigurationProvider(FileConfigurationSource source) : FileConfigurationProvider(source)
{
    public override void Load(Stream stream)
    {
        try
        {
            Data = YamlConfigurationFileParser.Parse(stream);
        }
        catch (Exception e)
        {
            throw new FormatException("Invalid YAML", e);
        }
    }
}