using System;
using Game.Core.Interface;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Core
{
    public abstract class MonoPoolable<T>: MonoBehaviour, IPoolable<T> where T : MonoBehaviour
    {
        public Transform ObjectPool => GameObject.Find($"{transform.name} Pool")?.transform;
        
        public bool Initialised => ObjectPool != null; 

        public void Dispose()
        {
            OnDispose();
            gameObject.SetActive(false);
            transform.parent = ObjectPool;
        }

        public T Spawn()
        {
            if (Initialised == false)
            {
                Debug.Log($"{transform.name} Object pool not initialised");
                return null;
            }

            if (ObjectPool.childCount != 0)
            {
                var new_obj = ObjectPool.GetChild(0).GetComponent<T>();
                new_obj.gameObject.SetActive(true);
                new_obj.transform.parent = null;
                OnSpawn(); 
                return new_obj;
            }

            else
            {
                Debug.Log($"{transform.name} Object pool is empty");
                return null;
            }
        }

        public void InitialisePool(int size)
        {
            var pool = new GameObject($"{transform.name} Pool").transform;
            while (size > 0)
            {
                var clone = Instantiate(this,pool);
                clone.transform.name = this.transform.name;
                clone.gameObject.SetActive(false);
                clone.OnDispose();
                size--;
            }

            //Initialised = true;
        }

        protected abstract void OnDispose();
        protected abstract void OnSpawn();
        
    }
}