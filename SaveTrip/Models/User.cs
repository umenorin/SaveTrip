using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaveTrip.Models
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        [StringLength(11)]
        public string Cpf { get; set; }
        public string Password { get; set; }
        public List<Travel> Travels { get; set; } = new List<Travel>();
        public List<Cost> UserCosts { get; set; } = new List<Cost>();

        public User() { }
        public User(int id,string name, string cpf, string password)
        {
            Id = id;
            Name = name;
            Cpf = cpf;
            Password = password;
        }

        public void AddTravel(Travel travel)
        {
            if (!Travels.Contains(travel)){
                Travels.Add(travel);
                travel.AddTraveler(this);
            }
        }
        public void AddCost(Cost cost) {
            if (!UserCosts.Contains(cost)) {
                    UserCosts.Add(cost);  
                    cost.AddNewTraveler(this);
            }
        }
    }
}
