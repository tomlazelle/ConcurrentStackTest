using System;

namespace ConcurrentStackTest
{
    public interface IEventStreamer
    {
        Guid BeginFor<T>(BeginEventStream<T> startStream);
        void Append<T>(EventItem<T> usefulData);
    }
}