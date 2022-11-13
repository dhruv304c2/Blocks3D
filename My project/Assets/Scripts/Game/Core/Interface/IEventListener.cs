using System.Collections.Generic;
using Game.Core.Types;
using Unity.VisualScripting;

namespace Game.Core.Interface
{
    public interface IEventListener<T>
    {
        void OnNotify(T data, GameEvent gameEvent);
        public void SubscribeTo(IObservableDataSource<T> subject)
        {
            subject.Listeners ??= new List<IEventListener<T>>();
            subject.Listeners.Add(this);
        }
    }
}