using System.Collections;
using System.Collections.Generic;
using UnityEngine.SocialPlatforms;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

	[SerializeField] private float range = 3f;
	[SerializeField] private float timeBetweenAttacks = 1f;//el tiempo entre cada ataque del enemigo

	private Animator anim;//animador
	private GameObject player;//referencia al jugador
	private bool playerInRange;//tiene en cuenta si el heroe esta dentro del rango de aaque o no
	private BoxCollider[] weaponColliders;//arreglo para contar las armas de los enemigos
	private EnemyHealth enemyHealth;

	// Use this for initialization
	void Start () {
		
		enemyHealth = GetComponent<EnemyHealth>();
		weaponColliders = GetComponentsInChildren<BoxCollider>();//obtener los componentes desde los hijos (por ejemplo dentro del riggin de los pjs)
		player = GameManager.instance.Player;//para usar el get del jugador dentro del GameManager
		anim = GetComponent<Animator> ();
		StartCoroutine(attack());//se llama a la coroutine desde el inicio
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Vector3.Distance(transform.position, player.transform.position) < range && enemyHealth.IsAlive)
		{
			playerInRange = true;

		}
		else
		{
			playerInRange = false;
		}
		
	}

	IEnumerator attack()//coroutine es una función que permite trabajar en bloques de codigo
	{
		if(playerInRange && !GameManager.instance.GameOver)//si el jugador esta en rango y el juego aun esta activo
		{
			anim.Play("Attack");//animación de ataque
			yield return new WaitForSeconds(timeBetweenAttacks);//esperar segundos con la variable timeBetweenAttacks
		}
		yield return null;
		StartCoroutine(attack());
	}

	public void EnemyBeginAttack()//disparador de evento en la animación para activar el colisionador del arma en cierto frame de la animación
	{
		foreach( var weapon in weaponColliders)
		{
			weapon.enabled = true;

		}

	}

	public void EnemyEndAttack()//disparador de evento en la animación para desactivar el colisionador del arma en cierto frame de la animación
	{
		foreach( var weapon in weaponColliders)
		{
			weapon.enabled = false;

		}
		
	}

}
