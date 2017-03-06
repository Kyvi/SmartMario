using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : GameEntity {

	public Text currentMush;
	public Text winlose;
	public Text timeLeft;
	public int nbMush = 0;

	public GameObject Generator;
	public GameObject path;
	private MushroomGenerator mushroomGenerator;
	private bool finish = false;
	public float timer = 3.0f;
	public float nextTimer = 3.0f;
	// Use this for initialization
	void Start () {
		mushroomGenerator = Generator.GetComponent<MushroomGenerator> ();
	}
	
	// Update is called once per frame
	void Update () {
		timeLeft.text = "Remaining time : " +(timer - Time.time).ToString("F1");
		if (Time.time > timer) {
			winlose.text = "You Lose !";
			Time.timeScale = 0;
		} else {
			if (Input.GetButtonDown ("Horizontal") && posX < 9) {
				timer = Time.time + nextTimer;
				posX++;
				Vector3 pos = transform.position;
				pos.x = transform.position.x + horizontalDistance;
				Instantiate (path, pos, Quaternion.identity, this.transform.parent.transform);
				collectMushroom ();
				transform.position = pos;
			}
			if (Input.GetButtonDown ("Vertical") && posY < 9) {
				timer = Time.time + nextTimer;
				posY++;
				Vector3 pos = transform.position;
				pos.y = transform.position.y - verticalDistance;
				Instantiate (path, pos, Quaternion.identity, this.transform.parent.transform);
				collectMushroom ();
				transform.position = pos;
			}

			if (posX == 9 && posY == 9 && !finish) {
				mushroomGenerator.resetMushrooms ();
				mushroomGenerator.Disjtra (new Graph (mushroomGenerator.tabMushrooms));
				finish = true;
				if (mushroomGenerator.maxScore > nbMush) {
					winlose.text = "You Lose !";
				} else {
					winlose.text = "You Win !";
				}
				Time.timeScale = 0;
			}
		}
	}

	public void collectMushroom(){
		int numberRoom = posY * mushroomGenerator.numberCase + posX;
		if (mushroomGenerator.tabMushrooms[numberRoom] == 0){
			nbMush++;
			currentMush.text = "Mushrooms Collected : " + nbMush;
			Destroy (mushroomGenerator.tabGameObject [numberRoom]);
		}
	}



}
