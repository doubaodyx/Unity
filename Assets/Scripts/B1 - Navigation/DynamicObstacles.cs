using UnityEngine;
using System.Collections;

public class DynamicObstacles : MonoBehaviour {
	int count = 0;
	public int speed = 3;
	public int length = 600;
	// Update is called once per frame
	void Update () {
		if (count <= length) {
			transform.position = transform.position - new Vector3(speed * Time.deltaTime, 0, 0);
			count++;
		}
		else if (count <= 2*length){
			transform.position = transform.position + new Vector3(speed * Time.deltaTime, 0, 0);
			count++;
		}
		else count = 0;
		
	}
}
