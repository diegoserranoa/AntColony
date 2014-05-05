using UnityEngine;
using System.Collections;

public class TerrainScript : MonoBehaviour {

	public Transform prefab;
	public Transform groundPrefab;

	float time;

	int foodGenerationInterval = 100;
	// Use this for initialization
	void Start () {
		int x = 5;
		int y = -5;
		// parte de abajo
		for (int i = 0; i < 10; i++) {
			Transform ground = Instantiate(groundPrefab) as Transform;
			ground.transform.position = new Vector2(x, y);
			x += 10;
			
		}
		x = 105;
		y = 5;
		for (int i = 0; i < 10; i++) {
			Transform ground = Instantiate(groundPrefab) as Transform;
			ground.transform.position = new Vector2(x, y);
			ground.transform.Rotate(new Vector3(0, 0, 90));
			y += 10;
		}
		x = 95;
		y = 105;
		for (int i = 0; i < 10; i++) {
			Transform ground = Instantiate(groundPrefab) as Transform;
			ground.transform.position = new Vector2(x, y);
			ground.transform.Rotate(new Vector3(0, 0, 180));
			x -= 10;
		}
		x = -5;
		y = 95;
		for (int i = 0; i < 10; i++) {
			Transform ground = Instantiate(groundPrefab) as Transform;
			ground.transform.position = new Vector2(x, y);
			ground.transform.Rotate(new Vector3(0, 0, -90));
			y -= 10;
		}
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;	
		if (time > 15) {
			time = 0;
			// Generate food

			int x = (Random.Range(5,10) * 10) + 5;
			int y = (Random.Range(0,10) * 10) + 5;

			for (int i = 0; i < 10; i++){
				Transform food = Instantiate(prefab) as Transform;
				food.transform.position = new Vector2(x, y);
			}

		}
	}
}
