using System.Collections.Generic;
using Game.Core.Singletons;
using UnityEngine;

namespace Game.Core.GameInput
{
    public class GameInputManager: Singleton<GameInputManager>
    {
        public static float TriggerResetTime = 0.01f;

        public InputTrigger moveRightTrigger  = new InputTrigger();
        public InputTrigger moveLeftTrigger  = new InputTrigger();
        public InputTrigger moveForwardTrigger  = new InputTrigger();
        public InputTrigger moveBackwardTrigger = new InputTrigger();
        public InputTrigger quickDropTrigger  = new InputTrigger();
        public InputTrigger rotateXTrigger = new InputTrigger();
        public InputTrigger rotateYTrigger = new InputTrigger();
        public InputTrigger rotateZTrigger = new InputTrigger();

        private static Dictionary<string, TouchAxis> _touchAxisDictionary = new Dictionary<string, TouchAxis>();

        public static void RegisterTouchAxis(string name, TouchAxis touchAxis)
        {
            _touchAxisDictionary.Add(name, touchAxis);
            Debug.Log($"Axis registered with name {name}");
        }

        public static TouchAxis GetAxis(string name)
        {
            if (_touchAxisDictionary.ContainsKey(name) == true)
            {
                return _touchAxisDictionary[name];
            }
            Debug.Log($"Touch axis with name {name} not found");
            return null;
        }
    }
    
    public enum MoveType
    {
        Right,
        Left,
        Forward,
        Backward,
        QuickDrop,
        RotateFloaterX,
        RotateFloaterY,
        RotateFloaterZ
    }
}