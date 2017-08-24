using UnityEngine;
using UnityEngine.SceneManagement;

public class actionCenaPrincipal : MonoBehaviour {

    private bool comecou;
    private bool acabou;
    private bool podeReiniciar;
    private bool cartaoDetectado;

    private float scrollSpeed;
    private float velocidadeObjeto = -75f;
    private float posicaoZInicialObjetos = 6.0f;
    private GameObject objetoX;
    private float velocidade;
    private int score;

    public Material materialPiso;
    public GameObject nodeRootCena;
    public GameObject cerca;
    public GameObject canos;
    public GameObject arbusto;
    public GameObject nuvem;
    public GameObject pedras;
    public GameObject passarinho;
    public GameObject peninhas;

    public GUIText textoMensagem;
    public GUIText textoScore;

    public AudioSource somMusica;
    public AudioClip somFinal;
    public AudioClip somHit;
    public AudioClip somVoa;
    public AudioClip somScore;
    public AudioClip somPick;

    void OnGUI() {
        if (GUI.Button(new Rect(10, 10, 30, 30), "X")) {
            SceneManager.LoadScene("cenaMenu");
        }
    }

    // Use this for initialization
    void Start () {

        if (Application.loadedLevelName == "cenaGame") {
            cartaoDetectado = true;
        } else {
            cartaoDetectado = false;
        }

        Time.timeScale = 1f;
        scrollSpeed = 0.0f;

        InvokeRepeating("CriaCerca", 1, 1.3f);
        InvokeRepeating("CriaCanos", 1, 5.0f);
        InvokeRepeating("CriaArbustoNuvemPedra", 1, 2.5f);

        somMusica.loop = true;
        somMusica.volume = 1.5f;
        somMusica.Play();
    }
	
	// Update is called once per frame
	void Update () {
        float offset = Time.time * scrollSpeed;
        materialPiso.SetTextureOffset("_MainTex", new Vector2(offset, 0));

        if (Input.anyKeyDown) {
            if (!acabou) {
                if (comecou) {
                    VoaBird();
                }
                else {
                    passarinho.transform.position = new Vector3(0.0f, 2.5f, -2.0f);
                    passarinho.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                    passarinho.GetComponent<Rigidbody>().useGravity = true;
                    comecou = true;
                    scrollSpeed = -0.5f;
                    VoaBird();
                    textoMensagem.text = "";
                    textoScore.text = score.ToString();
                }
            } else {
                if (podeReiniciar) {
                    ApagaTodosObjetos();
                }
            }
        }
    }

