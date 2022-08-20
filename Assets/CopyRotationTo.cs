using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyRotationTo : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform fromTransform;
    [SerializeField] private Transform targetTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        targetTransform.rotation = Quaternion.Euler(fromTransform.eulerAngles + offset);
    }
}
