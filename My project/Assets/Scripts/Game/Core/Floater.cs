using System.Linq;
using System.Numerics;
using Game.Core.Singletons;
using Game.Core.Types;
using UnityEngine;
using UnityEngine.EventSystems;
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

        public Vector3Int MinXCell
        {
            get
            {
                var min = Mathf.Min(Center.x, Cell1.x, Cell2.x, Cell3.x);
                if (min == Center.x) return Center;
                else if (min == Cell1.x) return Cell1;
                else if (min == Cell2.x) return Cell2;
                else return Cell3;
            }
        }
        
        public Vector3Int MaxXCell
        {
            get
            {
                var max = Mathf.Max(Center.x, Cell1.x, Cell2.x, Cell3.x);
                if (max == Center.x) return Center;
                else if (max == Cell1.x) return Cell1;
                else if (max == Cell2.x) return Cell2;
                else return Cell3;
            }
        }
        
        public Vector3Int MinYCell
        {
            get
            {
                var min = Mathf.Min(Center.y, Cell1.y, Cell2.y, Cell3.y);
                if (min == Center.y) return Center;
                else if (min == Cell1.y) return Cell1;
                else if (min == Cell2.y) return Cell2;
                else return Cell3;
            }
        }
        
        public Vector3Int MaxYCell
        {
            get
            {
                var max = Mathf.Max(Center.y, Cell1.y, Cell2.y, Cell3.y);
                if (max == Center.y) return Center;
                else if (max == Cell1.y) return Cell1;
                else if (max == Cell2.y) return Cell2;
                else return Cell3;
            }
        }
        
        public Vector3Int MinZCell
        {
            get
            {
                var min = Mathf.Min(Center.z, Cell1.z, Cell2.z, Cell3.z);
                if (min == Center.z) return Center;
                else if (min == Cell1.z) return Cell1;
                else if (min == Cell2.z) return Cell2;
                else return Cell3;
            }
        }
        
        public Vector3Int MaxZCell
        {
            get
            {
                var max = Mathf.Max(Center.z, Cell1.z, Cell2.z, Cell3.z);
                if (max == Center.z) return Center;
                else if (max == Cell1.z) return Cell1;
                else if (max == Cell2.z) return Cell2;
                else return Cell3;
            }
        }


        private BlockColor _color;
        private FloaterType _type;

        public Floater InVolume(Volume volume, FloaterType type)
        {
            _volume = volume;
            Center = new Vector3Int(_volume.Width / 2, _volume.Height - 4 , _volume.Depth/2);
            _type = type;
            _myConstructionVector.GetConstructionVectors(_type);
            return this;
        }

        public Floater FillWithColor(BlockColor color)
        {
            _color = color;
            if(Center!= null) _volume.Cells[Center].Color = color;
            if(Cell1!= null) _volume.Cells[Cell1].Color = color;
            if(Cell2!= null) _volume.Cells[Cell2].Color = color;
            if(Cell3!= null) _volume.Cells[Cell3].Color = color;
            FillMe();
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

        public void FixBlocks()
        {
            _volume.FillCellAtLocation(Center,_color,false);
            _volume.FillCellAtLocation(Cell1,_color,false);
            _volume.FillCellAtLocation(Cell2,_color,false);
            _volume.FillCellAtLocation(Cell3,_color,false);

            //End the game if the newly added blocks exceed the game limit
            var maxY = Mathf.Max(Center.y, Cell1.y, Cell2.y, Cell3.y);
            if(maxY >= GameDataProvider.GameHeight) GameDataProvider.Instance.EndGame();
        }
        
        
        //============== FLOATER MOVEMENT LOGIC ===============//


        #region Translation Logic
        public bool MoveDown()
        {
            //Checking if all the blocks can be moved to next position
            Vector3Int nextPosC = Center + Vector3Int.down;
            if(CanMoveTo(nextPosC) == false) return false;
            Vector3Int nextPosC1 = Cell1 + Vector3Int.down;
            if(CanMoveTo(nextPosC1) == false) return false;
            Vector3Int nextPosC2 = Cell2 + Vector3Int.down;
            if(CanMoveTo(nextPosC2) == false) return false;
            Vector3Int nextPosC3 = Cell3 + Vector3Int.down;
            if(CanMoveTo(nextPosC3) == false) return false;

            //Move if all blocks can be moved
            ClearMe();
            Center = nextPosC;
            FillMe();
            return true;
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

        public bool TryKick()
        {
            //Kick from walls
            while (MinXCell.x < 0) { Center += Vector3Int.right; }
            while (MaxXCell.x > _volume.Width -1 ) { Center += Vector3Int.left; }
            while (MinYCell.y < 0) { Center += Vector3Int.up; }
            while (MaxYCell.y > _volume.Height -1 ) { Center += Vector3Int.down; }
            while (MinZCell.z < 0) { Center += Vector3Int.forward; }
            while (MaxZCell.z > _volume.Depth -1 ) { Center += Vector3Int.back; }
            
            //Kick from blocks
            var movement_Dir  = new Vector3Int();
            if (_volume.Cells[Cell1].Filled) movement_Dir += UnitRectVector(Center - Cell1);
            if (_volume.Cells[Cell2].Filled) movement_Dir += UnitRectVector(Center - Cell2);
            if (_volume.Cells[Cell3].Filled) movement_Dir += UnitRectVector(Center - Cell3);
            Center += movement_Dir;

                //Check if new configuration is Valid
            return CanMoveTo(Center) && CanMoveTo(Cell1) && CanMoveTo(Cell2) && CanMoveTo(Cell3);
        }

        private Vector3Int UnitRectVector(Vector3Int vector)
        {
            return new Vector3Int(Mathf.Clamp(vector.x,-1,1), 
                                  Mathf.Clamp(vector.y,-1,1), 
                                  Mathf.Clamp(vector.z,-1,1));
        }

        public void QuickDrop()
        {
            //Get all floaters
            var floaters = _volume.Cells.Where(k => k.Value.IsFloater);

            
            if (floaters.Any())
            {
                //Get the offset for the highest colliding Floater
                var offset = new Vector3Int(0,-_volume.Height,0);

                foreach (var newHighestFloater in floaters)
                {
                    var s = newHighestFloater.Key;
                    if (s.y > 0)
                    {
                        while (!_volume.Cells[s + Vector3Int.down].Filled || _volume.Cells[s + Vector3Int.down].IsFloater)
                        {
                            s += Vector3Int.down; 
                            if(s.y == 0) break;
                        }
                    }
                    var o = s - newHighestFloater.Key;
                    if (o.y > offset.y) offset = o;
                }

                //Move center by the given offset
                ClearMe();
                Center += offset;
                FillMe();
            }
        }

        #endregion

        #region Rotation Logic

        public void RotateAlongZ()
        {
            ClearMe();
            var start = Center;
            _myConstructionVector.RotateAlongZBy(BlockRotation.PiBy2);
            if (TryKick() == false)
            {
                _myConstructionVector.RotateAlongZBy(BlockRotation._3PiBy2);
                Center = start;
                Debug.Log("Rotation Along Z Failed");
            }
            FillMe();
        }
        
        public void RotateAlongY()
        {
            ClearMe();
            var start = Center;
            _myConstructionVector.RotateAlongYBy(BlockRotation.PiBy2);
            if (TryKick() == false)
            {
                _myConstructionVector.RotateAlongYBy(BlockRotation._3PiBy2);
                Center = start;
                Debug.Log("Rotation Along Y Failed");
            }
            FillMe();
        }

        public void RotateAlongX()
        {
            ClearMe();
            var start = Center;
            _myConstructionVector.RotateAlongXBy(BlockRotation.PiBy2);
            if (TryKick() == false)
            {
                _myConstructionVector.RotateAlongXBy(BlockRotation._3PiBy2);
                Center = start;
                Debug.Log("Rotation Along X Failed");
            }
            FillMe();
        }

        #endregion
        

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

            public BlockConstructionVectors RotateAlongYBy(BlockRotation rotation)
            {
                switch (rotation)
                {
                    case BlockRotation.Zero:
                        break;
                    case BlockRotation.PiBy2:
                        Vector1 = new Vector3Int(Vector1.z, Vector1.y, -Vector1.x);
                        Vector2 = new Vector3Int(Vector2.z, Vector2.y, -Vector2.x);
                        Vector3 = new Vector3Int(Vector3.z, Vector3.y, -Vector3.x);
                        break;
                    case BlockRotation.Pi:
                        Vector1 = new Vector3Int(Vector1.x, Vector1.y, -Vector1.z);
                        Vector2 = new Vector3Int(Vector2.x, Vector2.y, -Vector2.z);
                        Vector3 = new Vector3Int(Vector3.x, Vector3.y, -Vector3.z);
                        break;
                    case BlockRotation._3PiBy2:
                        Vector1 = new Vector3Int(-Vector1.z, Vector1.y, Vector1.x);
                        Vector2 = new Vector3Int(-Vector2.z, Vector2.y, Vector2.x);
                        Vector3 = new Vector3Int(-Vector3.z, Vector3.y, Vector3.x);
                        break;
                }
                return this;
            }

            public BlockConstructionVectors RotateAlongXBy(BlockRotation rotation)
            {
                switch (rotation)
                {
                    case BlockRotation.Zero:
                        break;
                    case BlockRotation.PiBy2:
                        Vector1 = new Vector3Int(Vector1.x, -Vector1.z, Vector1.y);
                        Vector2 = new Vector3Int(Vector2.x, -Vector2.z, Vector2.y);
                        Vector3 = new Vector3Int(Vector3.x, -Vector3.z, Vector3.y);
                        break;
                    case BlockRotation.Pi:
                        Vector1 = new Vector3Int(Vector1.x, -Vector1.y, -Vector1.z);
                        Vector2 = new Vector3Int(Vector2.x, -Vector2.y, -Vector2.z);
                        Vector3 = new Vector3Int(Vector3.x, -Vector3.y, -Vector3.z);
                        break;
                    case BlockRotation._3PiBy2:
                        Vector1 = new Vector3Int(Vector1.x, Vector1.z, -Vector1.y);
                        Vector2 = new Vector3Int(Vector2.x, Vector2.z, -Vector2.y);
                        Vector3 = new Vector3Int(Vector3.x, Vector3.z, -Vector3.y);
                        break;
                }
                return this;    
            }
        }
        
        
    }
}