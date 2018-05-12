using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

	// loader criado para conectar o script a camera principal, pq ela sempre da load
	//Quando rodamos a main camera será chamada e instanciaremos o jogo uma vez

	public GameObject gameManager; // Tem que ser gameObject, pq quando criamos o GameManager nós criamos um gameobject vázio

	void Awake () {								//Vai ser chamado quando carregar o jogo
		if (GameManager.instance == null)				//checa se o gameManager é null
			Instantiate (gameManager);				//instancia o gameManager
	}
}
