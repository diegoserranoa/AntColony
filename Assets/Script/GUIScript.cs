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
		GUI.Label (new Rect(95, 7, 100, 100), score.ToString("n2"));
		GUI.Label (new Rect(270, 7, 100, 100), randRatio + "");
		GUI.Label (new Rect(355, 7, 100, 100), expense + "");
        GUI.Label (new Rect(460, 7, 100, 100), iTime + "");
		if (GUI.Button (new Rect (10, 40, 100, 100), "Modo Precisión en sensor")) {
			GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");
			foreach (GameObject link in gameObjects){
				link.GetComponent<AntScript>().useStay = !link.GetComponent<AntScript>().useStay;
			}
		}
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
