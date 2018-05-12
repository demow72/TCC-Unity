using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	public GameObject spawnPoint; // lugar onde os inimigos irão aparecer
	public GameObject[] enemies; //array pra 3 tipos de inimigos 
	public int maxEnemiesOnScreen; // nr de inimigos na tela, váriavel criada pra manipular a dificuldade
	public int totalEnemies;
	public int enemiesPerSpawn; // Quantos enemigos vão ser gerados por spawn

	private int enemiesOnScreen = 0; //Então toda a vez q spawnarmos inimigos incrementaremos com essa váriavel
	// Use this for initialization
	void Awake(){
		if (instance == null)   // Se a instance for igual a null nos queremos setar a intancia a ela mesma
			instance = this;
		else if (instance != this) // Se n for igual a ela mesma queremos destruir o gameObjetct, 
			Destroy(gameObject);
		
		DontDestroyOnLoad (gameObject);
	}

	void Start () {
		spawnEnemy ();
	}

	void spawnEnemy(){
		if ( enemiesPerSpawn > 0 && enemiesOnScreen < totalEnemies) { // Se enimigosporSpawn for menor que 0 E os inimigos na tela forem menores que o nr total de enimigos
			for (int i=0; i <enemiesPerSpawn; i++){                   // i = 0 enquanto i for menor que inimigos por spawn SEGUE O BAILE
				if (enemiesOnScreen < maxEnemiesOnScreen) {           // Se os inimigos na tela forem MENOR que o nr MAX de inimigos na tela, instancia o gameObjetct e transoforma a posição dele mostrando os inimigo
					GameObject newEnemy = Instantiate(enemies[0]) as GameObject;
						newEnemy.transform.position = spawnPoint.transform.position;
						enemiesOnScreen = enemiesOnScreen +1 ;
				}
			}
		}
	}

	public void removeEnemyFromScreen(){
		if (enemiesOnScreen > 0)
			enemiesOnScreen = enemiesOnScreen - 1;           //Decrementamos os inimigos na tela, função chamada no gameManager pra remover os inimigos que já chegaram no FINISH ou foram abatidos
	}
}
