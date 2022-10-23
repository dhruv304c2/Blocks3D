﻿using System;
using System.Collections.Generic;
using Game.Core.Interface;
using Game.Core.Types;
using UnityEngine;

namespace Game.Core
{
    public class Volume: IObservableDataSource<Volume>
    {
        //Data Source Implementations
        public Action OnDataSourceChanged { get; set; }
        
        //Data
        private Dictionary<Vector3Int, UnitCell> _cells = new Dictionary<Vector3Int, UnitCell>();
        public Dictionary<Vector3Int, UnitCell> Cells => _cells;
        
        private int _height;
        public int Height => _height;
        
        private int _width;
        public int Width => _width;
        
        private int _depth;
        public int Depth => _depth;
        
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
                        _cells[new Vector3Int(i, j, k)] = new UnitCell(new Vector3Int(i, j, k));
                    }
                }
            }
        }
        
        // Volume Updates
        public void FillCellAtLocation(Vector3Int position, BlockColor color, bool isFloater = false)
        {
            _cells[position].Fill(color, isFloater);
            OnDataSourceChanged.Invoke();
        }

        public void ClearCellAtLocation(Vector3Int position)
        {
            _cells[position].Clear();
            OnDataSourceChanged.Invoke();
        }

    }
}