using System;
using System.Collections.Generic;
using Game.Core.Interface;
using Game.Core.Types;
using UnityEngine;

namespace Game.Core.Singletons
{
    public class GameDataProvider: Singleton<GameDataProvider>, IObservableDataSource<int>
    {

        private bool _isRunning;
        public bool IsRunning => _isRunning;
        
        private int _score;
        public int Score => _score;
        
        //Game Variables
        public static int GameHeight = 10;

        public void StartGame()
        {
            _isRunning = true; 
            ((IObservableDataSource<int>)this).Notify(_score,GameEvent.Layer_Clear);
        }

        public void EndGame()
        {
            _isRunning = false;
            ((IObservableDataSource<int>)this).Notify(_score,GameEvent.End_Game);
        }

        public void AddScore(int score)
        {
            _score += score;
            ((IObservableDataSource<int>)this).Notify(_score,GameEvent.Layer_Compleate);
        }

        public List<IEventListener<int>> Listeners { get; set; }
    }
}