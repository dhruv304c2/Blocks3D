using System;
using System.Collections.Generic;
using Game.Core.Interface;
using UnityEngine;

namespace Game.Core
{
    public class Volume: IObservableDataSource<Volume>
    {
        //Data Source Implementations
        public Action OnDataSourceChanged { get; set; }
        
        //Data
        private Dictionary<Vector3Int, UnitCell> _cells = new Dictionary<Vector3Int, UnitCell>();
        private int _height;
        private int _width;
        private int _depth;
        
        public Volume(int height, int width, int depth)
        {
            _height = height;
            _width = width;
            _depth = depth;

            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    for (int k = 0; k < _depth; k++)
                    {
                        _cells[new Vector3Int(i, j, k)] = new UnitCell(i, j, k);
                    }
                }
            }
        }
        
        // Volume Updates
        public void FillCellAtLocation(Vector3Int position)
        {
            _cells[position].Fill();
            OnDataSourceChanged.Invoke();
        }

        public void ClearCellAtLocation(Vector3Int position)
        {
            _cells[position].Clear();
            OnDataSourceChanged.Invoke();
        }

    }
}