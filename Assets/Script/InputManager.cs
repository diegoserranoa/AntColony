using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

     GameObject clicked;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)){
			RaycastHit2D hit =
                Physics2D.Raycast(
                    Camera.main.ScreenToWorldPoint(
                    Input.mousePosition), Vector2.zero);
			if (hit.collider != null)
			{
				if(hit.collider.tag == "Player")
                {
                    if (clicked != null)
                    {
                        if (clicked != hit.transform.gameObject)
                        {
                            clicked.GetComponent<AntScript>().updater = false;
                            clicked.GetComponent<SpriteRenderer>().color = Color.white;
                        }
                    }
                    hit.transform.gameObject.GetComponent<AntScript>().updater = true;
                    hit.transform.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                    clicked = hit.transform.gameObject;
                }
			}
		}
	}
}
