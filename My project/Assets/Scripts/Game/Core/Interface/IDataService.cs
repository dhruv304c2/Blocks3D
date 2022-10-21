namespace Game.Core.Interface
{
    public interface IDataService<T>
    {
        T DataSource { get; set; }

        void DataService();
    }
}