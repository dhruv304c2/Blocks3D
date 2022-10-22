using System.Collections;
using System.Collections.Generic;
using Game.Core;
using Game.Core.Interface;
using Game.Core.Types;
using UnityEngine;

public class VolumeRenderer : MonoBehaviour, IDataRenderer<Volume>
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Data Renderer Implementation
    public IObservableDataSource<Volume> DataSource { get; set; }

    public void RenderData()
    {
        foreach (var kvp in DataSource.Self.Cells)
        {
            if (kvp.Value.Filled == true)
            {
                GameObject prefab = GetBlock(kvp.Value.Color);
                var b = Instantiate(prefab, transform, true);
                b.transform.parent = transform;
                b.transform.localPosition = kvp.Key;
                kvp.Value.BlockObject = b;
            }
            else
            {
                if (kvp.Value.BlockObject != null)
                {
                    Destroy(kvp.Value.BlockObject);
                }
            }
        }
    }

    public GameObject GetBlock(BlockColor blockColor)
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
    public GameObject RedBlockPrefab;
    public GameObject YellowBlockPrefab;
}
