using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class actionObjetos : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.z < -6.0){
			Destroy(this.gameObject);
		} 
	}
}
