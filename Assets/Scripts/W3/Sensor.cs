using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour {

    protected TriggerSystemManager manager;
    public enum SensorType
    {
        sight,
        sound,
        health
    }
    public SensorType sensorType;

    public virtual void Notify(Trigger t)
    {

    }

    private void Awake()
    {
        manager = FindObjectOfType<TriggerSystemManager>();
    }
}
