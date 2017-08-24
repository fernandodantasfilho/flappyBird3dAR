using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class actionPeninha : MonoBehaviour {

    private bool destroyPeninha;
    
    // Use this for initialization
	void Start () {
        Invoke("DestroyPeninha", 2);
	}
	
	// Update is called once per frame
	void Update () {
		if (destroyPeninha){
            Destroy(this.gameObject);
        }
	}

    void DestroyPeninha(){
        destroyPeninha = true;
    }
}
