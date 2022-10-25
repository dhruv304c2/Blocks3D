using System.Numerics;
using Game.Core.Types;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

namespace Game.Core
{
    public class Floater
    {
        private Volume _volume;
        private BlockConstructionVectors _myConstructionVector = new BlockConstructionVectors();
        
        public Vector3Int Center;
        public Vector3Int Cell1 => Center + _myConstructionVector.Vector1;
        public Vector3Int Cell2 => Center + _myConstructionVector.Vector2;
        public Vector3Int Cell3 => Center + _myConstructionVector.Vector3;

        private BlockColor _color;
        private FloaterType _type;

        public Floater InVolume(Volume volume, Vector3Int startPos, FloaterType type)
        {
            _volume = volume;
            Center = new Vector3Int(_volume.Width / 2, _volume.Height / 2, _volume.Depth -4);
            _type = type;
            _myConstructionVector.GetConstructionVectors(_type);
            FillMe();
            return this;
        }

        public Floater WithColor(BlockColor color)
        {
            _color = color;
            if(Center!= null) _volume.Cells[Center].Color = color;
            if(Cell1!= null) _volume.Cells[Cell1].Color = color;
            if(Cell2!= null) _volume.Cells[Cell2].Color = color;
            if(Cell3!= null) _volume.Cells[Cell3].Color = color;
            return this;
        }

        void FillMe()
        {
            _volume.FillCellAtLocation(Center,_color,true);
            _volume.FillCellAtLocation(Cell1,_color,true);
            _volume.FillCellAtLocation(Cell2,_color,true);
            _volume.FillCellAtLocation(Cell3,_color,true);    
        }

        void ClearMe()
        {
            _volume.ClearCellAtLocation(Center);
            _volume.ClearCellAtLocation(Cell1);
            _volume.ClearCellAtLocation(Cell2);
            _volume.ClearCellAtLocation(Cell3);    
        }
        
        
        //============== FLOATER MOVEMENT LOGIC ===============//
        
        public void MoveDown()
        {
            //Checking if all the blocks can be moved to next position
            Vector3Int nextPosC = Center + Vector3Int.down;
            if(CanMoveTo(nextPosC) == false) return;
            Vector3Int nextPosC1 = Cell1 + Vector3Int.down;
            if(CanMoveTo(nextPosC1) == false) return;
            Vector3Int nextPosC2 = Cell2 + Vector3Int.down;
            if(CanMoveTo(nextPosC2) == false) return;
            Vector3Int nextPosC3 = Cell3 + Vector3Int.down;
            if(CanMoveTo(nextPosC3) == false) return;

            //Move if all blocks can be moved
            ClearMe();
            
            Center = nextPosC;

            FillMe();
        }
        
        public void MoveUp()
        {
            //Checking if all the blocks can be moved to next position
            Vector3Int nextPosC = Center + Vector3Int.up;
            if(CanMoveTo(nextPosC) == false) return;
            Vector3Int nextPosC1 = Cell1 + Vector3Int.up;
            if(CanMoveTo(nextPosC1) == false) return;
            Vector3Int nextPosC2 = Cell2 + Vector3Int.up;
            if(CanMoveTo(nextPosC2) == false) return;
            Vector3Int nextPosC3 = Cell3 + Vector3Int.up;
            if(CanMoveTo(nextPosC3) == false) return;

            //Move if all blocks can be moved
            ClearMe();
            
            Center = nextPosC;

            FillMe();
        }

        public void MoveForward()
        {
            //Checking if all the blocks can be moved to next position
            Vector3Int nextPosC = Center + Vector3Int.forward;
            if(CanMoveTo(nextPosC) == false) return;
            Vector3Int nextPosC1 = Cell1 + Vector3Int.forward;
            if(CanMoveTo(nextPosC1) == false) return;
            Vector3Int nextPosC2 = Cell2 + Vector3Int.forward;
            if(CanMoveTo(nextPosC2) == false) return;
            Vector3Int nextPosC3 = Cell3 + Vector3Int.forward;
            if(CanMoveTo(nextPosC3) == false) return;

            //Move if all blocks can be moved
            ClearMe();
            
            Center = nextPosC;

            FillMe();
        }
        
        public void MoveBack()
        {
            //Checking if all the blocks can be moved to next position
            Vector3Int nextPosC = Center + Vector3Int.back;
            if(CanMoveTo(nextPosC) == false) return;
            Vector3Int nextPosC1 = Cell1 + Vector3Int.back;
            if(CanMoveTo(nextPosC1) == false) return;
            Vector3Int nextPosC2 = Cell2 + Vector3Int.back;
            if(CanMoveTo(nextPosC2) == false) return;
            Vector3Int nextPosC3 = Cell3 + Vector3Int.back;
            if(CanMoveTo(nextPosC3) == false) return;

            //Move if all blocks can be moved
            ClearMe();
            
            Center = nextPosC;

            FillMe();
        }
        
