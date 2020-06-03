namespace Varesin.Mvc.Models.User
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public int Gender { get; set; }
        public string RegisterDate { get; set; }
        public bool IsSuperAdmin { get; set; }
    }
}
