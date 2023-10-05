using UnityEngine;


public class RoomNode : Node
{
    public RoomNode(Vector2Int bottomLeftAreaCorner, Vector2Int toprightAreaCorner, Node parentNode, int index) : base(parentNode)
    {
        this.BottomLeftAreaCorner = bottomLeftAreaCorner;
        this.TopRightAreaCorner = toprightAreaCorner;
        this.BottomRightAreaCorner = new Vector2Int(toprightAreaCorner.x, bottomLeftAreaCorner.y);
        this.TopLeftAreaCorner = new Vector2Int(bottomLeftAreaCorner.x, toprightAreaCorner.y);
        this.TreeLayerIndex = index;
    }

    public int Width { get => (int)(TopRightAreaCorner.x - BottomLeftAreaCorner.x); }
    public int Length { get => (int)(TopRightAreaCorner.y - BottomLeftAreaCorner.y); }


}