﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EdgeDB;


namespace ContactDatabase.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public Contact NewContact { get; set; } = new ();
        private readonly EdgeDBClient _client;

        public IndexModel(EdgeDBClient client)
        {
            _client = client;
        }
        public void OnGet()
        {
            var client = new EdgeDBClient();
        }

        public async Task<IActionResult> OnPostAsync() 
        {
            Console.WriteLine(NewContact.MaritalStatus);
            await _client.ExecuteAsync($$"""
                INSERT Contact {
                first_name := "{{NewContact.FirstName}}", 
                last_name := "{{NewContact.LastName}}",
                email := "{{NewContact.Email}}",
                title := "{{NewContact.Title}}",
                description :="{{NewContact.Description}}",
                birth_date :="{{NewContact.BirthDate}}",
                marital_status :=<bool>"{{NewContact.MaritalStatus}}"
                }
             """);
            return RedirectToPage("/Dataview");
        }
    }

    public class Contact
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public String Title { get; set; }
        public String Description { get; set; } 
        public String BirthDate { get; set; }
        public bool MaritalStatus { get; set; }

        public Contact()
        {

        }

        public Contact(string firstName, string lastName, string email, string title, string description, string birthDate, bool maritalStatus)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Title = title;
            Description = description;
            BirthDate = birthDate;
            MaritalStatus = maritalStatus;
        }
    }


}