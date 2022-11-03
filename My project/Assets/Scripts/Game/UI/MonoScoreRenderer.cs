using System;
using Game.Core.Interface;
using Game.Core.Singletons;
using UnityEngine;
using TMPro;

namespace Game.UI
{
    public class MonoScoreRenderer: MonoBehaviour, IDataRenderer<GameDataProvider>
    {
        private void Awake()
        {
            ((IObservableDataSource<GameDataProvider>)GameDataProvider.Instance).SubscribeTo(this);
        }

        private TextMeshProUGUI MyTMPro => GetComponent<TextMeshProUGUI>();  

        // Data Renderer Implementation
        public IObservableDataSource<GameDataProvider> DataSource { get; set; }
        public void RenderData()
        {
            MyTMPro.text = "Score: " + DataSource.Self.Score;
        }
    }
}