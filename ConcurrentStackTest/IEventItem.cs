using System;

namespace ConcurrentStackTest
{
    public interface IEventItem
    {
        Guid Id { get; set; }
        dynamic Data { get; set; }
        int Index { get; }
    }
}