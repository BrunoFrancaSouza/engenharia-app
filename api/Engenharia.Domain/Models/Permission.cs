namespace Engenharia.Domain.Models
{
    public class Permission
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
