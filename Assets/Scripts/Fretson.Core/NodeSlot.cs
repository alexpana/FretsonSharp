using Fretson.Core;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NodeSlot : MonoBehaviour, IDragHandler
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

    public string Label;

    private TMP_Text _labelText;

    public bool PositionDirty = false;

    private void Start()
    {
        _labelText = GetComponentInChildren<TMP_Text>();

        if (_labelText != null)
        {
            _labelText.text = Label;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
    }
}