using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace Game.Core.GameInput
{
    public class MonoObjectInputTrigger: MonoBehaviour
    {
        public MoveType moveType;

        private void OnMouseUpAsButton()
        {
            OnClick();
        }

        public void OnClick()
        {
            switch (moveType)
            {
                case MoveType.Right:
                    StartCoroutine(GameInputManager.Instance.moveRightTrigger.Trigger());
                    break;
                case MoveType.Left:
                    StartCoroutine(GameInputManager.Instance.moveLeftTrigger.Trigger());
                    break;
                case MoveType.Backward:
                    StartCoroutine(GameInputManager.Instance.moveBackwardTrigger.Trigger());
                    break;
                case MoveType.Forward:
                    StartCoroutine(GameInputManager.Instance.moveForwardTrigger.Trigger());
                    break;
                case MoveType.QuickDrop:
                    StartCoroutine(GameInputManager.Instance.quickDropTrigger.Trigger());
                    break;
                case MoveType.RotateFloaterX:
                    StartCoroutine(GameInputManager.Instance.rotateXTrigger.Trigger());
                    break;
                case MoveType.RotateFloaterY:
                    StartCoroutine(GameInputManager.Instance.rotateYTrigger.Trigger());
                    break;
                case MoveType.RotateFloaterZ:
                    StartCoroutine(GameInputManager.Instance.rotateZTrigger.Trigger());
                    break;
            }
        }
    }
}