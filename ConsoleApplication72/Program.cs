using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication72
{
  class Program
  {
    static void Main(string[] args)
    {
      var context = new BotContext(new OffState());
      while (true)
      {
        Console.Write("user>");
        var text = Console.ReadLine();
        context.Request(text);
      }
    }
  }

  public class BotContext
  {
    public string CurrentCommand { get; private set; } = string.Empty;
    public string CurrentMessage { get; private set; } = string.Empty;

    public BotState State { get; set; }
    public BotContext(BotState state)
    {
      State = state;
    }
    public void Request(string text)
    {
      if(text[0] == '/')
      {
        var index = text.IndexOf(' ');
        if(index >= 0)
        {
          CurrentCommand = text.Substring(0, index);
          CurrentMessage = text.Substring(index + 1);
        }
        else
        {
          CurrentCommand = text;
        }
      }
      
      State.Handle(this);
    }
  }
}
