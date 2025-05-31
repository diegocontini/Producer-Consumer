using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Producer_Consumer.Models
{
    internal class Producer([Required] ChannelWriter<Message> writer)
    {
        private readonly ChannelWriter<Message> _writer = writer;

        public async Task ProduceAsync([Required]int lenght)
        {
            for (int i = 0; i < lenght; i++)
            {
                var message = new Message() { Content = $"Meu conteúdo é:{i}" };
                Console.WriteLine($"Produzido: {i}");
                await Task.Delay(500);
                await _writer.WriteAsync(message);
            }
            _writer.Complete();
        }
    }
}
