using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;//para asegurarse que los elementos requeridos en el juego estan puestos

public class CameraFollow : MonoBehaviour {

	[SerializeField] Transform target; //jugador
	[SerializeField] float smoothing = 5f;//relacionado con el movimiento de la camara

	Vector3 offset;//offset de el jugador y la camara desde el inicio
	
void Awake() {

	Assert.IsNotNull(target);
}

	// Use this for initialization
	void Start () {
		
		offset = transform.position - target.position;//transform.position es la posición de la camara
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector3 targetCamPos = target.position + offset;//targetCamPos es la posición en la que se quiere que se mueva la camara
		transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);//Lerp es la interpolación entre dos vectores. Vector3 Lerp(Vector3 a, Vector3 b, float t)

	}
}
