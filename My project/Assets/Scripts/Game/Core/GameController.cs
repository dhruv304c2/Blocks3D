using System;
using Game.Core.Interface;
using Game.Core.Renderers;
using Game.Core.Types;
using UnityEngine;
using Random = System.Random;

namespace Game.Core
{
    public class GameController: MonoBehaviour
    {
        private IObservableDataSource<Volume> GameVolume;
        public VolumeRenderer VolumeRenderer;
        public HintBlockRenderer HintBlockRenderer;

        public float floaterMoveTimeStep = 0.5f;
        private float floaterMovetimer;

        private Floater _activeFloater;
        private void Start()
        {
            GameVolume = new Volume(15, 6, 6);
            GameVolume.SubscribeTo(VolumeRenderer);
            GameVolume.SubscribeTo(HintBlockRenderer);
            
            /*for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    for (int k = 0; k < 5; k++)
                    {
                        if( i <= k && j<1)GameVolume.Self.FillCellAtLocation(new Vector3Int(i,j,k), BlockColor.Yellow);
                    }
                }
            }*/

            SpwanNewFloater();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow)) _activeFloater.MoveUp();
            else if (Input.GetKeyDown(KeyCode.W)) _activeFloater.MoveForward();
            else if (Input.GetKeyDown(KeyCode.S)) _activeFloater.MoveBack();
            else if (Input.GetKeyDown(KeyCode.A)) _activeFloater.MoveLeft();
            else if (Input.GetKeyDown(KeyCode.D)) _activeFloater.MoveRight();
            else if (Input.GetKeyDown(KeyCode.Z)) _activeFloater.RotateAlongZ();
            else if (Input.GetKeyDown(KeyCode.Y)) _activeFloater.RotateAlongY();
            else if (Input.GetKeyDown(KeyCode.X)) _activeFloater.RotateAlongX();
            
            //Move down periodically
            floaterMovetimer += Time.deltaTime;
            if (floaterMovetimer >= floaterMoveTimeStep)
            {
                floaterMovetimer = 0;
                var moved = _activeFloater.MoveDown();
                if (moved == false)
                {
                    _activeFloater.ReleaseBlocks();
                    SpwanNewFloater();
                }
            }
            
        }

        private void SpwanNewFloater()
        {
            Array values = Enum.GetValues(typeof(FloaterType));
            Random random = new Random();
            FloaterType randomFloater = (FloaterType)values.GetValue(random.Next(values.Length));
            
            Array valuesColor = Enum.GetValues(typeof(BlockColor));
            BlockColor randomColor = (BlockColor)values.GetValue(random.Next(valuesColor.Length));
            
            _activeFloater = new Floater().InVolume(GameVolume.Self,randomFloater).FillWithColor(randomColor);
        }
    }
}