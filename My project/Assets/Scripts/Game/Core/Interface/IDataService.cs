namespace Game.Core.Interface
{
    public interface IDataService<T>
    {
        IObservableDataSource<T> DataSource { get; set; }

        void DataService();
    }
}