using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
public class PlayerHealth : MonoBehaviour {

	[SerializeField] int startingHealth = 100;
	[SerializeField] float timeSinceLastHit = 2f;//tiempo entre ataques

	private float timer = 0f;//timer para el control entre golpes
	private CharacterController characterController;
	private Animator anim;
	private int currentHealth;//vida del heroe actual
    private new AudioSource audio;

    // Use this for initialization
    void Start () {
		
		anim = GetComponent<Animator> ();
		characterController = GetComponent <CharacterController>();
		currentHealth = startingHealth;
		audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
		timer += Time.deltaTime;
	}

	void OnTriggerEnter (Collider other)
	{
		if (timer >= timeSinceLastHit && !GameManager.instance.GameOver)//timer >= timeSinceLastHit evalua cuanto tiempo paso desde el ultimo ataque
		{
			if(other.tag =="Weapon")
			{
				takeHit ();
				timer = 0;
			}

		}


	}

	void takeHit()
	{
		if(currentHealth > 0)
		{
			GameManager.instance.PlayerHit(currentHealth);
			anim.Play("Hurt");
			currentHealth -=10;
			audio.PlayOneShot(audio.clip);
			

		}
		if(currentHealth <= 0)
		{
			killPlayer();
		}

	}

	void killPlayer()
	{
		GameManager.instance.PlayerHit (currentHealth);
		anim.SetTrigger("HeroDie");
		characterController.enabled=false;//una vez "muerto" el heroe se activa esta funcion y se coloca en estado falso para que no se pueda controlar más el personaje heroe
		audio.PlayOneShot(audio.clip);
	}

}
