using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ziggurat.Messages.Worker
{
    public sealed class Colorize : IDisposable
    {
        private readonly ConsoleColor _originalColor;
        private Colorize(ConsoleColor textColor)
        {
            _originalColor = Console.ForegroundColor;
            Console.ForegroundColor = textColor;
        }
        public void Dispose()
        {
            Console.ForegroundColor = _originalColor;
        }

        public static IDisposable With(ConsoleColor textColor)
        {
            return new Colorize(textColor);
        }
    }
}
