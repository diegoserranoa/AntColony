using UnityEngine;
using System.Collections;

public class QueenScript : MonoBehaviour {

	int food = 0;
	public int neededFood;
	int eggX = 10;
	int eggY = 10;
	public Transform eggPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (food >= neededFood) {
			// crear larva
			food = 0;
			Transform egg = Instantiate(eggPrefab) as Transform;
			egg.transform.position = new Vector2(eggX, eggY);
			eggY += 10;
			Destroy (egg.gameObject, 5);

		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		Debug.Log ("queen");
		
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.tag.Equals ("Food2")) {
			Destroy (collision.gameObject);
			food++;
		}
		
	}
}
