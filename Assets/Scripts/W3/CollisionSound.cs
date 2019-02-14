using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSound : MonoBehaviour {
    public AudioClip collisionSound;
	void Start () {
        gameObject.GetComponent<AudioSource>().PlayOneShot(collisionSound);
	}
}
