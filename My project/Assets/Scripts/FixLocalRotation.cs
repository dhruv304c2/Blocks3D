using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixLocalRotation : MonoBehaviour
{
    void Update()
    {
        transform.localEulerAngles = Vector3.zero;
    }
}
