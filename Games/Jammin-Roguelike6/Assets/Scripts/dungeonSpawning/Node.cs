using System;
using System.Collections.Generic;
using UnityEngine;
public abstract class Node
{
    private List<Node> childrenNodeList;

    public List<Node> ChildrenNodeList { get => childrenNodeList; }

    public bool Visited { get; set; }
    public Vector2Int BottomLeftAreaCorner { get; set; }
    public Vector2Int BottomRightAreaCorner { get; set; }
    public Vector2Int TopRightAreaCorner { get; set; }
    public Vector2Int TopLeftAreaCorner { get; set; }

    public Node Parent { get; set; }

    public int TreeLayerIndex { get; set; }

    public Node(Node parentNode)
    {
        childrenNodeList = new List<Node>();
        this.Parent = parentNode;
        if (parentNode != null)
        {
            parentNode.addChild(this);
        }
    }

    public void addChild(Node node)
    {
        childrenNodeList.Add(node);
    }

    public void removeChild(Node node)
    {
        childrenNodeList.Remove(node);
    }
}