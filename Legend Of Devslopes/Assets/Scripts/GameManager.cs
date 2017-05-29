using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	[SerializeField] GameObject player;//una instancia del heroe
	private bool gameOver = false;//monitorear a travez del game manager el status del juego

	
	public bool GameOver //instancia privada
	{
		get {return gameOver; }

	}

	public GameObject Player
	{
		get {return player; }

	}

	void Awake() {

		if(instance == null)
		{
			instance = this;

		}
		else if(instance !=this)
		{
			Destroy (gameObject);
		}

		DontDestroyOnLoad (gameObject);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayerHit(int currentHP)
	{
		if(currentHP > 0)
		{
			gameOver = false;
		}
		else
		{
			gameOver = true;
		}
	}
}
