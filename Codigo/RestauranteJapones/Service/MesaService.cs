using Core;
using Microsoft.EntityFrameworkCore;

namespace Core.Service
{
    public class MesaService : IMesaService
    {
        private readonly RestauranteContext _context;

        public MesaService(RestauranteContext context)
        {
            _context = context;
        }

        public IEnumerable<Mesa> GetAll()
        {
            return _context.Mesas
                .AsNoTracking()
                .ToList();
        }

        public Mesa? Get(int id)
        {
            return _context.Mesas
                .AsNoTracking()
                .FirstOrDefault(m => m.Id == id);
        }

        public uint Create(Mesa mesa)
        {
            _context.Mesas.Add(mesa);
            _context.SaveChanges();
            return (uint)mesa.Id;
        }

        public void Edit(Mesa mesa)
        {
            _context.Mesas.Update(mesa);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var mesa = _context.Mesas.Find(id);
            if (mesa is not null)
            {
                _context.Mesas.Remove(mesa);
                _context.SaveChanges();
            }
        }
    }
}