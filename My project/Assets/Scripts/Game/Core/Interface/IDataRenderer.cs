using Unity.VisualScripting;

namespace Game.Core.Interface
{
    public interface IDataRenderer<T> : IDataService<T>
    {
        void RenderData();

        void IDataService<T>.DataService()
        {
            RenderData();
        }
    }
}