using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movCaixa : MonoBehaviour {

	public float velocidade = 4f;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		   gameObject.transform.position -= new Vector3(0f, 0f, velocidade * Time.deltaTime);
	}
}
