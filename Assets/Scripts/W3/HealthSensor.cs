using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSensor : Sensor {
    
	void Start () {
        sensorType = SensorType.health;
        manager.RegisterSensor(this);
	}
}
