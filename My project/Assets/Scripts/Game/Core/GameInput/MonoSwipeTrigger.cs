using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Core.GameInput
{
    public class MonoSwipeTrigger: MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        public MoveType moveType;
        public float swipeDistance;
        public AxisConstraint axisConstraints;
        public bool invert = false;

        private float startPositionX;
        private float startPositionY;

        public void OnPointerDown(PointerEventData eventData)
        {
            startPositionX = eventData.position.x;
            startPositionY = eventData.position.y;
        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            float rawVal = 0;
            
            switch (axisConstraints)
            {
                case AxisConstraint.XAxis:
                    rawVal = eventData.position.x - startPositionX;
                    break;
                case AxisConstraint.YAxis:
                    rawVal = eventData.position.y - startPositionY;
                    break;
            }

            if (invert) rawVal = -rawVal;

            if (Mathf.Abs(rawVal) >= swipeDistance)
            {
                Trigger();
            }
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