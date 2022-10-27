using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixLocalRotation : MonoBehaviour
{
    void LateUpdate()
    {
        transform.localEulerAngles = Vector3.zero;
    }
}
