namespace WebTelNET.Models.Libs
{
    public interface IDatabaseSettings
    {
        DatabaseSettings DatabaseSettings { get; set; }
    }

    public class DatabaseSettings
    {
        public RoleSettings RoleSettings { get; set; }
    }

    public class RoleSettings
    {
        public string UserRole { get; set; }
        public string AdminRole { get; set; }
    }
}
