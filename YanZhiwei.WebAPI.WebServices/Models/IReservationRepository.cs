using System.Collections.Generic;

namespace YanZhiwei.WebAPI.WebServices.Models
{
    public interface IReservationRepository
    {
        IEnumerable<Reservation> GetAll();

        Reservation Get(int id);

        Reservation Add(Reservation item);

        void Remove(int id);

        bool Update(Reservation item);
    }
}