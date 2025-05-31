
using Producer_Consumer.Models;
using System.Threading.Channels;

class Program
{
    static CountdownEvent countdownEvent = new CountdownEvent(3);
    static async Task Main(string[] args)
    {
        var channel = Channel.CreateBounded<Message>(new BoundedChannelOptions(5)
        {
            FullMode = BoundedChannelFullMode.Wait,
        });

        Producer producer = new(channel.Writer);
        Consumer consumer1 = new(channel.Reader);
        Consumer consumer2 = new(channel.Reader);


        Thread threadProducer = new Thread(async () =>
        {
            await producer.ProduceAsync(30);
            countdownEvent.Signal();
        }

        );
        Thread threadConsumer1 = new Thread(async () =>
        {
            await consumer1.ConsumeAsync();
            countdownEvent.Signal();
        });

        Thread threadConsumer2 = new Thread(async () =>
        {
            await consumer2.ConsumeAsync();
            countdownEvent.Signal();
        });

        threadProducer.Start();
        threadConsumer1.Start();
        threadConsumer2.Start();
        

        threadProducer.Join();
        threadConsumer1.Join();
        threadConsumer2.Join();

        countdownEvent.Wait();

        Console.WriteLine("Finalizando programa..");

    }
}



