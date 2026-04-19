namespace App.Models;

public class UserModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public ICollection<AddressModel> Addresses { get; set; } = new List<AddressModel>();
}
