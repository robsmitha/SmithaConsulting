using DataLayer.Entities;
using System;

namespace DomainLayer.Models
{
    public class CustomerModel
    {
        public int ID { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public CustomerModel(Customer customer)
        {
            if (customer != null)
            {
                ID = customer.ID;
                CreatedAt = customer.CreatedAt;
                FirstName = customer.FirstName;
                MiddleName = customer.MiddleName;
                LastName = customer.LastName;
                Email = customer.Email;
            }
        }

        public CustomerModel() { }
    }
}
