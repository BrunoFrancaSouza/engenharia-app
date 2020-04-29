namespace Engenharia.Domain.Auth
{
    public enum Permissions
    {
        
        UserView = 1,
        UserRead = 2,
        UserUpdate = 3,
        UserDelete = 4,

        RoleView = 5,
        RoleCreate = 6,
        RoleUpdate = 7,
        RoleDelete = 8,

        Teste = 11,

        AccessAll = 9
    }

    public static class Extensions
    {
        public static string ValueToString(this Permissions permission)
        {
            return permission.ToString("D");
        }
    }
}
