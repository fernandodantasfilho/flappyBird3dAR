using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class actionPassarinho : MonoBehaviour {

    public GameObject objetoCamera;
    private actionCenaPrincipal scriptB;
    
    // Use this for initialization
	void Start () {
		scriptB = (actionCenaPrincipal) objetoCamera.GetComponent(typeof(actionCenaPrincipal));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider outro) {  //função que verifica se o passarinho entro em algum colisor
        if (outro.gameObject.tag == "Cano") { //Aqui verifica se o objeto que o passarinho colidiu é da tag CANO 
            scriptB.FimDeJogo();
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 200, -200  ), ForceMode.Force);
        }
    }

    void OnTriggerExit (Collider outro) {    //função que verifica se o passarinho entro em algum colisor
        if (outro.gameObject.tag == "Vão") { //Aqui verifica se o objeto que o passarinho colidiu é da tag Vão 
            scriptB.UpdateScore();
        }
    }

}
