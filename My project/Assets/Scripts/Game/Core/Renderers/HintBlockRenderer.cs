using System;
using System.Collections.Generic;
using System.Linq;
using Game.Core.Interface;
using Game.Core.Types;
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
        
        public void RenderData(Volume volume, GameEvent gameEvent)
        {
            switch (gameEvent)
            {
                case GameEvent.Floater_Created:
                    foreach (var b in ActiveHintBlocks) { b.Dispose();}
                    ActiveHintBlocks = new List<MonoPoolableBlock>();
            
                    //Get all floaters
                    var floaters = volume.Cells.Where(k => k.Value.IsFloater);

            
                    if (floaters.Any())
                    {
                        //Get the offset for the highest colliding Floater
                        var offset = new Vector3Int(0,-volume.Height,0);

                        foreach (var newHighestFloater in floaters)
                        {
                            var s = newHighestFloater.Key;
                            if (s.y > 0)
                            {
                                while (!volume.Cells[s + Vector3Int.down].Filled || volume.Cells[s + Vector3Int.down].IsFloater)
                                {
                                    s += Vector3Int.down; 
                                    if(s.y == 0) break;
                                }
                            }
                            var o = s - newHighestFloater.Key;
                            if (o.y > offset.y) offset = o;
                        }

                        //place the hints block based on the obtained offset from 
                        foreach (var floater in floaters)
                        {
                            var position = floater.Key + offset;
                            if (volume.Cells[position].Filled == false)
                            {
                                var h = HintBlock.Spawn();
                                h.transform.parent = transform;
                                h.transform.localPosition = position;
                                ActiveHintBlocks.Add(h);
                            }
                        }   
                    }
                    break;
            }
        }


        private List<MonoPoolableBlock> ActiveHintBlocks = new List<MonoPoolableBlock>();
        
        //Pool-able blocks
        public MonoPoolableBlock HintBlock;
    }
}