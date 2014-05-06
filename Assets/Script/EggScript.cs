using UnityEngine;
using System.Collections;

public class EggScript : MonoBehaviour {
	
	public Transform antPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDestroy(){
		// Al destruirse, crear hormiga.
		Transform ant = Instantiate(antPrefab) as Transform;
		ant.transform.position = new Vector2(this.transform.position.x, this.transform.position.y);
	}
}
