using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OurCameraControllerB2 : MonoBehaviour
{
	

	Vector2 mouseMovement;
    Vector2 mouseSmoothMovement;
    Vector2 clampInDegrees = new Vector2(360, 180);
    public Vector2 targetDirection;
    public Vector2 targetCharacterDirection;
    int speed = 100;
    public GameObject characterBody;

    public void MoveCamera(float horizontalArrow, float verticalArrow, float horizontalMouse, float verticalMouse)
    {
        Vector3 moveArrow = new Vector3(horizontalArrow, 0.0f, verticalArrow);
        Vector3 moveMouse = new Vector3(-verticalMouse, horizontalMouse, 0.0f);
        transform.position += moveArrow * Time.deltaTime * 35.0f;
        transform.Rotate(moveMouse);
		targetDirection = transform.localRotation.eulerAngles;
        if (characterBody) targetCharacterDirection = characterBody.transform.localRotation.eulerAngles;
    }


    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
		        if (Input.GetKey(KeyCode.W))
        {
			Vector3 newPos = new Vector3(0, 0, this.transform.forward.z * speed * Time.deltaTime);
			transform.position += newPos;
        }
        if (Input.GetKey(KeyCode.A))
        {
			transform.position -= this.transform.right* speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
			Vector3 newPos = new Vector3(0, 0, this.transform.forward.z * speed * Time.deltaTime);
			transform.position -= newPos;
        }
        if (Input.GetKey(KeyCode.D))
        {
			transform.position += this.transform.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            transform.position += new Vector3(0, 1, 0);
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            float h = 2 * Input.GetAxis("Mouse X");
            float v = 2 * Input.GetAxis("Mouse Y");
            transform.Rotate(-v, h, 0);
        }
		if(Input.GetMouseButton(1)){

			var targetOrientation = Quaternion.Euler(targetDirection);
			var targetCharacterOrientation = Quaternion.Euler(targetCharacterDirection);
			var mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));	
			mouseDelta = Vector2.Scale(mouseDelta, new Vector2(6, 6));
			mouseSmoothMovement.x = Mathf.Lerp(mouseSmoothMovement.x, mouseDelta.x, 1f / 3);
			mouseSmoothMovement.y = Mathf.Lerp(mouseSmoothMovement.y, mouseDelta.y, 1f / 3);
			mouseMovement += mouseSmoothMovement;

			var xRotation = Quaternion.AngleAxis(-mouseMovement.y, targetOrientation * Vector3.right);
			transform.localRotation = xRotation;
			transform.localRotation *= targetOrientation;

			if (characterBody)
			{
				var yRotation = Quaternion.AngleAxis(mouseMovement.x, characterBody.transform.up);
				characterBody.transform.localRotation = yRotation;
				characterBody.transform.localRotation *= targetCharacterOrientation;
			}
			else
			{
				var yRotation = Quaternion.AngleAxis(mouseMovement.x, transform.InverseTransformDirection(Vector3.up));
				transform.localRotation *= yRotation;
			}
		}
    }

}
