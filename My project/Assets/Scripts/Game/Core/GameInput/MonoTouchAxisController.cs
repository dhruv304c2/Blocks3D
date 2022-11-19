using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Core.GameInput
{
    public class MonoTouchAxisController : MonoBehaviour
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

        public void Update()
        {
            if (Input.touchCount == 1)
            {
                
                Touch screenTouch = Input.GetTouch(0);

                switch (axisConstraints)
                {
                    case AxisConstraint.XAxis:
                        _myAxis.value = screenTouch.deltaPosition.x * sensitivity;
                        break;
                    case AxisConstraint.YAxis:
                        _myAxis.value = screenTouch.deltaPosition.y * sensitivity;
                        break;
                }

                if (invertAxis) _myAxis.value = -_myAxis.value;
                
                _myAxis.isDown = true;
            }
            else
            {
                _myAxis.isDown = false;
            }
        }
    }

    public enum AxisConstraint
    {
        None,
        XAxis,
        YAxis
    }
}