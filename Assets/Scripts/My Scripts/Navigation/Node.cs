using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A node that connects to other nodes, which the AI can traverse.
/// </summary>
public class Node : MonoBehaviour
{
    /// <summary>
    /// The list containing the connecting nodes.
    /// </summary>
    public List<Node> nodeList;

    /// <summary>
    /// Debug variable to show the connections of a node in edit mode.
    /// </summary>
    public bool ShowConnections=false;

    /// <summary>
    /// Gets a node that has not been used in the pattern before.
    /// </summary>
    /// <param name="randomInt">A random integer provided by the path builder.</param>
    /// <param name="usedNodes">A list of nodes that have already been used in the path.</param>
    /// <returns>Returns a unused node, </returns>
    public Node GetNode(int randomInt, List<Node> usedNodes) {
        int i = 0;
        while(usedNodes.Contains(nodeList[((randomInt + i) % nodeList.Count)])) {
            i++;
            if(i == nodeList.Count) {
                return null;
            }
        }
            
        return nodeList[((randomInt + i) % nodeList.Count)];
    }

    void OnDrawGizmos() {
        Gizmos.DrawSphere(transform.position, 1f);
        if(ShowConnections)
            foreach(Node otherNode in nodeList)
                Gizmos.DrawLine(transform.position, otherNode.transform.position);
    }
}

