using UnityEngine;
using System.Collections;

public class TerrainScript : MonoBehaviour {

	public Transform prefab;
	float time;

	int foodGenerationInterval = 100;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;	
		if (time > 15) {
			time = 0;
			// Generate food
			int x = Random.Range(5,10) * 10;
			int y = Random.Range(0,10) * 10;

			for (int i = 0; i < 10; i++){
				Transform food = Instantiate(prefab) as Transform;
				food.transform.position = new Vector2(x, y);
			}
		}
	}
}
