using EventStoreApp.Core;
using System;
using System.Threading.Tasks;

namespace EventStoreState
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var context = new BasketDbContext();
            context.Database.EnsureCreated();

            using var connection = await EventStoreHelpers.CreateConnection();

            var guid = new Guid("ceda5c05-f314-4f04-91ad-1b8cad4d929e");
            var stream = $"basket-{guid}";
            var state = new BasketState { Id = guid };

            await context.BasketStates.AddAsync(state);
            await context.SaveChangesAsync();

            var isEndOfStream = false;

            var lastProcessEventNumber = 0;
            var batchSize = 2;


            while(!isEndOfStream)
            {
                var oldEvents = await connection.ReadStreamEventsForwardAsync(
                    stream,
                    lastProcessEventNumber,
                    batchSize,
                    false,
                    EventStoreHelpers.GetCredentials());

                foreach(var evt in oldEvents.Events)
                {
                    state.Update(evt, lastProcessEventNumber);

                    await context.SaveChangesAsync();

                    lastProcessEventNumber++;
                }

                isEndOfStream = oldEvents.IsEndOfStream;
            }
        }
    }
}
