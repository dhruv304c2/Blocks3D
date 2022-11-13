using System.Collections;
using Game.Core.Singletons;
using UnityEngine;

namespace Game.Core.GameInput
{
    public class InputTrigger
    {
        private bool _value = false;

        public void Trigger()
        {
            _value = true;
        }

        public bool CheckTrigger()
        {
            var val = _value;
            _value = false;
            return val;
        }
    }
    
}