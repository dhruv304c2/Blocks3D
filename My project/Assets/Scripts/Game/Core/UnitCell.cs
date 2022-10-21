using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
    public class UnitCell
    {
        private int _x;
        private int _y;
        private int _z;
        public bool Filled { get; private set; }

        public UnitCell(int x, int y, int z)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        public UnitCell Fill()
        {
            Filled = true;
            return this;
        }

        public UnitCell Clear()
        {
            Filled = false;
            return this;
        } 
    }
}

