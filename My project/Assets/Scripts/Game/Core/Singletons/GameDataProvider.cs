using System;
using Game.Core.Interface;
using UnityEngine;

namespace Game.Core.Singletons
{
    public class GameDataProvider: Singleton<GameDataProvider>, IObservableDataSource<GameDataProvider>
    {
        public Action OnDataSourceChanged { get; set; }

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