    void CriaCerca(){
        if (comecou && cartaoDetectado) {
            GameObject novoObjeto = (GameObject)Instantiate(cerca);
            novoObjeto.transform.parent = nodeRootCena.transform;
            novoObjeto.transform.position = new Vector3(-2f, 0, posicaoZInicialObjetos);
            novoObjeto.transform.rotation = Quaternion.Euler(-90, 0, 0);
            novoObjeto.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, velocidadeObjeto), ForceMode.Force);
        }
   }

    void CriaCanos() {
        if (comecou && cartaoDetectado) {
            var offSetCano = Random.Range(-2f, 0.0f);
            GameObject novoObjeto = (GameObject)Instantiate(canos);
            novoObjeto.transform.parent = nodeRootCena.transform;
            novoObjeto.transform.position = new Vector3(0, offSetCano, posicaoZInicialObjetos);
            novoObjeto.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, velocidadeObjeto), ForceMode.Force);
        }
    }

    void CriaArbustoNuvemPedra() {
        if (comecou && cartaoDetectado) {
            var sorteiaObjeto = Random.Range(1, 4);
            var offSetNuvem = Random.Range(3.0f, 5.0f);
            var giroRandom = Random.Range(-180.0f, 180.0f);
            var giroNuvem = 0.0f;
            var posicaoX = 0.0f;

            if ((Random.Range(1, 3) % 2) == 0) {
                posicaoX = -1.0f;
                giroNuvem = 0.0f;
            } else {
                posicaoX = 1.5f;
                giroNuvem = 180.0f;
            }

            switch (sorteiaObjeto) {
                case 1:
                    objetoX = (GameObject)Instantiate(arbusto);
                    objetoX.transform.parent = nodeRootCena.transform;
                    objetoX.transform.position = new Vector3(posicaoX, 0, posicaoZInicialObjetos);
                    objetoX.transform.rotation = Quaternion.Euler(-90, giroRandom, 0);
                    break;

                case 2:
                    objetoX = (GameObject)Instantiate(nuvem);
                    objetoX.transform.parent = nodeRootCena.transform;
                    objetoX.transform.position = new Vector3(posicaoX, offSetNuvem, posicaoZInicialObjetos);
                    objetoX.transform.rotation = Quaternion.Euler(-90, giroNuvem, 0);
                    break;

                case 3:
                    objetoX = (GameObject)Instantiate(pedras);
                    objetoX.transform.parent = nodeRootCena.transform;
                    objetoX.transform.position = new Vector3(posicaoX, 0, posicaoZInicialObjetos);
                    objetoX.transform.rotation = Quaternion.Euler(-90, giroRandom, 0);
                    break;
                    default: break;
            }

        objetoX.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, velocidadeObjeto), ForceMode.Force);
        }
    }


    void VoaBird() {
        passarinho.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        passarinho.GetComponent<Rigidbody>().AddForce(new Vector3(0, 200, 0), ForceMode.Force);
        passarinho.GetComponent<AudioSource>().PlayOneShot(somVoa, 1.0f);
        CriaPeninhas();   
    }

    void LateUpdate(){
        velocidade = passarinho.GetComponent<Rigidbody>().velocity.y;
        passarinho.transform.rotation = Quaternion.Euler(velocidade * -5, 0, 0);    
    }

    public void FimDeJogo(){
        if (!acabou) {
            acabou = true;
            print("Game Over");
            comecou = false;
            scrollSpeed = 0.0f;
           ParaObjetos();
           Invoke("SetEstadoReload", 2);
           passarinho.GetComponent<AudioSource>().PlayOneShot(somHit, 0.3f);
        }
    }

    void ParaObjetos() {
        var objects = GameObject.FindGameObjectsWithTag("Objetos");     //Atribuindo a variável objects todos os objetos da tag "Objetos" 
        foreach (var obj in objects) {              //cria a variável 
            if (obj != null) {              //Necessário, caso haja algum objeto se, rigidbody
                obj.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);          //Setando velocidade 0 para os objetos
                //Destroy(obj);          //Pode-se destruir os objetos
            }
        }
    }

    void SetEstadoReload() {
        podeReiniciar = true;
        textoMensagem.text ="Toque para Reiniciar!!!";
        passarinho.GetComponent<AudioSource>().PlayOneShot(somFinal, 0.5f);
    }

    void ApagaTodosObjetos() {
        var gameObjects = GameObject.FindGameObjectsWithTag("Objetos");   //Atribuindo a variável objects todos os objetos da tag "Objetos" 
        for (var i = 0; i < gameObjects.Length; i++) {   //laço que vai de 0 até o número de objetos encontrados na tag "Objetos"
            Destroy(gameObjects[i]);    //Destroy o objetos da posição do laço
        }
        passarinho.transform.position = new Vector3(0.0f, 2.5f, -2.0f);
        passarinho.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        passarinho.GetComponent<Rigidbody>().useGravity = true;
        comecou = true;
        acabou = false;
        podeReiniciar = false;
        scrollSpeed = -0.5f;
        VoaBird();
        textoMensagem.text = "";
        score = 0;
        textoScore.text = score.ToString();
    }

    public void UpdateScore() {
        score++;
            textoScore.text = score.ToString();
            passarinho.GetComponent<AudioSource>().PlayOneShot(somScore, 0.8f);
    }

    public void CriaPeninhas() {
        for (int i=0; i < 5; i++) {         //Este laço é executado 5 vezes e faz com que apereçam 5 peninhas
            var dirx = Random.Range(-50,50);      //aqui é criado 6 valores randômicos de posição e rotação das peninhas
            var diry = Random.Range(-50, 50);
            var dirz = Random.Range(-50, 50);
            var rotx = Random.Range(-180,180);      
            var roty = Random.Range(-180, 180);
            var rotz = Random.Range(-180, 180);

            var randomScala = Random.Range(0.35f, 0.55f);      //aqui é criado uma tamanho randômico para as peninhas
            GameObject novaPeninha = (GameObject)Instantiate(peninhas, passarinho.transform.position, Quaternion.Euler(0,0,0)); //Aqui é criado o objeto peninha na Posição do passarinho que foi passado como parâmetro
            novaPeninha.transform.localScale = new Vector3(randomScala, randomScala, randomScala);   //Aqui é definido o tamanho da peninha no eixo x, y e z com base no tamanho criado anteriormente de forma randomica
            novaPeninha.GetComponent<Rigidbody>().AddForce(new Vector3(dirx, diry, dirz), ForceMode.Force);  //Aqui a peninha recebe a força para empurrar as penas pra algum lado
            novaPeninha.GetComponent<Rigidbody>().AddTorque(new Vector3(rotx, roty, rotz));     //Adicionando o torque, para a peninha rotacionar em algum eixo
        }
    }

    public void CartaoAtivado() {
        cartaoDetectado = true;
    }

    public void CartaoDesativado() {
        cartaoDetectado = false;
    }
}
