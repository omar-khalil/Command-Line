using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowRotate : MonoBehaviour {

    public float speed;
	
	// Update is called once per frame
	void Update () {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + (speed * Time.deltaTime), transform.localEulerAngles.z);
	}
}
