using System;

namespace ConcurrentStackTest
{
    public class BeginEventStream<T> : IEventItem
    {
        public BeginEventStream(T objectEventsToTrack)
        {
            Data = objectEventsToTrack;
        }

        

        public Guid Id { get; set; }
        public dynamic Data { get; set; }

        public int Index
        {
            get
            {
                return 1;
            }
        }

        public string GetTrackingData()
        {
            return "return something useful";
        }
    }
}