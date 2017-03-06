using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MushroomGenerator : MonoBehaviour {
	
	public Text maxMush;
	public int maxScore;

	public GameObject mushroom;
	public GameObject path;
	public GameObject[] tabGameObject;

	public GameObject field;

	public List<int> optimalPath = new List<int>();

	public float horizontalDistance;
	public float verticalDistance;
	public float startX;
	public float startY;

	public float probability = 30;

	public int numberCase = 10;
	public int nbrooms = 100;
	public int[] tabMushrooms;
	// Use this for initialization
	void Start () {
		tabMushrooms = new int[nbrooms];
		for (int i = 0; i < tabMushrooms.Length; i++) {
			tabMushrooms [i] = 1;
		}
		tabGameObject = new GameObject[nbrooms];
		createMushrooms ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void createMushrooms(){
		for (int i = 0; i < numberCase; i++) {
			for (int j = 0; j < numberCase; j++) {
				if ((i != 0 || j != 0) && (i != numberCase - 1 || j != numberCase - 1)) {
					int roomNumber = j * numberCase + i;
					float value = Random.value * 100;
					if (value <= probability) {
						Vector3 pos = new Vector3 (); 
						pos.x = startX + i * horizontalDistance; 
						pos.y = startY - j * verticalDistance; 
						GameObject g = Instantiate (mushroom, pos, Quaternion.identity,field.transform);
						tabGameObject [roomNumber] = g;	
						tabMushrooms [roomNumber] = 0;
					}
				}
			}
		}
	}

	public void resetMushrooms(){
		for (int i = 0; i < numberCase; i++) {
			for (int j = 0; j < numberCase; j++) {
				int roomNumber = j * numberCase + i;
				if (tabMushrooms [roomNumber] == 0) {
					Vector3 pos = new Vector3 (); 
					pos.x = startX + i * horizontalDistance; 
					pos.y = startY - j * verticalDistance; 
					GameObject g = Instantiate (mushroom, pos, Quaternion.identity, field.transform);
					tabGameObject [roomNumber] = g;	
				}
			}
		}
	}

	public void Disjtra(Graph G){
		List<Node> remainingNodes = G.graphNodes;
		Node sdeb = remainingNodes [0];
		Node sfin = remainingNodes [nbrooms - 1];
		while (remainingNodes.Count > 0) {
			Node s1 = findminScore (remainingNodes);

			remainingNodes.Remove (s1);
			for (int i = 0; i < s1.arcs.Count; i++) {
				updateScores (s1, s1.arcs[i].finish);
			}
		}
			
		Node s = sfin;
		while (s != sdeb) {
			optimalPath.Add (s.id);
			s = s.father;
		}
		for (int i=0; i< optimalPath.Count; i++){
			maxScore = maxScore + (1-tabMushrooms [optimalPath [i]]);
			Vector3 pos = new Vector3 ();
			pos.x = startX + (optimalPath[i]%numberCase) * horizontalDistance; 
			pos.y = startY - (optimalPath[i]/numberCase) * verticalDistance; 
			Instantiate (path, pos, Quaternion.identity, field.transform);
			}
		maxMush.text = "Maximal amount of mushrooms : " + maxScore;
	}



	public Node findminScore(List<Node> nodes){
		int minScore = 1000;
		Node minNode = null;
		for (int i = 0; i < nodes.Count; i++) {
			Node currentNode = nodes [i];
			int currentScore = currentNode.score;
			if (currentScore < minScore) {
				minScore = currentScore;
				minNode = currentNode;
			}
		}
		return minNode;
	}

	public void updateScores(Node s1, Node s2){
		int newScore = s1.score + tabMushrooms [s2.id];
		if (s2.score > newScore) {
			s2.score = newScore;
			s2.father = s1;
		}
	}
}
