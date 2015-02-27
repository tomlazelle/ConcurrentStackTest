using System;

namespace ConcurrentStackTest
{
    public class EventItem<T> : IEventItem
    {
        public EventItem(int index)
        {
            Index = index;
        }

        public Guid Id { get; set; }
        public dynamic Data { get; set; }
        public int Index { get; private set; }
    }
}