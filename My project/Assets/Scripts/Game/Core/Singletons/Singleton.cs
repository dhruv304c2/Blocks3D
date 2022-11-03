using UnityEngine;

namespace Game.Core.Singletons
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance {
            get
            {
                if (_instance == null) return _instance = new GameObject(typeof(T).ToString()).AddComponent<T>();
                else return _instance;
            }
        }

        public bool Instantiated => _instance != null;
    }
}