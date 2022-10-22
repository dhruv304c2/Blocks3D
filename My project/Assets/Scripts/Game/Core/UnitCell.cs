using System.Collections;
using System.Collections.Generic;
using Game.Core.Types;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

namespace Game.Core
{
    public class UnitCell
    {
        private Vector3Int _position;
        public Vector3Int Postiton => _position;
        public bool Filled { get; private set; }
        public BlockColor Color { get; set; }
        public GameObject BlockObject;

        public UnitCell(Vector3Int position)
        {
            _position = position;
        }

        public UnitCell Fill(BlockColor color)
        {
            Filled = true;
            Color = color;
            return this;
        }

        public UnitCell Clear()
        {
            Filled = false;
            return this;
        } 
    }
}

