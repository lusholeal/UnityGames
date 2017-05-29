using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


	[SerializeField] private float moveSpeed = 10.0f;
	[SerializeField] private LayerMask layerMask;
	private CharacterController characterController;
	private Vector3 currentLookTarget = Vector3.zero;
	private Animator anim;
	private BoxCollider[] swordColliders;
	// Use this for initialization
	void Start () {
		
		characterController = GetComponent<CharacterController>();
		anim = GetComponent<Animator> ();
		swordColliders = GetComponentsInChildren<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update () {

		if(!GameManager.instance.GameOver)
		{
			Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));// movimiento en los ejes X y Z. Horizontal habilita los ejes horizontales con lso botones por defecto a y d, vertcal los botones verticales w y s
		characterController.SimpleMove(moveDirection * moveSpeed);//movimiento

			if(moveDirection == Vector3.zero)
			{
				anim.SetBool("IsWalking", false);//si no se mueve el personaje, entonces se selecciona el animador y el parametro IsWalking es puesto en estado falso
			}
			else
			{
				anim.SetBool("IsWalking", true);
			}

			if(Input.GetMouseButtonDown(0))
			{
				anim.Play("DoubleChop");
			}

			if(Input.GetMouseButtonDown(1))
			{
				anim.Play("SpinAttack");
			}

		}


	}

	void FixedUpdate() {

		if(!GameManager.instance.GameOver)
		{
			RaycastHit hit;//creado punto de choque
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//se crea un rayo desde la camara hasta el punto del mouse

		Debug.DrawRay(ray.origin, ray.direction * 500, Color.blue);//se dibuja linea desde la camara hasta el punto donde el mouse se encuentra apuntando

		if(Physics.Raycast(ray, out hit, 500, layerMask, QueryTriggerInteraction.Ignore))
		{
			if(hit.point != currentLookTarget)
			{
				currentLookTarget = hit.point;// si actualmente no se esta mirando donde el mouse apunta hacer ese punto lo que queremos ver


			}
			Vector3 targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
			Quaternion rotation = Quaternion.LookRotation(targetPosition - transform.position);//quaternion aplicado para la rotación de la camara
			transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 10f);

		}
		}

	}

	public void BeginAttack()
	{
		foreach(var weapon in swordColliders)
		{
			weapon.enabled = true;

		}

	}

	public void EndAttack()
	{
		foreach(var weapon in swordColliders)
		{
			weapon.enabled = false;

		}


	}

}
