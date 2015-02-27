using System;
using System.Collections.Concurrent;
using System.Linq;

namespace ConcurrentStackTest
{
    public class EventStreamer : IEventStreamer
    {
        private ConcurrentBag<IEventItem> stream = new ConcurrentBag<IEventItem>();

        public Guid BeginFor<T>(BeginEventStream<T> startStream)
        {
            if (startStream.Id == Guid.Empty)
            {
                startStream.Id = Guid.NewGuid();
            }

            stream.Add(startStream);

            return startStream.Id;
        }

        public void Append<T>(EventItem<T> usefulData)
        {
            stream.Add(usefulData);
        }

        public int NextId()
        {
            return stream.Max(x => x.Index) + 1;
        }

        public void Persist(Action<IEventItem> persistThis)
        {
            stream.OrderBy(x => x.Index).ToList().ForEach(x => persistThis.Invoke(x));
        }
    }
}