using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph {


	public int nbrooms = 100;
	public int[] tabMushrooms;

	/// <summary>
	/// The graph nodes.
	/// </summary>
	public List<Node> graphNodes;

	/// <summary>
	/// Initializes a new instance of the <see cref="Graph"/> class.
	/// </summary>
	/// <param name="n">root</param>
	/// <param name="d">depth</param>
	public Graph(int[] tab){
		graphNodes = new List<Node> ();
		this.tabMushrooms = new int[nbrooms];

		for (int i = 0; i < nbrooms; i++) {
			this.tabMushrooms [i] = tab [i];
		}

		createGraph ();
	}


	public void createGraph(){
		for (int i = 0; i < nbrooms; i++) {
			
			if (i == 0) {
				graphNodes.Add (new Node (0, 0, 0, 0));
			} else {
				graphNodes.Add (new Node (i, i / 10, i % 10, 1000));
			}
		}

		for (int i = 0; i < nbrooms; i++) {
			Node node = graphNodes [i];

			if (node.line != 9) {
				node.addArc (graphNodes [i+10], tabMushrooms[i+10]);
			}
			if (node.column != 9) {
				node.addArc(graphNodes[i+1],tabMushrooms[i+1]);
			}
		}

		/*for (int i = 0; i < nbrooms; i++) {
			Debug.Log (graphNodes[i].arcs.Count);
			for (int j=0; j< graphNodes[i].arcs.Count; j++){
				Debug.Log(graphNodes [i].arcs[j].finish.print());
					}
		}*/
	}

	/// <summary>
	/// Creates the graphNodes.
	/// </summary>
	/*public void createGraph(){
		int d = depth; // Depth of the graph
		List<Node> aux = new List<Node> (); // Memory List
		List<Node> currentNodes = new List<Node> (); // Building List
		currentNodes.Add (root); // Adds root as the first node of the Building List.

		while (d >= 0) {
			for (int i = 0; i < currentNodes.Count; i++) {
				Node currentNode = currentNodes [i]; //takes out each Nodes from the Building list
				aux.Add (currentNodes [i]); // Adds the nodes to the memory List.
				graphNodes.Add (currentNodes [i]); // Adds the nodes to the graphNodes List.

			}
			currentNodes.Clear (); // Deletes the building list 
			for (int i = 0; i < aux.Count; i++) {
				aux [i].createChildren (); // Create children for every nodes of the memory List
				for (int j = 0; j < aux[i].children.Count; j++) {
					currentNodes.Add (aux [i].children [j]); // Adds those children to the building List
				}
			}
			aux.Clear (); // Deletes the memory List
			d--; 
		}

	}*/


}