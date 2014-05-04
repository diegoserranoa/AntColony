using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AntScript : MonoBehaviour {

	int maxX = 100;
	int maxY = 100;
	float positionX = 1;
	float positionY;
	float larvaX = 0;
	float larvaY = 0;
	int foodX;
	int foodY;
	int lastX;
	int lastY;
	int index = 0;
	bool foundSomething = false;
	bool hasFood = false;
	bool foundHormone = false;
	GameObject food;
	List<GameObject> hormones;
	public Transform prefab;
	public Transform hormonePrefab;


	// Use this for initialization
	void Start () {
		hormones = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (hasFood) {
			if (((lastX - transform.position.x) > 10) || ((lastY - transform.position.y) > 10)){
				lastX = (int) transform.position.x;
				lastY = (int) transform.position.y;
				Transform hormone = Instantiate(hormonePrefab) as Transform;
				hormone.transform.position = new Vector2(transform.position.x, transform.position.y);
				Destroy(hormone.gameObject,3);
			}

			if (transform.position.x < larvaX){
				transform.position = new Vector2(transform.position.x + 1f, transform.position.y);
			} else if (transform.position.x > larvaX){
				transform.position = new Vector2(transform.position.x - 1f, transform.position.y);
			} else if (transform.position.y < larvaY){
				transform.position = new Vector2(transform.position.x, transform.position.y + 1f);
			} else if (transform.position.y > larvaY){
				transform.position = new Vector2(transform.position.x, transform.position.y - 1f);
			} else if (transform.position.x == larvaX && transform.position.y == larvaY){
				Transform newFood = Instantiate(prefab) as Transform;
				newFood.transform.position = new Vector3(larvaX, larvaY);
				hasFood = false;
			}
		} else if (foundHormone){
			GameObject hormone = (GameObject) hormones[index];
			int hormoneX = (int) hormone.transform.position.x;
			int hormoneY = (int) hormone.transform.position.y;
			Debug.Log ("hormoneX: " + hormoneX);
			if (transform.position.x < hormoneX){
				transform.position = new Vector2(transform.position.x + 1f, transform.position.y);
			} else if (transform.position.x > hormoneX){
				transform.position = new Vector2(transform.position.x - 1f, transform.position.y);
			} else if (transform.position.y < hormoneY){
				transform.position = new Vector2(transform.position.x, transform.position.y + 1f);
			} else if (transform.position.y > hormoneY){
				transform.position = new Vector2(transform.position.x, transform.position.y - 1f);
			} else if (transform.position.x == hormoneX && transform.position.y == hormoneY){
				index++;
			}
		} else if (foundSomething) {
			if (transform.position.x < foodX){
				transform.position = new Vector2(transform.position.x + 1f, transform.position.y);
			} else if (transform.position.x > foodX){
				transform.position = new Vector2(transform.position.x - 1f, transform.position.y);
			} else if (transform.position.y < foodY){
				transform.position = new Vector2(transform.position.x, transform.position.y + 1f);
			} else if (transform.position.y > foodY){
				transform.position = new Vector2(transform.position.x, transform.position.y - 1f);
			} else if (transform.position.x == foodX && transform.position.y == foodY && food != null){
				Destroy(food.gameObject);
				food = null;
				foundSomething = false;
				hasFood = true;
				lastX = (int) transform.position.x;
				lastY = (int) transform.position.y;

			}


		} else {
			if (transform.position.x >= maxX && transform.position.y < maxY){
				transform.position = new Vector2(transform.position.x, transform.position.y + 1);
				positionX = -1;
			} else if (transform.position.x <= 0) {
				transform.position = new Vector2(transform.position.x, transform.position.y + 1);
				positionX = 1;
			}

			transform.position = new Vector2(transform.position.x + positionX, transform.position.y);
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		Debug.Log (coll);

	}

	void OnTriggerEnter2D(Collider2D collision) {

		if (collision.gameObject.tag.Equals ("Hormone") && !foundSomething) {
			Debug.Log("Found hormone");
			bool exists = false;
			foundHormone = true;

			foreach (GameObject gameObject in hormones){
				if (gameObject == collision.gameObject) {
					exists = true;
				}
			}
			if (!exists){
				hormones.Add (collision.gameObject);
			}
		} else if (collision.gameObject.tag.Equals ("Food")) {
			Debug.Log("Found food");
			if (food == null && !hasFood) {
				foodX = (int)collision.gameObject.transform.position.x;
				foodY = (int)collision.gameObject.transform.position.y;

				foundHormone = false;
				index = 0;
				hormones = new List<GameObject> ();
				foundSomething = true;
				food = collision.transform.gameObject;
			}
		}

	}

}
