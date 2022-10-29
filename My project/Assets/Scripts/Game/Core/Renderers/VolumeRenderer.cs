using System.Collections;
using System.Collections.Generic;
using Game.Core;
using Game.Core.Interface;
using Game.Core.Types;
using Unity.VisualScripting;
using UnityEngine;


//This Data Renderer is responsible for rendering the blocks based on Volume data
//The Renderer picks block from object pool and adds them to required locations 
namespace Game.Core.Renderers
{
    public class VolumeRenderer : MonoBehaviour, IDataRenderer<Volume>
    {
        void Awake()
        {
            if( !orangeBlockPrefab.Initialised ) orangeBlockPrefab.InitialisePool(500);
            if( !yellowBlockPrefab.Initialised ) yellowBlockPrefab.InitialisePool(500);
            if( !greenBlockPrefab.Initialised ) greenBlockPrefab.InitialisePool(500);
            if( !purpleBlockPrefab.Initialised ) purpleBlockPrefab.InitialisePool(500);
            if( !whiteBlockPrefab.Initialised ) whiteBlockPrefab.InitialisePool(100);
        }
        
        //Data Renderer Implementation
        public IObservableDataSource<Volume> DataSource { get; set; }
    
        public void RenderData()
        {
            
            //Disposing all active blocks and resetting the list
            foreach (var block in ActiveBlocks)
            {
                block.Dispose();
            }
    
            ActiveBlocks = new List<MonoPoolableBlock>();
            
            foreach (var kvp in DataSource.Self.Cells)
            {
                if (kvp.Value.Filled)
                {
                    MonoPoolableBlock pool = GetBlock(kvp.Value.Color);
                    var b = pool.Spawn();
                    var transform1 = b.transform;
                    transform1.parent = transform;
                    transform1.localPosition = kvp.Key;
                    ActiveBlocks.Add(b);
                }
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
        
        private List<MonoPoolableBlock> ActiveBlocks = new List<MonoPoolableBlock>();
        
        
        //Pool-able Blocks
        public MonoPoolableBlock orangeBlockPrefab;
        public MonoPoolableBlock yellowBlockPrefab;
        public MonoPoolableBlock greenBlockPrefab;
        public MonoPoolableBlock purpleBlockPrefab;
        public MonoPoolableBlock whiteBlockPrefab;
    }
}

