using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class movePersonagem : MonoBehaviour {

  public GameObject jogador;
  public Animation animacao;
  public GameObject particulaOvo;
  public GameObject particulaPena;
  public GameObject particulaEstrela;
  public GameObject objetoParticulaFogo;

  CharacterController objetoCharControler;
  Vector3 vetorDirecao = new Vector3(0, 0, 0);
  float giro = 3.0f;
  float velocidade = 5.0f;
  private float pulo = 6.0f;
  float frente = 5.0f;
  Vector3 moveCameraFrente;
  Vector3 moveMove;
  Vector3 normalZeroPiso = new Vector3(0,0,0);
  Transform transformCamera;

	// Use this for initialization
	void Start () {
        objetoCharControler = GetComponent<CharacterController>();
        animacao = jogador.GetComponent<Animation>();
        transformCamera = Camera.main.transform;

	}

	// Update is called once per frame
	void Update () {

    // modifca o CrossPlatformInputManager para ficar relativo a camera
    moveCameraFrente = Vector3.Scale(transformCamera.forward, new Vector3(1,0,1)).normalized;
    moveMove = CrossPlatformInputManager.GetAxis("Vertical") * moveCameraFrente + CrossPlatformInputManager.GetAxis("Horizontal") * transformCamera.right;
    vetorDirecao.y -= 3.0f * Time.deltaTime;
    objetoCharControler.Move(vetorDirecao * Time.deltaTime);
    objetoCharControler.Move(moveMove * velocidade * Time.deltaTime);

    if(moveMove.magnitude > 1f) moveMove.Normalize();

    moveMove = transform.InverseTransformDirection(moveMove);
    moveMove = Vector3.ProjectOnPlane(moveMove,normalZeroPiso);
    giro = Mathf.Atan2(moveMove.x,moveMove.z);
    frente = moveMove.z;

    objetoCharControler.SimpleMove(Physics.gravity);
    aplicaRotacao();

    //animacao
    if (CrossPlatformInputManager.GetButton("Jump")){
      if (objetoCharControler.isGrounded == true) {
        vetorDirecao.y = pulo;
        jogador.GetComponent<Animation>().Play("JUMP");
        }
    } else {
      jogador.GetComponent<Animation>().Play("WALK");
      if(  (CrossPlatformInputManager.GetAxis("Horizontal"))  != 0.0f|| (CrossPlatformInputManager.GetAxis("Vertical"))  != 0.0f ){
        
          jogador.GetComponent<Animation>().Play("WALK");
        
      }else{
        if(objetoCharControler.isGrounded == true){
          jogador.GetComponent<Animation>().Play("IDLE");
        }
      }
    }

    //movimentacao antiga
    /*
    giro = 300.0f;
    Vector3 forward = CrossPlatformInputManager.GetAxis("Vertical") * transform.TransformDirection(Vector3.forward) * velocidade;
    transform.Rotate(new Vector3(0,CrossPlatformInputManager.GetAxis("Horizontal")*giro*Time.deltaTime,0));
    objetoCharControler.Move(forward * Time.deltaTime);
    objetoCharControler.SimpleMove(Physics.gravity);
    
    vetorDirecao.y -= 3.0f * Time.deltaTime;
    objetoCharControler.Move(vetorDirecao * Time.deltaTime);
    */

  
    
    

	}


void OnTriggerEnter(Collider other){

     if (other.gameObject.tag == "OVO")
        {
            //Instantiate(particulaOvo, other.gameObject.transform.position, Quaternion.identity);
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "PENA")
        {
            //Instantiate(particulaPena, other.gameObject.transform.position, Quaternion.identity);
            //Destroy(other.gameObject); 
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "ESTRELA")
        {
            //Instantiate(particulaEstrela, other.gameObject.transform.position, Quaternion.identity);
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "FOGUEIRA")
        {
        }
        if (other.gameObject.tag == "BURACO")
        {
        }

    }





  void aplicaRotacao(){
    float turnSpeed = Mathf.Lerp(180,360,frente);
    transform.Rotate(0,giro*turnSpeed*Time.deltaTime,0);
  }
}
