using SaveTrip.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaveTrip.Models
{
    public class Travel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Destiny { get; set; }
        public DateTime Date { get; set; }
        public StatusEnumerate Status { get; set; }
        public double TotalCost { get; set; }
        public double TotalPaid { get; set; }

        public List<User> Travelers { get; set; } = new List<User>();
        public List<Cost> TravelCosts { get; set; } = new List<Cost>();
        

        public Travel() { }
        public Travel(int id,string name, string destiny, DateTime date, double totalCost, double totalPaid)
        {
            Id = id;
            Name = name;
            Destiny = destiny;
            Date = date;
            Status = StatusEnumerate.PROGRESS;
            TotalCost = totalCost;
            TotalPaid = totalPaid;
        }

        public void AddTraveler(User user)
        {
            if (!Travelers.Contains(user))
            {
                Travelers.Add(user);
                user.AddTravel(this);
            }
        }

        public void AddCost(Cost cost)
        {
            if (!TravelCosts.Contains(cost))
            {
                TravelCosts.Add(cost);
            }
        }
    }
}
