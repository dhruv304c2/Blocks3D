using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        public Transform GameView;

        public float floaterMoveTimeStep = 0.5f;
        public float viewRotationsSpeed = 3f;
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
            if (Input.GetKeyDown(KeyCode.W)) _activeFloater.MoveForward();
            else if (Input.GetKeyDown(KeyCode.S)) _activeFloater.MoveBack();
            else if (Input.GetKeyDown(KeyCode.A)) _activeFloater.MoveLeft();
            else if (Input.GetKeyDown(KeyCode.D)) _activeFloater.MoveRight();
            else if (Input.GetKeyDown(KeyCode.Z)) _activeFloater.RotateAlongZ();
            else if (Input.GetKeyDown(KeyCode.Y)) _activeFloater.RotateAlongY();
            else if (Input.GetKeyDown(KeyCode.X)) _activeFloater.RotateAlongX();
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                _activeFloater.QuickDrop();
                _activeFloater.FixBlocks();
                ClearCompletedLayers();
                SpwanNewFloater();
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                GameView.transform.eulerAngles += new Vector3(0, viewRotationsSpeed * Time.deltaTime, 0);
            }
            
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                GameView.transform.eulerAngles += new Vector3(0, -viewRotationsSpeed * Time.deltaTime, 0);
            }
            
            //Move down periodically
            floaterMovetimer += Time.deltaTime;
            if (floaterMovetimer >= floaterMoveTimeStep)
            {
                floaterMovetimer = 0;
                var moved = _activeFloater.MoveDown();
                if (moved == false)
                {
                    _activeFloater.FixBlocks();
                    ClearCompletedLayers();
                    SpwanNewFloater();
                }
            }
            
        }
        
        //=============== GAME LOGIC ===============//

        private void SpwanNewFloater()
        {
            Array values = Enum.GetValues(typeof(FloaterType));
            Random random = new Random();
            FloaterType randomFloater = (FloaterType)values.GetValue(random.Next(values.Length));
            
            Array valuesColor = Enum.GetValues(typeof(BlockColor));
            BlockColor randomColor = (BlockColor)values.GetValue(random.Next(valuesColor.Length -1)); //not including white color while spawning
            
            _activeFloater = new Floater().InVolume(GameVolume.Self,randomFloater).FillWithColor(randomColor);
        }

        private void ClearCompletedLayers()
        {
            HashSet<int> distinctLevels = new HashSet<int>();
            distinctLevels.Add(_activeFloater.Center.y);
            distinctLevels.Add(_activeFloater.Cell1.y);
            distinctLevels.Add(_activeFloater.Cell2.y);
            distinctLevels.Add(_activeFloater.Cell3.y);

            foreach (var level in distinctLevels)
            {
                if (IsSurfaceIsComplete(level)) StartCoroutine(ClearLevel(level));
            }
        }

        private IEnumerator ClearLevel(int level)
        {
            var levelBlockPos = GameVolume.Self.Cells.Keys.Where(k => k.y == level);
            Dictionary<Vector3Int, BlockColor> colorDic = new Dictionary<Vector3Int, BlockColor>();
            foreach (var blockPos in levelBlockPos)
            {
                colorDic.Add(blockPos, GameVolume.Self.Cells[blockPos].Color);
                GameVolume.Self.FillCellAtLocation(blockPos, BlockColor.White);
            }
            yield return new WaitForSeconds(0.1f);
            foreach (var blockPos in levelBlockPos)
            {
                GameVolume.Self.ClearCellAtLocation(blockPos);
            }
            
            // Dropping the blocks above down;
            for (int j = level + 1; j < GameVolume.Self.Height; j++)
            {
                for (int i = 0; i < GameVolume.Self.Width; i++)
                {
                    for (int k = 0; k < GameVolume.Self.Depth; k++)
                    {
                        if (GameVolume.Self.Cells[new Vector3Int(i, j, k)].Filled && !GameVolume.Self.Cells[new Vector3Int(i, j, k)].IsFloater)
                        {
                            GameVolume.Self.ClearCellAtLocation(new Vector3Int(i, j, k));
                            GameVolume.Self.FillCellAtLocation(new Vector3Int(i, j-1, k), GameVolume.Self.Cells[new Vector3Int(i, j, k)].Color);
                        }
                    }
                }
            }
        }

        private bool IsSurfaceIsComplete(int y)
        {
            for (int i = 0; i < GameVolume.Self.Width; i++)
            {
                for (int k = 0; k < GameVolume.Self.Depth; k++)
                {
                    if (GameVolume.Self.Cells[new Vector3Int(i, y, k)].Filled == false)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}