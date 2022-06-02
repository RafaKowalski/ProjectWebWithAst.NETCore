using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} Required")]
        public string Name { get; set; }
        public ICollection<Seller> Sellers  { get; set; } = new List<Seller>();

        public Department()
        {
        }

        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void AddSeller(Seller seller)
        {
            Sellers.Add(seller);
        }

        public double TotalSales(DateTime initital, DateTime final)
        {
            return Sellers.Sum(seller => seller.TotalSales(initital, final));
        }
    }
}
