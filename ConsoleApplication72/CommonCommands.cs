using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication72
{
  public static class CommonCommands
  {
    public static void CommandsList(BotContext context)
    {
      Console.WriteLine("Доступные команды:");
      Console.ForegroundColor = ConsoleColor.Blue;

      foreach (var key in context.State.Actions.Keys)
      {
        Console.WriteLine(key);
      }
      Console.ForegroundColor = ConsoleColor.White;
    }
  }
}
