using System;
using Game.Core.Interface;
using Game.Core.Singletons;
using Game.Core.Types;
using UnityEngine;
using TMPro;

namespace Game.UI
{
    public class MonoScoreRenderer: MonoBehaviour, IDataRenderer<int>
    {
        private void Awake()
        {
            ((IDataRenderer<int>)this).SubscribeTo(GameDataProvider.Instance);
        }

        private TextMeshProUGUI MyTMPro => GetComponent<TextMeshProUGUI>();  

        // Data Renderer Implementation
        public void RenderData(int score, GameEvent gameEvent)
        {
            switch (gameEvent)
            {
                case GameEvent.Start_Game:
                    MyTMPro.text = "Score: " + score;
                    break;
                case GameEvent.Layer_Clear:
                    MyTMPro.text = "Score: " + score;
                    break;
                case GameEvent.End_Game:
                    MyTMPro.text = "Score: " + score;
                    break;
            }
        }
    }
}