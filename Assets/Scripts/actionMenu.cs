using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class actionMenu : MonoBehaviour {

    public Texture2D texturaFundo;
    public Texture2D texturaLogo;
    public Texture2D texturaFelpudo;

    private float larguraLogo = 100;
    private float alturaLogo = 60;
    private float larguraFelpudo = 140;
    private float alturaFelpudo = 125;
    private float larguraBotao = 160;
    private float alturaBotao = 40;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    void OnGUI() {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texturaFundo, ScaleMode.StretchToFill);    //BG
        GUI.DrawTexture(new Rect(Screen.width - larguraLogo - 10, 10, larguraLogo, alturaLogo), texturaLogo, ScaleMode.StretchToFill);  //Logo
        GUI.DrawTexture(new Rect(10, Screen.height / 2 - alturaFelpudo / 2, larguraFelpudo, alturaFelpudo), texturaFelpudo, ScaleMode.StretchToFill);  //Felpudo
        if (GUI.Button(new Rect(Screen.width / 2 - larguraBotao / 2, Screen.height / 2 + alturaBotao - 30, larguraBotao, alturaBotao), "Jogar COM Câmera")) {
            SceneManager.LoadScene("cenaGameAR");
        }
            
        if (GUI.Button(new Rect(Screen.width / 2 - larguraBotao / 2, Screen.height / 2 + alturaBotao + 30, larguraBotao, alturaBotao), "Jogar SEM Câmera")) {
            SceneManager.LoadScene("cenaGame");
        }
           
    }
}