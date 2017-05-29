using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour {

	[SerializeField] Transform player;
	private NavMeshAgent nav;
	private Animator anim;
	private EnemyHealth enemyHealth;//referencia al script EnemyHealth
	
	void Awake() {
		Assert.IsNotNull (player);
	}

	// Use this for initialization
	void Start () {
		enemyHealth = GetComponent<EnemyHealth>();
		anim = GetComponent<Animator> ();
		nav = GetComponent<NavMeshAgent> ();

	}
	
	// Update is called once per frame
	void Update () {

		if(!GameManager.instance.GameOver && enemyHealth.IsAlive)//si el juego no ha acabado y seguimos vivos
		{
		nav.SetDestination(player.position);//esto hace que el enemigo vaya hacia la posición del jugador
		
		}
		else if((!GameManager.instance.GameOver || GameManager.instance.GameOver) && !enemyHealth.IsAlive) //si el juego ha terminado (Heroe muerto), el enemigo hara la animación Idle
		{
			nav.enabled = false;
			
		}
		else
		{
			nav.enabled = false;
			anim.Play("Idle");
		}
		
		
	}
}
