using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMove : MonoBehaviour {
	void Update () {
        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.Translate(Vector3.up * 4, Space.Self);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            transform.Translate(Vector3.down * 4, Space.Self);
        }

	}
}
