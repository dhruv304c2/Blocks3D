using System;
using System.Collections.Generic;
using System.Linq;
using Game.Core.Interface;
using UnityEngine;

//This Data Renderer is responsible for rendering the Hint blocks based on Volume data
//The Renderer picks block from object pool and adds them to required locations 
namespace Game.Core.Renderers
{
    public class HintBlockRenderer: MonoBehaviour, IDataRenderer<Volume>
    {
        private void Awake()
        {
            if (!HintBlock.Initialised) HintBlock.InitialisePool(10);
        }

        public IObservableDataSource<Volume> DataSource { get; set; }
        public void RenderData()
        {
            foreach (var b in ActiveHintBlocks) { b.Dispose();}
            ActiveHintBlocks = new List<MonoPoolableBlock>();
            
            //Get all floaters
            var floaters = DataSource.Self.Cells.Where(k => k.Value.IsFloater);

            if (floaters.Any())
            {
                var offset = new Vector3Int(0,-DataSource.Self.Height,0);

                foreach (var newLowestFloater in floaters)
                {
                    var s = newLowestFloater.Key;
                    if (s.y > 0)
                    {
                        while (!DataSource.Self.Cells[s + Vector3Int.down].Filled || DataSource.Self.Cells[s + Vector3Int.down].IsFloater)
                        {
                            s += Vector3Int.down; 
                            if(s.y == 0) break;
                        }
                    }
                    var o = s - newLowestFloater.Key;
                    if (o.y > offset.y) offset = o;
                }

                foreach (var floater in floaters)
                {
                    var position = floater.Key + offset;
                    if (DataSource.Self.Cells[position].Filled == false)
                    {
                        var h = HintBlock.Spawn();
                        h.transform.parent = transform;
                        h.transform.localPosition = position;
                        ActiveHintBlocks.Add(h);
                    }
                }   
            }
        }


        private List<MonoPoolableBlock> ActiveHintBlocks = new List<MonoPoolableBlock>();
        
        //Pool-able blocks
        public MonoPoolableBlock HintBlock;
    }
}