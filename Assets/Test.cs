using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using WebSocketServer;

public class Test : MonoBehaviour
{
    public string lastValue;

    [SerializeField] float pitchOffset;
    [SerializeField] float rollOffset;

    private Rigidbody rigidBody;

    private float X;
    private float Z;
    private float Y;


    private float xAcc;
    private float yAcc;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateValues(WebSocketMessage message)
    {
        lastValue = message.data;
        EyeOfRaInfo info = JsonConvert.DeserializeObject<EyeOfRaInfo>(message.data);
        
        transform.eulerAngles = new Vector3(info.Gyroscope.Z, info.Gyroscope.Y, info.Gyroscope.X);
    }
}
