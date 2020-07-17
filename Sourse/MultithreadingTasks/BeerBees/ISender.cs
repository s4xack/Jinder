using System.Collections.Generic;

namespace BeerBees
{
    public interface ISender<TValue>
    {
        void Send(IReadOnlyCollection<TValue> data);
    }
}