        public void MoveRight()
        {
            //Checking if all the blocks can be moved to next position
            Vector3Int nextPosC = Center + Vector3Int.right;
            if(CanMoveTo(nextPosC) == false) return;
            Vector3Int nextPosC1 = Cell1 + Vector3Int.right;
            if(CanMoveTo(nextPosC1) == false) return;
            Vector3Int nextPosC2 = Cell2 + Vector3Int.right;
            if(CanMoveTo(nextPosC2) == false) return;
            Vector3Int nextPosC3 = Cell3 + Vector3Int.right;
            if(CanMoveTo(nextPosC3) == false) return;

            //Move if all blocks can be moved
            ClearMe();
            
            Center = nextPosC;

            FillMe();
        }

        public void MoveLeft()
        {
            //Checking if all the blocks can be moved to next position
            Vector3Int nextPosC = Center + Vector3Int.left;
            if(CanMoveTo(nextPosC) == false) return;
            Vector3Int nextPosC1 = Cell1 + Vector3Int.left;
            if(CanMoveTo(nextPosC1) == false) return;
            Vector3Int nextPosC2 = Cell2 + Vector3Int.left;
            if(CanMoveTo(nextPosC2) == false) return;
            Vector3Int nextPosC3 = Cell3 + Vector3Int.left;
            if(CanMoveTo(nextPosC3) == false) return;

            //Move if all blocks can be moved
            ClearMe();
            
            Center = nextPosC;

            FillMe();
        }

        private bool CanMoveTo(Vector3Int position)
        {
            if (_volume.Cells.ContainsKey(position))
            {
                return !(_volume.Cells[position].Filled && !_volume.Cells[position].IsFloater);
            }
            return false;
        }

        public void RotateAlongZ()
        {
            ClearMe();
            _myConstructionVector.RotateAlongZBy(BlockRotation.PiBy2);
            FillMe();
        }

        //======CONSTRUCTION OF FLOATERS BASED ON CONSTRUCTION VECTORS============//

        public class BlockConstructionVectors
        {
            public Vector3Int Vector1;
            public Vector3Int Vector2;
            public Vector3Int Vector3;
            
            public BlockConstructionVectors GetConstructionVectors(FloaterType type)
            {
                switch (type)
                {
                    case FloaterType.I:
                        Vector1 = new Vector3Int(0, 2, 0);
                        Vector2 = new Vector3Int(0, 1, 0);
                        Vector3 = new Vector3Int(0, -1, 0);
                        break;
                    case FloaterType.L:
                        Vector1 = new Vector3Int(0, 1, 0);
                        Vector2 = new Vector3Int(0, -1, 0);
                        Vector3 = new Vector3Int(1, -1, 0);
                        break;
                    case FloaterType.O:
                        Vector1 = new Vector3Int(-1, 0, 0);
                        Vector2 = new Vector3Int(0, -1, 0);
                        Vector3 = new Vector3Int(-1, -1, 0);
                        break;
                    case FloaterType.S:
                        Vector1 = new Vector3Int(1, 0, 0);
                        Vector2 = new Vector3Int(0, -1, 0);
                        Vector3 = new Vector3Int(-1, -1, 0);
                        break;
                    default:
                        Vector1 = new Vector3Int(-1, 0, 0);
                        Vector2 = new Vector3Int(1, 0, 0);
                        Vector3 = new Vector3Int(0, -1, 0);
                        break;
                }

                return this;
            }

            public BlockConstructionVectors RotateAlongZBy(BlockRotation rotation)
            {
                switch (rotation)
                {
                    case BlockRotation.Zero:
                        break;
                    case BlockRotation.PiBy2:
                        Vector1 = new Vector3Int(-Vector1.y, Vector1.x, Vector1.z);
                        Vector2 = new Vector3Int(-Vector2.y, Vector2.x, Vector2.z);
                        Vector3 = new Vector3Int(-Vector3.y, Vector3.x, Vector3.z);
                        break;
                    case BlockRotation.Pi:
                        Vector1 = new Vector3Int(-Vector1.x, -Vector1.y, Vector1.z);
                        Vector2 = new Vector3Int(-Vector2.x, -Vector2.y, Vector2.z);
                        Vector3 = new Vector3Int(-Vector3.x, -Vector3.y, Vector3.z);
                        break;
                    case BlockRotation._3PiBy2:
                        Vector1 = new Vector3Int(Vector1.y, -Vector1.x, Vector1.z);
                        Vector2 = new Vector3Int(Vector2.y, -Vector2.x, Vector2.z);
                        Vector3 = new Vector3Int(Vector3.y, -Vector3.x, Vector3.z);
                        break;
                }
                return this;
            }
        }
        
        
    }
}