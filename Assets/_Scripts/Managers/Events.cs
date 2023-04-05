using System;

public static class Events
{
  public static readonly Evt OnGameStart = new Evt();
  public static readonly Evt OnGameOver = new Evt();
  public static readonly Evt OnFoodTake = new Evt();
}

public class Evt
{
  private event Action _action = delegate { };
  public void Invoke()=> _action?.Invoke();
  public void AddListener(Action listener) =>_action += listener;
  public void RemoveListener(Action listener)=> _action -= listener;
  public void DisableEvent() => _action= null;
}

// with paramaters
public class Evt<T>
{
  private event Action<T> _action = delegate{  };
  public void Invoke(T param)=> _action.Invoke(param);
  public void AddListener(Action<T> listener)=>  _action += listener;
  public void RemoveListener(Action<T> listener)=> _action -= listener;
    
}
