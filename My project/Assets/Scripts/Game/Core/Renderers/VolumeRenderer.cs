using System.Collections.Generic;
using Game.Core.Interface;
using Game.Core.Types;
using UnityEngine;


//This Data Renderer is responsible for rendering the blocks based on Volume data
//The Renderer picks block from object pool and adds them to required locations 
namespace Game.Core.Renderers
{
    public class VolumeRenderer : MonoBehaviour, IDataRenderer<UnitCell>
    {
        void Awake()
        {
            if( !orangeBlockPrefab.Initialised ) orangeBlockPrefab.InitialisePool(500);
            if( !yellowBlockPrefab.Initialised ) yellowBlockPrefab.InitialisePool(500);
            if( !greenBlockPrefab.Initialised ) greenBlockPrefab.InitialisePool(500);
            if( !purpleBlockPrefab.Initialised ) purpleBlockPrefab.InitialisePool(500);
            if( !whiteBlockPrefab.Initialised ) whiteBlockPrefab.InitialisePool(100);
        }

        public void RenderData(UnitCell cell, GameEvent gameEvent)
        {
            switch (gameEvent)
            {
                case GameEvent.Cell_Fill:
                    if (ActiveBlocks.ContainsKey(cell.Postiton)) //Dispose any existing block at this position
                    {
                        ActiveBlocks[cell.Postiton].Dispose(); 
                    }
                    MonoPoolableBlock pool = GetBlock(cell.Color);
                    var b = pool.Spawn();
                    var transform1 = b.transform;
                    transform1.parent = transform;
                    transform1.localPosition = cell.Postiton;
                    ActiveBlocks[cell.Postiton] = b;
                    break;
                case GameEvent.Cell_Clear:
                    ActiveBlocks[cell.Postiton].Dispose();
                    break;
            }
        }
    
        public MonoPoolableBlock GetBlock(BlockColor blockColor)
        {
            switch (blockColor)
            {
                case BlockColor.Orange:
                    return orangeBlockPrefab;
                case BlockColor.Yellow:
                    return yellowBlockPrefab;
                case BlockColor.Green:
                    return greenBlockPrefab;
                case BlockColor.Purple:
                    return purpleBlockPrefab;
                case BlockColor.White:
                    return whiteBlockPrefab;
            }
    
            return null;
        }
        
        private Dictionary<Vector3, MonoPoolableBlock> ActiveBlocks = new Dictionary<Vector3, MonoPoolableBlock>();
        
        
        //Pool-able Blocks
        public MonoPoolableBlock orangeBlockPrefab;
        public MonoPoolableBlock yellowBlockPrefab;
        public MonoPoolableBlock greenBlockPrefab;
        public MonoPoolableBlock purpleBlockPrefab;
        public MonoPoolableBlock whiteBlockPrefab;
    }
}

