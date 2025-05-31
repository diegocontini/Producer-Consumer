using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Producer_Consumer.Models
{
    internal class Consumer([Required] ChannelReader<Message> reader )
    {
        private readonly ChannelReader<Message> _reader = reader;

        public async Task ConsumeAsync() { 
        await foreach (var message in _reader.ReadAllAsync())
            {
                Console.WriteLine($"Consumido: {message.Content}");
                await Task.Delay(5000);
            }
        }

    }
}
