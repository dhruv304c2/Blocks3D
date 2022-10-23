using System.Collections;
using System.Collections.Generic;
using Game.Core;
using Game.Core.Interface;
using Game.Core.Types;
using Unity.VisualScripting;
using UnityEngine;

public class VolumeRenderer : MonoBehaviour, IDataRenderer<Volume>
{
    // Start is called before the first frame update
    void Awake()
    {
        if( !RedBlockPrefab.Initialised ) RedBlockPrefab.InitialisePool(500);
        if( !YellowBlockPrefab.Initialised ) YellowBlockPrefab.InitialisePool(500);
    }

    // Update is called once per frame
    void Update()
    {
        
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
                b.transform.parent = transform;
                b.transform.localPosition = kvp.Key;
                ActiveBlocks.Add(b);
            }
        }
    }

    public MonoPoolableBlock GetBlock(BlockColor blockColor)
    {
        switch (blockColor)
        {
            case BlockColor.Red:
                return RedBlockPrefab;
            case BlockColor.Yellow:
                return YellowBlockPrefab;
        }

        return null;
    }


    public Transform Base;
    public MonoPoolableBlock RedBlockPrefab;
    public MonoPoolableBlock YellowBlockPrefab;
    public List<MonoPoolableBlock> ActiveBlocks;
}
