using Game.Core.Types;
using Unity.VisualScripting;

namespace Game.Core.Interface
{
    public interface IDataRenderer<T> : IEventListener<T>
    {
        void RenderData(T data, GameEvent gameEvent);

        void IEventListener<T>.OnNotify(T data, GameEvent gameEvent)
        {
            RenderData(data,gameEvent);
        }
    }
}