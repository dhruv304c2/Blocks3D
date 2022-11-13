using System;
using System.Collections.Generic;
using Game.Core.Types;
using UnityEngine;

namespace Game.Core.Interface
{
    public interface IObservableDataSource<T>
    {
        List<IEventListener<T>> Listeners { get; set; }

        public void Notify(T data, GameEvent gameEvent)
        {
            foreach (var eventListener in Listeners)
            {
                eventListener.OnNotify(data,gameEvent);
            }
        }
    }
}