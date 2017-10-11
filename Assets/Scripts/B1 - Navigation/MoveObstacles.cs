using UnityEngine;
using System.Collections;

public class MoveObstacles : MonoBehaviour {

	private Renderer moveableObjRender;
	private bool isSelected;
	private Rigidbody rb;
	// Update is called once per frame
	void Awake () {
		moveableObjRender = GetComponent<Renderer> ();
		rb = GetComponent<Rigidbody> ();
	}

	void Update () {
		if (isSelected) {
			float horizontalArrow = Input.GetAxis ("Horizontal");
			float verticalArrow = Input.GetAxis ("Vertical");
			Vector3 inputMovement = new Vector3 (horizontalArrow, 0.0f, verticalArrow);
			transform.position += inputMovement * Time.deltaTime * 25.0f;
		}
	}
	
	void OnMouseDown () {
		if (!isSelected)
			isSelected = true;
		else
			isSelected = false;
	}
}