using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour {

	public float 		        score;
	public int 		        expense;
	public double 	        randRatio;
    public static GameObject       current;
    private float           time;
    private int             iTime;

	// Use this for initialization
	void Start () {
        time = 0.0f;
	}

	// Update is called once per frame
	void Update () {
        time = time + Time.deltaTime;
        if (current != null)
        {
            setTime();
            setScore();
            setRandom();
            setExpense();
        }
	}

    public static void setGameObject(GameObject o)
    {
        current = o;
    }

	void OnGUI () {
		GUI.Label (new Rect(150, 15, 100, 100), score.ToString("n2"));
		if (randRatio == 0){
			GUI.Label (new Rect(410, 15, 100, 100), randRatio + "");
		} else {
			GUI.Label (new Rect(410, 15, 100, 100), (randRatio - 1) + "");
		}
		GUI.Label (new Rect(550, 15, 100, 100), expense + "");
        GUI.Label (new Rect(700, 15, 100, 100), iTime + "");
		/*
		if (GUI.Button (new Rect (10, 40, 100, 100), "Modo Precisión en sensor")) {
			GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");
			foreach (GameObject link in gameObjects){
				link.GetComponent<AntScript>().useStay = !link.GetComponent<AntScript>().useStay;
			}
		}
		*/
	}

    void setScore() {
        if (expense != 0)
            score = (current.GetComponent<AntScript>().score) / expense;
    }

    void setRandom() {
        randRatio = current.GetComponent<AntScript>().randomFactor;
    }

    void setExpense() {
        expense = current.GetComponent<AntScript>().expense;
    }

    public void setTime()
    {
        iTime = (int)(time);
    }
}
