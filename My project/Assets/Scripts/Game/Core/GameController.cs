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

            _activeFloater = new Floater().InVolume(GameVolume.Self, new Vector3Int(3, 9, 3), FloaterType.S).WithColor(BlockColor.Red);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.DownArrow)) _activeFloater.MoveDown();
            else if (Input.GetKeyDown(KeyCode.UpArrow)) _activeFloater.MoveUp();
            else if (Input.GetKeyDown(KeyCode.W)) _activeFloater.MoveForward();
            else if (Input.GetKeyDown(KeyCode.S)) _activeFloater.MoveBack();
            else if (Input.GetKeyDown(KeyCode.A)) _activeFloater.MoveLeft();
            else if (Input.GetKeyDown(KeyCode.D)) _activeFloater.MoveRight();
            else if (Input.GetKeyDown(KeyCode.Z)) _activeFloater.RotateAlongZ();
            else if (Input.GetKeyDown(KeyCode.Y)) _activeFloater.RotateAlongY();
            else if (Input.GetKeyDown(KeyCode.X)) _activeFloater.RotateAlongX();
        }
    }
}