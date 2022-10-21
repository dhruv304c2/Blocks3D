using System;
using UnityEngine;

namespace Game.Core.Interface
{
    public interface IObservableDataSource<T>
    {
        Action OnDataSourceChanged { get; set; }

        void SubscribeTo(IDataService<T> service)
        {
            OnDataSourceChanged ??= () => { };
            OnDataSourceChanged = service.DataService;
        }
    }
}