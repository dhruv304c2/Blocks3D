using System;
using System.Collections.Generic;
using Game.Core.Interface;
using Game.Core.Types;
using UnityEngine;

namespace Game.Core
{
    public class Volume: IObservableDataSource<UnitCell>, IObservableDataSource<Volume>
    {
        //Data
        private Dictionary<Vector3Int, UnitCell> _cells = new Dictionary<Vector3Int, UnitCell>();
        public Dictionary<Vector3Int, UnitCell> Cells => _cells;
        
        private int _height;
        public int Height => _height;
        
        private int _width;
        public int Width => _width;
        
        private int _depth;
        private Action<Volume, GameEvent> _onDataSourceChanged;
        private List<IEventListener<UnitCell>> _listeners;
        private List<IEventListener<Volume>> _listeners1;
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
            try
            {
                ((IObservableDataSource<UnitCell>)this).Notify(Cells[position],GameEvent.Cell_Fill);
                if(isFloater)
                    ((IObservableDataSource<Volume>)this).Notify(this,GameEvent.Floater_Created);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        public void ClearCellAtLocation(Vector3Int position)
        {
            _cells[position].Clear();
            try
            {
                ((IObservableDataSource<UnitCell>)this).Notify(Cells[position],GameEvent.Cell_Clear);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        List<IEventListener<UnitCell>> IObservableDataSource<UnitCell>.Listeners
        {
            get => _listeners;
            set => _listeners = value;
        }

        List<IEventListener<Volume>> IObservableDataSource<Volume>.Listeners
        {
            get => _listeners1;
            set => _listeners1 = value;
        }
    }
}