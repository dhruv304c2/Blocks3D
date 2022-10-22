using System;
using UnityEngine;

namespace Game.Core.Interface
{
    public interface IObservableDataSource<T>
    {
        T Self => (T)this;
        Action OnDataSourceChanged { get; set; }

        public void SubscribeTo(IDataService<T> service)
        {
            OnDataSourceChanged ??= () => { };
            OnDataSourceChanged = service.DataService;
        }
    }
}