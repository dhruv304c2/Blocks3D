using System;
using Game.Core.Interface;
using UnityEngine;

namespace Game.Core.Singletons
{
    public class GameDataProvider: MonoBehaviour, IObservableDataSource<GameDataProvider>
    {
        public Action OnDataSourceChanged { get; set; }

        private static GameDataProvider _instance;
        public static GameDataProvider Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject("GameDataProvider").AddComponent<GameDataProvider>();
                    return _instance;
                }
                else return _instance;
            }
        }

        private bool _isRunning;
        public bool IsRunning => _isRunning;
        
        private int _score;
        public int Score => _score;
        
        //Game Variables
        public static int GameHeight = 10;

        public void StartGame()
        {
            _isRunning = true; 
            OnDataSourceChanged?.Invoke();
        }

        public void EndGame()
        {
            _isRunning = false;
            OnDataSourceChanged?.Invoke();
        }

        public void AddScore(int score)
        {
            _score += score;
            OnDataSourceChanged?.Invoke();
        }
    }
}