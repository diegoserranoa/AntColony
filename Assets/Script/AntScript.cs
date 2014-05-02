using UnityEngine;
using System.Collections;

public class AntScript : MonoBehaviour {

	int maxX = 100;
	int maxY = 100;
	bool foundSomething = false;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (foundSomething) {
			Debug.Log ("Found food.");
			foundSomething = false;


		} else {
			if (this.transform.position.x < maxX) {
				//transform.position = new Vector2(transform.position.x + 1, transform.position.y);
			}
			
			if (this.transform.position.y < maxY) {
				//transform.position = new Vector2(transform.position.x, transform.position.y + 1);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collision) {
		Debug.Log(collision);

		foundSomething = true;
	}
}
