using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator
{
    public RoomGenerator(int maxIterations, int roomLengthMin, int roomWidthMin)
    {
    }

    public List<RoomNode> GenerateRoomsInAGivenSpaces(List<Node> roomSpaces)
    {
        List<RoomNode> listToReturn = new List<RoomNode>();
        foreach (var space in roomSpaces)
        {
            Vector2Int newBottomLeftPoint = StructureHelper.GenerateBottomLeftCornerBetween(space.BottomLeftAreaCorner, space.TopRightAreaCorner, 0.1f, 1);

            Vector2Int newTopRightPoint = StructureHelper.GenerateTopRightCornerBetween(space.BottomLeftAreaCorner, space.TopRightAreaCorner, 0.9f, 1);
            space.BottomLeftAreaCorner = newBottomLeftPoint;
            space.TopRightAreaCorner = newTopRightPoint;
            space.TopRightAreaCorner = new Vector2Int(newTopRightPoint.x, newBottomLeftPoint.y);
            space.TopLeftAreaCorner = new Vector2Int(newBottomLeftPoint.x, newTopRightPoint.y);
            listToReturn.Add((RoomNode)space);
        }
        return listToReturn;
    }
}