using UnityEngine;

public class Enemy : MonoBehaviour {

	//precisamos q ele vá de um checkpoit ao outro
	public int target = 0;    // Primeiro target é o checkpoit 0 porque todos os checkpoints vão estar em um ARRAY
	public Transform exitPoint;  // Ponto de saída, basicamente um location no 2d map
	public Transform[] waypoints; // variavel matriz contendo todos os nossos checkpoints(pontos por onde o inimigo percorrerá)
	public float navigationUpdate; //  comparada ao delta time, é basicamente nossa atualização de movimento
	//Basicamente é comparada ao DeltaTime do Unity, e basicamente a atualização da navegação será atualizada para o nosso vetor, a fim de avançar

	private Transform enemy; // Então essa será a localização dos nossos inimigos e vamos movelas de um ponto a outro
	private float navigationTime = 0;
	// Use this for initialization
	void Start () {
		enemy = GetComponent<Transform> (); //o Componente nos permite endereçar as propriedades das transformações para que possamos acessar outros scripts e outros componentes,
		//então quando faço isso, posso passar para a atualização(UPDATE) e a atualização é chamada uma vez por FRAME(QUADRO), então acontece muito!!!!
	}
	
	// Update is called once per frame
	void Update () {                                       
		if (waypoints != null) {                          //verifico se os waypoints é diferente de null
			navigationTime = navigationTime + Time.deltaTime; // Se for diferente adicionamos tempo ao navigationTime, para assim sabermos quanto tempo passou desde que o tempo de navegação foi criado
			if (navigationTime > navigationUpdate) {  // em seguida criei um uma instrução para comparar o tempo de navegação com o tempo de atualização 
				// NECESSÀRIO POIS SE N O INIMIGO SE MOVE(TELEPORTANDO) temos que atualizar o navigation ti
				if (target < waypoints.Length) { // Se o nosso alvo for menor que o tamanho de waypoints, então basicamente estamos apenas nos certificando que n estamos saindo do intervalo da matriz aqui, significa que podemos mover a pos do inimigo
					enemy.position = Vector2.MoveTowards (enemy.position, waypoints [target].position, navigationTime); 
					//chamamos a pos do inimigo, usamos Vector2 e MoveTowards que são ferramentas imbutidas e passamos três variaveis
					//a primeira e a posição atual, a segunda é ALVO do waypoint dentro do ARRAY, e o tempo de navegação até o alvo(O tempo que ele levará para avançar até o novo ponto)																								

				} else {
					enemy.position = Vector2.MoveTowards (enemy.position, exitPoint.position, navigationTime); // QUando estiver fora saberei que estou no último índice da matriz o q signifca a saída o EXIT POINT
																												// por parâmetro passo o "exitPoint" q no caso é um coord assim como os waypotins :)
				}
				navigationTime = 0; // e aqui estou redefinindo o temporizador, e é isso q irá mover os personagens sem que eles se teleportem de acordo com o valor
			}
		}
	}
	//Eu adicionei Colisores aos checkpoitns e também aos inimigos que passarão por ele, então vou saber quando o inimigo entra em outro objeto 
	// então basicamente preciso incrementar o meu índice da matriz de pontos de verificação(checkpoints)
	void OnTriggerEnter2D(Collider2D other){ //Para fazer isso uso uma ferramenta embutida, e passoo o Collider2D e other(poderia passar qlqr coisa), basicamente se acertamos alguma coisa isso vai ser CHAMADOe chamará a próxima 
		if (other.tag == "checkpoint")    //other aqui é o nosso CHECKPOINT 
			target = target + 1;  //Se for um ponto de Checkpoint eu quero incrementar o valor, para atingir o próximo e depois o próximo
		else if (other.tag == "Finish") { 
			GameManager.instance.removeEnemyFromScreen (); //Se a proxima tag for o Finish precisamos destruir o gameoject para q os inimigos n fiquem na tela, para isso chamamos a função removeEnemyFromScreen()
			Destroy (gameObject);
		}
	}
}
