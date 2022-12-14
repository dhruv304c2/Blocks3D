using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace Game.Core.GameInput
{
    public class MonoObjectInputTrigger: MonoBehaviour, IPointerUpHandler
    {
        public MoveType moveType;

        private void OnMouseUp()
        {
            Trigger();
        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            Trigger();
        }
        
        public void Trigger()
        {
            switch (moveType)
            {
                case MoveType.Right:
                    GameInputManager.Instance.moveRightTrigger.Trigger();
                    break;
                case MoveType.Left:
                    GameInputManager.Instance.moveLeftTrigger.Trigger();
                    break;
                case MoveType.Backward:
                    GameInputManager.Instance.moveBackwardTrigger.Trigger();
                    break;
                case MoveType.Forward:
                    GameInputManager.Instance.moveForwardTrigger.Trigger();
                    break;
                case MoveType.QuickDrop:
                    GameInputManager.Instance.quickDropTrigger.Trigger();
                    break;
                case MoveType.RotateFloaterX:
                    GameInputManager.Instance.rotateXTrigger.Trigger();
                    break;
                case MoveType.RotateFloaterY:
                    GameInputManager.Instance.rotateYTrigger.Trigger();
                    break;
                case MoveType.RotateFloaterZ:
                    GameInputManager.Instance.rotateZTrigger.Trigger();
                    break;
            }
        }
    }
}