using System;

namespace Game.Core.GameInput
{
    [Serializable]
    public class TouchAxis
    {
        public bool isDown = false;
        public float value = 0;
    }
}