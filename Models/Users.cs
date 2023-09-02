using Assessment.Models;

public class Users
{
    public int Id { get; set; }
    public string PhoneNumber { get; set; } = "";
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public string Role { get; set; } = "User";
    public string Password { get; set; } = "";
    public List<Events> RegisteredEvents { get; set; } = new List<Events>();
}
