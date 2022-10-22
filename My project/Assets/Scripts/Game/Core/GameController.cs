using System;
using Game.Core.Interface;
using Game.Core.Types;
using UnityEngine;

namespace Game.Core
{
    public class GameController: MonoBehaviour
    {
        private IObservableDataSource<Volume> GameVolume;
        public VolumeRenderer VolumeRenderer;

        private Floater _activeFloater;
        private void Start()
        {
            GameVolume = new Volume(10, 5, 5);
            GameVolume.SubscribeTo(VolumeRenderer);
            
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    for (int k = 0; k < 5; k++)
                    {
                        if( i <= k && j<1)GameVolume.Self.FillCellAtLocation(new Vector3Int(i,j,k), BlockColor.Yellow);
                    }
                }
            }

            _activeFloater = new Floater().InVolume(GameVolume.Self, new Vector3Int(3, 9, 3)).WithColor(BlockColor.Red);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.DownArrow)) _activeFloater.MoveDown();
            if (Input.GetKeyDown(KeyCode.UpArrow)) _activeFloater.MoveUp();
            if (Input.GetKeyDown(KeyCode.W)) _activeFloater.MoveForward();
            if (Input.GetKeyDown(KeyCode.S)) _activeFloater.MoveBack();
            if (Input.GetKeyDown(KeyCode.A)) _activeFloater.MoveLeft();
            if (Input.GetKeyDown(KeyCode.D)) _activeFloater.MoveRight();
        }
    }
}