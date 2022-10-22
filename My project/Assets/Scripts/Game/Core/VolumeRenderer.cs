using System.Collections;
using System.Collections.Generic;
using Game.Core;
using Game.Core.Interface;
using UnityEngine;

public class VolumeRenderer : MonoBehaviour, IDataRenderer<Volume>
{
    // Start is called before the first frame update
    void Start()
    {
        DataSource = new Volume(5, 5, 5);
        DataSource.SubscribeTo(this);

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                for (int k = 0; k < 5; k++)
                {
                    if(j<1)DataSource.Self.FillCellAtLocation(new Vector3Int(i,j,k));
                }
            }
        }
        
        RenderData();
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
                var b = Instantiate(BlockPrefab, transform, true);
                b.transform.parent = transform;
                b.transform.localPosition = kvp.Key;
                kvp.Value.BlockObject = b;
            }
            else
            {
                if (kvp.Value.BlockObject != null) Destroy(BlockPrefab);
            }
        }
    }


    public Transform Base;
    public GameObject BlockPrefab;
}
