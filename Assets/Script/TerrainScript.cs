using UnityEngine;
using System.Collections;

public class TerrainScript : MonoBehaviour {

	public Transform prefab;
	public Transform groundPrefab;
	public int foodVar;

	float time;

	int foodGenerationInterval = 100;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;	
		if (time > 3) {
			time = 0;
			// Generate food

			int x = (Random.Range(-9,9) * 10) + 5;
			int y = (Random.Range(-9,9) * 10) + 5;

			for (int i = 0; i < foodVar; i++){
				Transform food = Instantiate(prefab) as Transform;
				food.transform.position = new Vector2(x, y);
			}

		}
	}
}
