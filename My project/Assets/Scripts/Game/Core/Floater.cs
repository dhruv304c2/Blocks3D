using System.Numerics;
using Game.Core.Types;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

namespace Game.Core
{
    public class Floater
    {
        private Volume _volume;
        public UnitCell Center;
        private BlockColor _color;

        public Floater InVolume(Volume volume, Vector3Int startPos)
        {
            _volume = volume;
            Center = _volume.Cells[startPos];
            _volume.FillCellAtLocation(startPos,_color);
            return this;
        }

        public Floater WithColor(BlockColor color)
        {
            _color = color;
            if(Center!= null) Center.Color = color;
            return this;
        }

        public void MoveDown()
        {
            Vector3Int nextPos = Center.Postiton + Vector3Int.down;
            TryToMoveTo(nextPos);
        }
        
        public void MoveUp()
        {
            Vector3Int nextPos = Center.Postiton + Vector3Int.up;
            TryToMoveTo(nextPos);
        }

        public void MoveForward()
        {
            Vector3Int nextPos = Center.Postiton + Vector3Int.forward;
            TryToMoveTo(nextPos);
        }
        
        public void MoveBack()
        {
            Vector3Int nextPos = Center.Postiton + Vector3Int.back;
            TryToMoveTo(nextPos);
        }
        
        public void MoveRight()
        {
            Vector3Int nextPos = Center.Postiton + Vector3Int.right;
            TryToMoveTo(nextPos);
        }

        public void MoveLeft()
        {
            Vector3Int nextPos = Center.Postiton + Vector3Int.left;
            TryToMoveTo(nextPos);
        }
        
        bool TryToMoveTo(Vector3Int nextPos)
        {
            if (_volume.Cells.ContainsKey(nextPos))
            {
                if(_volume.Cells[nextPos].Filled == false) MoveTo(_volume.Cells[nextPos]);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void MoveTo(UnitCell cell)
        {
            if(cell == null) return;
            _volume.ClearCellAtLocation(Center.Postiton);
            _volume.FillCellAtLocation(cell.Postiton,_color);
            Center = cell;
        }
    }
}