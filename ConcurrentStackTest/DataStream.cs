using System;
using System.Collections.Concurrent;

namespace ConcurrentStackTest
{
    public static class DataStream
    {
        private static readonly ConcurrentDictionary<Guid, EventStreamer> StreamManager = new ConcurrentDictionary<Guid, EventStreamer>();

//        public static Guid StartStream<T>(T objectToCapture)
//        {
//            var streamItem = new BeginEventStream<T>(objectToCapture);
//
//            var stream = new EventStreamer();
//            var id = stream.BeginFor(streamItem);
//
//            streamManager.TryAdd(id, stream);
//
//            return id;
//        }

        public static void Append<T>(Guid id, T objectToCapture)
        {

            if(!StreamManager.ContainsKey(id))
            {
                var streamItem = new BeginEventStream<T>(objectToCapture)
                {
                    Id = id
                };

                var stream = new EventStreamer();

                stream.BeginFor(streamItem);
                StreamManager.TryAdd(id, stream);

                return;
            }
            

                var item = new EventItem<T>(StreamManager[id].NextId())
                {
                    Id = id,
                    Data = objectToCapture
                };

                StreamManager[id].Append(item);

        }

        public static void Persist(Guid id)
        {
            if (StreamManager.ContainsKey(id))
            {
                StreamManager[id].Persist(x => Console.WriteLine(x.Data.Name));
            }
        }
    }
}