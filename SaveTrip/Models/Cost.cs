using SaveTrip.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaveTrip.Models
{
    public class Cost
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPaid { get; set; }
        public bool IsCanceled { get; set; }
        public double TotalValor { get; set; }
        public double TotalPaid { get; private set; }

        public StatusEnumerate Status { get; set; }
        public int TravelId { get; set; }
        public Travel Travel { get; set; }

        public List<User> Travelers = new List<User>();

        public Cost() { }
        public Cost(int id,string name, Travel travel, double totalValor,User traveler )
        {
            Id = id;
            Name = name;
            Travel = travel;
            IsPaid = false;
            Status = StatusEnumerate.PROGRESS;
            IsCanceled = false;
            TotalValor = totalValor;
            TotalPaid = 0;
            Travelers.Add( traveler );
        }

        public bool AddMoreMoney(User user, double money)
        {

            if (Status == Enums.StatusEnumerate.PROGRESS)
            {
                TotalPaid += money;
            }
            else { return false; }

            if (TotalPaid == TotalValor)
            {
                Status = Enums.StatusEnumerate.CONCLUED;
                return true;
            }
            return true;
        }
        public bool CancelCost()
        {
            if (Status == Enums.StatusEnumerate.PROGRESS)
            {
                Status = Enums.StatusEnumerate.CANCELED;
                return true;
            }
            return false;
        }
        public void AddNewTraveler(User user)
        {
            if ( Travel.Travelers.Contains(user) && Travelers.Contains(user) )
            {
                Travelers.Add(user);
            }
        }
    }
}
