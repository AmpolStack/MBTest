using System.Text;

namespace DbAccess.Custom;

public class MySqlConfig
{
    public string? Server { get; set; }
    public string? Port { get; set; }
    public string? Database { get; set; }
    public string? Uid { get; set; }
    public string? Pwd { get; set; }
    public string? Version { get; set; }

    public string GetConnectionString()
    {
        var sb = new StringBuilder();
        sb.Append($"Server={Server};");
        sb.Append($"Port={Port};");
        sb.Append($"Database={Database};");
        sb.Append($"Uid={Uid};");
        sb.Append($"Pwd={Pwd};");
        return sb.ToString();
    }
}