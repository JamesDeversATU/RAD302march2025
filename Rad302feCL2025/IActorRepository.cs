using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rad302feCL2025
{
    internal interface IActorRepository
    {

        Task<List<Actor>> GetAllActorsAsync();
        Task<Actor> GetActorByIdAsync(int id);

        Task AddActorAsync(Actor actor);

        Task UpdateActorAsync(Actor actor);

        Task DeleteActorAsync(int id);
    }
}
