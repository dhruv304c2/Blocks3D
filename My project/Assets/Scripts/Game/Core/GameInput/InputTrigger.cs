using System.Collections;
using Game.Core.Singletons;
using UnityEngine;

namespace Game.Core.GameInput
{
    public class InputTrigger
    {
        private bool _value = false;
        public bool Value {
            get
            {
                var val = _value;
                if (_value == true) _value = false;
                return val;
            }
        }

        public IEnumerator Trigger()
        {
            if (Value == true) yield return null;
            _value = true; 
            yield return ResetTrigger();
        }

        private IEnumerator ResetTrigger()
        {
            yield return new WaitForSeconds(GameInputManager.TriggerResetTime);
            _value = false;
        }
    }
    
}