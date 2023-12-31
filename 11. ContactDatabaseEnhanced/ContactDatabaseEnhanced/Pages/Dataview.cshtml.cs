using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EdgeDB;
namespace ContactDatabaseEnhanced.Pages;

public class DataviewModel : PageModel
{
    public List<Contact> ContactsList { get; private set; } = new();
    private readonly EdgeDBClient _client;
    public DataviewModel(EdgeDBClient client)
    {
        _client = client;
    }

    public async Task<IActionResult> OnGet()
    {
        var contacts = await _client.QueryAsync<DBContact>(
            "SELECT Contact {first_name, last_name, email, title, description, birth_date, marital_status, user_name, password, user_role, salt};");
        foreach (var contact in contacts)
        {
            ContactsList.Add(
                new Contact(
                    contact.first_name,
                    contact.last_name,
                    contact.email,
                    contact.title,
                    contact.description,
                    contact.birth_date,
                    contact.marital_status,
                    contact.user_name,
                    contact.password,
                    contact.user_role,
                    contact.salt
                )
            );
        }
        return Page();
    }

    public class DBContact
    {
        public string first_name { get; set; } = "";
        public string last_name { get; set; } = "";
        public string email { get; set; } = "";
        public string title { get; set; }
        public string description { get; set; } = "";
        public string birth_date { get; set; } = "";
        public bool marital_status { get; set; } = false;
        public string user_name { get; set; } = "";
        public string password { get; set; } = "";
        public string user_role { get; set; } = "";
        public byte[] salt { get; set; } = new byte[32];
    }
}