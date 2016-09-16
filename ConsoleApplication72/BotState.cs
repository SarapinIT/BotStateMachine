using System;
using System.Collections.Generic;


namespace ConsoleApplication72
{


  public abstract class BotState
  {
    private static readonly Dictionary<string, BotState> States = new Dictionary<string, BotState>
    {
      [nameof(OnState)] = new OnState(),
      [nameof(OffState)] = new OffState()
    };
 
    public static BotState State(string name)
    {
        BotState state;
        if(States.TryGetValue(name, out state))
        {
          return state;
        }
        throw new ArgumentException($"Не найдено состояние {state}");
    }

    public void Handle(BotContext context)
    {
      Action<BotContext> action;
      if(Actions.TryGetValue(context.CurrentCommand, out action))
      {
        action.Invoke(context);
      }
      else
      {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Неверная команда!");

        Console.WriteLine("Доступные команды:");
        Console.ForegroundColor = ConsoleColor.Blue;

        foreach (var key in Actions.Keys)
        {
          Console.WriteLine(key);
        }
        Console.ForegroundColor = ConsoleColor.White;
      }
    }
    protected abstract Dictionary<string, Action<BotContext>> Actions { get; }
  }

  public class OnState : BotState
  {
    private Dictionary<string, Action<BotContext>> _actions = new Dictionary<string, Action<BotContext>>
    {
      ["/on"] = (context) => { Console.WriteLine("state is already on!"); }, 
      ["/off"] = (context) => { Console.WriteLine("state switched to off"); context.State = State(nameof(OffState)); },
      ["/echo"] = (context) => { Console.WriteLine(context.CurrentMessage); }
    };
    protected override Dictionary<string, Action<BotContext>> Actions => _actions;
    
  }

  public class OffState : BotState
  {
    private Dictionary<string, Action<BotContext>> _actions = new Dictionary<string, Action<BotContext>>
    {
      ["/off"] = (context) => { Console.WriteLine("state is already off!"); },
      ["/on"] = (context) => { Console.WriteLine("state switched to on"); context.State = State(nameof(OnState)); }
    };
    protected override Dictionary<string, Action<BotContext>> Actions => _actions;
  }
}