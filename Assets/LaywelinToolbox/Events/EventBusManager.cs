using System;
using System.Collections.Generic;

namespace Laywelin {
  public class GameEvent { }

  public static class EventBusManager {
    // Note to self: storing the wrapped listener allows you to unsubscribe
    private static readonly Dictionary<Delegate, Action<GameEvent>> listenerWrappers = new();
    private static readonly Dictionary<Type, Action<GameEvent>> eventListeners = new();
    
    public static void AddListener<T>(Action<T> listener) where T: GameEvent {
      Type type = typeof(T);
      
      Action<GameEvent> wrappedListener = (e) => listener((T)e);

      listenerWrappers[listener] = wrappedListener;
      
      if (eventListeners.TryGetValue(type, out var existing))
        eventListeners[type] = existing + wrappedListener;
      else
        eventListeners[type] = wrappedListener;
    }

    public static void RemoveListener<T>(Action<T> listener) where T : GameEvent {
      Type type = typeof(T);

      if (!listenerWrappers.TryGetValue(listener, out var wrappedListener))
        return;
      
      if (!eventListeners.TryGetValue(type, out var existing))
        return;

      existing -= wrappedListener;
      listenerWrappers.Remove(listener);
      
      if (existing == null)
        eventListeners.Remove(type);
      else
        eventListeners[type] = existing;
    }

    public static void Emit(GameEvent evt) {

      Type currentType = evt.GetType();

      while (currentType != null && typeof(GameEvent).IsAssignableFrom(currentType)) {
        if (eventListeners.TryGetValue(currentType, out var listener))
          listener?.Invoke(evt);

        currentType = currentType.BaseType;
      }
    }

    public static void Clear() { 
      eventListeners.Clear();
      listenerWrappers.Clear();
    }
  }
}