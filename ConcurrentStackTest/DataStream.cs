using System;
using System.Collections.Concurrent;

namespace ConcurrentStackTest
{
    public static class DataStream
    {
        private static ConcurrentDictionary<Guid, EventStreamer> streamManager = new ConcurrentDictionary<Guid, EventStreamer>();

        public static Guid StartStream<T>(T objectToCapture)
        {
            var streamItem = new BeginEventStream<T>(objectToCapture);

            var stream = new EventStreamer();
            var id = stream.BeginFor(streamItem);

            streamManager.TryAdd(id, stream);

            return id;
        }

        public static void Append<T>(Guid id, T objectToCapture)
        {
            if (streamManager.ContainsKey(id))
            {
                var item = new EventItem<T>(streamManager[id].NextId())
                {
                    Id = id,
                    Data = objectToCapture
                };

                streamManager[id].Append(item);
            }
            else
            {
                throw new Exception("a stream does not exist for this id");
            }
        }

        public static void Persist(Guid id)
        {
            if (streamManager.ContainsKey(id))
            {
                streamManager[id].Persist(x => Console.WriteLine(x.Data.Name));
            }
        }
    }
}