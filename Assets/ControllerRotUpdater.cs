using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ControllerRotUpdater : MonoBehaviour
{

    [SerializeField] private Transform controller;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InputDevice handR = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        handR.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceRotation, out Quaternion rotR);
        controller.rotation = rotR;
    }
}
