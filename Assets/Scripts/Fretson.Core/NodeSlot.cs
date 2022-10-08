using Fretson.Core;
using UnityEngine;
using UnityEngine.UI;

public class NodeSlot : MonoBehaviour
{
    public enum Direction
    {
        In,
        Out
    }

    public enum Orientation
    {
        Up,
        Down,
        Left,
        Right
    }

    public Vector3 Position => transform.position;

    public DataType Type;

    public Image NodeAnchor;

    public Direction DataDirection;

    public Orientation SlotOrientation;

    public void Update()
    {
    }
}