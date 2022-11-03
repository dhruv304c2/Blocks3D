using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Core.GameInput
{
    public class MonoTouchAxisController : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        [SerializeField] private TouchAxis _myAxis;
        public AxisConstraint axisConstraints;
        [SerializeField] private string axisName;
        [SerializeField] private float deadZone = 0.1f;
        [SerializeField] private float sensitivity = 1f;
        [SerializeField] private bool invertAxis;

        private void Awake()
        {
            GameInputManager.RegisterTouchAxis(axisName, _myAxis);
        }

        public void OnDrag(PointerEventData eventData)
        {
            float rawVal = 0;
            switch (axisConstraints)
            {
                case AxisConstraint.XAxis:
                    rawVal = eventData.delta.x * sensitivity;
                    break;
                case AxisConstraint.YAxis:
                    rawVal = eventData.delta.y * sensitivity;
                    break;
            }

            _myAxis.value = Mathf.Clamp(rawVal, -1, 1);

            if (Math.Abs(_myAxis.value) <= deadZone && eventData.IsPointerMoving())
            {
                _myAxis.value = 0;
                _myAxis.isDown = false;
            }
            else
            {
                _myAxis.isDown = true;
            }

            if (invertAxis == true) _myAxis.value = -_myAxis.value;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _myAxis.isDown = false;
        }
    }

    public enum AxisConstraint
    {
        None,
        XAxis,
        YAxis
    }
}