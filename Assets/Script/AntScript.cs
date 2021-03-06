﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AntScript : MonoBehaviour {

	public int maxX;
	public int maxY;
	public Sprite antUp;
	public Sprite antDown;
	public Sprite antLeft;
	public Sprite antRight;
	public int randomFactor = 0;
	float positionX = 0;
	float positionY = 0;
	float larvaX = 5;
	float larvaY = 5;
	float time;
	float timeStay;
	int hormoneIndex = 1;
	public int lastHormoneIndex;
	int foodX;
	int foodY;
	int lastX;
	int lastY;
	int index = 0;

	public bool useStay = false;
	bool foundSomething = false;
	bool hasFood = false;
	bool foundHormone = false;
	GameObject food;
	List<GameObject> hormones;
	List<Vector2> colonyEntries;
	public Transform prefab;
	public Transform hormonePrefab;

	public float 	score;
	public int 		expense;
    public bool     updater;
    int             steps;


	// Use this for initialization
	void Start () {
		score = 0;
        steps = 1;
		expense = 0;
        updater = false;
		randomFactor = Random.Range(2,6);
		lastHormoneIndex = 1000;
		hormones = new List<GameObject> ();
		int rand = Random.Range(0,randomFactor);
		// Go Right
		if (rand == 1){
			positionX = 1;
			positionY = 0;
			this.gameObject.GetComponent<SpriteRenderer>().sprite = antRight;
		}
		// Go Left
		else if (rand == 2){
			positionX = -1;
			positionY = 0;
			this.gameObject.GetComponent<SpriteRenderer>().sprite = antLeft;
		}
		// Go Up
		else if (rand == 3){
			positionX = 0;
			positionY = 1;
			this.gameObject.GetComponent<SpriteRenderer>().sprite = antUp;
		}
		// Go Down
		else if (rand == 4){
			positionX = 0;
			positionY = -1;
			this.gameObject.GetComponent<SpriteRenderer>().sprite = antDown;
		}
	}

	// Update is called once per frame
	void Update () {
		// Regresar comida a reina.
        if (steps == 100)
        {
            expense = expense + 1;
            steps = 1;
        }
		if (this.transform.position.x > (maxX - 20)) {
			positionX = -1;
		} else if (this.transform.position.x < (-maxX + 20)) {
			positionX = 1;
		}
		if (this.transform.position.y > (maxY - 20)) {
			positionY = -1;
		} else if (this.transform.position.y < (-maxY + 20)) {
			positionY = 1;
		}


		if (hasFood) {
			goBackToColony();
		}
		// Encontro comida e ir hacia
		// la posicion de la comida.
		else if (foundSomething) {
			goForFood();
		}
		// Encontrar un hormona y seguirla para llegar
		// a un lugar donde haya comida.
		else if (foundHormone){
			followFeromone();
		}
		// No ha encontrado nada. Recorrer el mapa.
		else {
			explore ();
		}
        if (updater)
            sendToGUI();
        steps = steps + 1;
	}

    void sendToGUI()
    {
        GUIScript.setGameObject(this.transform.gameObject);
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
    }

	void goBackToColony(){
		// después de avanzar cierta cantidad dejar
		// hormonas en el camino
		if ((((Mathf.Abs(lastX) - Mathf.Abs(transform.position.x)) > 10) ||
		     ((Mathf.Abs(lastY) - Mathf.Abs(transform.position.y)) > 10))){
			lastX = (int) transform.position.x;
			lastY = (int) transform.position.y;
			Transform hormone = Instantiate(hormonePrefab) as Transform;
			hormone.transform.position = new Vector2(transform.position.x, transform.position.y);
			hormone.gameObject.GetComponent<HormoneScript>().index = hormoneIndex;
			hormoneIndex++;
			Destroy(hormone.gameObject,10);
		}
		// Checar si ya se encuentra en la posicion
		// de la reina para dejarle la comida.
		if (transform.position.y < larvaY){
			transform.position = new Vector2(transform.position.x, transform.position.y + 1f);
		} else if (transform.position.y > larvaY){
			transform.position = new Vector2(transform.position.x, transform.position.y - 1f);
		}
		if (transform.position.x < larvaX){
			transform.position = new Vector2(transform.position.x + 1f, transform.position.y);
		} else if (transform.position.x > larvaX){
			transform.position = new Vector2(transform.position.x - 1f, transform.position.y);
		}
		if (transform.position.x == larvaX && transform.position.y == larvaY){
			// Dejar comida. Food2
			Transform newFood = Instantiate(prefab) as Transform;
			newFood.transform.position = new Vector3(larvaX, larvaY);
			hasFood = false;
			hormoneIndex = 1;
			time = 1;
			lastHormoneIndex = 1000;
			score = score + 10;
		}
	}

	void goForFood(){
		if (food != null){
			// Revisar posicion de comida.
			if (transform.position.x < foodX){
				transform.position = new Vector2(transform.position.x + 1f, transform.position.y);
			} else if (transform.position.x > foodX){
				transform.position = new Vector2(transform.position.x - 1f, transform.position.y);
			} else if (transform.position.y < foodY){
				transform.position = new Vector2(transform.position.x, transform.position.y + 1f);
			} else if (transform.position.y > foodY){
				transform.position = new Vector2(transform.position.x, transform.position.y - 1f);
			} else if (transform.position.x == foodX && transform.position.y == foodY && food != null){
				// Encontro la comida. Destruye el objeto de la comida (para simular que lo agarro)
				Destroy(food.gameObject);
				food = null;
				foundSomething = false;
				hasFood = true;
				lastX = (int) transform.position.x;
				lastY = (int) transform.position.y;
			}
		} else {
			foundSomething = false;
		}
	}

	void followFeromone(){
		if (index < hormones.Count && (GameObject) hormones[index] != null){
			GameObject hormone = (GameObject) hormones[index];
			if (hormone.GetComponent<HormoneScript>().index <= lastHormoneIndex){
				int hormoneX = (int) hormone.transform.position.x;
				int hormoneY = (int) hormone.transform.position.y;
				// Revisar posicion de hormona encontrada e
				// ir hacia esa posicion
				if (transform.position.x < hormoneX){
					transform.position = new Vector2(transform.position.x + 1f, transform.position.y);
				} else if (transform.position.x > hormoneX){
					transform.position = new Vector2(transform.position.x - 1f, transform.position.y);
				} else if (transform.position.y < hormoneY){
					transform.position = new Vector2(transform.position.x, transform.position.y + 1f);
				} else if (transform.position.y > hormoneY){
					transform.position = new Vector2(transform.position.x, transform.position.y - 1f);
				} else if (transform.position.x == hormoneX && transform.position.y == hormoneY){
					lastHormoneIndex = hormone.GetComponent<HormoneScript>().index;
					index++;
				}
			} else {
				index++;
			}
		} else {
			foundHormone = false;
		}
	}

	void explore(){
		time += Time.deltaTime;
		if (time > 0.5) {
			time = 0;

			int rand = Random.Range(0,randomFactor);
			// Go Right
			if (rand == 1){
				positionX = 1;
				positionY = 0;
				this.gameObject.GetComponent<SpriteRenderer>().sprite = antRight;
			}
			// Go Left
			else if (rand == 2){
				positionX = -1;
				positionY = 0;
				this.gameObject.GetComponent<SpriteRenderer>().sprite = antLeft;
			}
			// Go Up
			else if (rand == 3){
				positionX = 0;
				positionY = 1;
				this.gameObject.GetComponent<SpriteRenderer>().sprite = antUp;
			}
			// Go Down
			else if (rand == 4){
				positionX = 0;
				positionY = -1;
				this.gameObject.GetComponent<SpriteRenderer>().sprite = antDown;
			}
		}
		transform.position = new Vector2(transform.position.x + positionX, transform.position.y + positionY);
	}

	void OnCollisionEnter2D(Collision2D coll) {
		Debug.Log (coll);


	}

	void OnTriggerEnter2D(Collider2D collision) {
		if (!useStay) {
			// Colision con una hormona.
			if (collision.gameObject.tag.Equals ("Hormone") && !foundSomething && !hasFood ) {
				bool exists = false;
				foundHormone = true;
				int thisIndex = collision.gameObject.GetComponent<HormoneScript>().index;
				if (lastHormoneIndex > thisIndex){
					foreach (GameObject gameObject in hormones){
						if (gameObject == collision.gameObject) {
							exists = true;
						}
					}
					if (!exists){
						lastHormoneIndex = thisIndex;
						hormones.Add (collision.gameObject);
					}
				}
			}
			// Colision con comida.
			else if (collision.gameObject.tag.Equals ("Food")) {
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


	void OnTriggerStay2D(Collider2D collision) {
		if (useStay) {
			timeStay += Time.deltaTime;
			if (timeStay > 0.6) {
				timeStay = 0;
				// Colision con una hormona.
				if (collision.gameObject.tag.Equals ("Hormone") && !foundSomething && !hasFood ) {
					bool exists = false;
					foundHormone = true;
					int thisIndex = collision.gameObject.GetComponent<HormoneScript>().index;
					if (lastHormoneIndex > thisIndex){
						foreach (GameObject gameObject in hormones){
							if (gameObject == collision.gameObject) {
								exists = true;
							}
						}
						if (!exists){
							lastHormoneIndex = thisIndex;
							hormones.Add (collision.gameObject);
						}
					}
				}
				// Colision con comida.
				else if (collision.gameObject.tag.Equals ("Food")) {
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
	}

}
