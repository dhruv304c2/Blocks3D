using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Core.GameInput
{
    public class MonoSwipeTrigger: MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public MoveType moveType;
        public float speed;
        public AxisConstraint axisConstraints;
        public bool invert = false;

        private float startPositionX;
        private float startPositionY;

        private float startTime = 0;

        public void OnPointerDown(PointerEventData eventData)
        {
            startPositionX = eventData.position.x;
            startPositionY = eventData.position.y;
            startTime = 0;
            Debug.Log($"Enter at x: {eventData.position.x}, y: {eventData.position.y}");
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            float timeElapsed = Time.time - startTime;
            float rawVal = 0;
            
            switch (axisConstraints)
            {
                case AxisConstraint.XAxis:
                    rawVal = (eventData.position.x - startPositionX) / (timeElapsed);
                    break;
                case AxisConstraint.YAxis:
                    rawVal = (eventData.position.y - startPositionY) / (timeElapsed);
                    break;
            }

            if (invert) rawVal = -rawVal;

            if (Mathf.Abs(rawVal) >= speed)
            {
                Trigger();
            }
            
            Debug.Log($"Exit at x: {eventData.position.x}, y: {eventData.position.y}");
            Debug.Log($"Swipe Time {timeElapsed}");
            Debug.Log($"Swipe speed {rawVal}");
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