using System.Collections.Generic;
using Core;
using Core.Input;
using Fretson.Core;
using geniikw.DataRenderer2D;
using UnityEngine;

[Core.Component]
public class ConnectionManager : MonoBehaviour
{
    private NodeSlot _hoveredSlot;
    private UILine _linkShadow;
    private bool _isDraggingConnection = false;

    private List<SlotConnection> _connections = new();

    private NodeSlot _sourceSlot;
    private GameObject _bezierPrefab;

    // Start is called before the first frame update
    private void Start()
    {
        _bezierPrefab = DI.ResolvePrefab(1);
        _linkShadow = Instantiate(_bezierPrefab, GameObject.Find("SlotConnections").transform).GetComponent<UILine>();
        DI.ResolveSubscriber<NodeSlotEventListener.NodeSlotHoveredEvent>().Subscribe(OnNodeSlotHovered);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(MouseButton.Left) && _hoveredSlot != null)
        {
            _sourceSlot = _hoveredSlot;
            _isDraggingConnection = true;
            _linkShadow.gameObject.SetActive(true);
        }

        if (Input.GetMouseButtonUp(MouseButton.Left))
        {
            if (CanConnect(_hoveredSlot, _sourceSlot))
            {
                CreateConnection(_sourceSlot, _hoveredSlot);
            }

            _isDraggingConnection = false;
            _linkShadow.gameObject.SetActive(false);
        }

        if (_isDraggingConnection)
        {
            UpdateBezier(_linkShadow, _sourceSlot.transform.position, Input.mousePosition);
        }
    }

    private bool CanConnect(NodeSlot slotA, NodeSlot slotB)
    {
        if (slotA != null && slotB != null && slotA != slotB &&
            slotA.DataDirection != slotB.DataDirection && slotA.Type == slotB.Type)
        {
            foreach (var slotConnection in _connections)
            {
                if (slotConnection.Connects(slotA, slotB))
                {
                    return false;
                }
            }

            return true;
        }

        return false;
    }

    private void CreateConnection(NodeSlot slotA, NodeSlot slotB)
    {
        // TODO: handle multiple outputs to the same input
        Debug.Log($"Creating connection between {slotA}, {slotB}");
        _connections.Add(new SlotConnection(slotA, slotB));
        CreateBezier(slotA, slotB);
    }

    private void CreateBezier(NodeSlot slotA, NodeSlot slotB)
    {
        var bezier = Instantiate(_bezierPrefab, GameObject.Find("SlotConnections").transform).GetComponent<UILine>();
        UpdateBezier(bezier, slotB.transform.position, slotA.transform.position);

    }

    private void UpdateBezier(UILine bezier, Vector3 from, Vector3 to)
    {
        float handleScale = Mathf.Min(200, (from - to).magnitude / 2.0f);
        bezier.line.EditPoint(0, _sourceSlot.transform.position,
            SlotOrientationToBezierHandle(_sourceSlot.SlotOrientation) * handleScale, Vector3.zero, 2);

        var destinationOrientation = -SlotOrientationToBezierHandle(_sourceSlot.SlotOrientation);
        if (_hoveredSlot != null)
        {
            destinationOrientation = SlotOrientationToBezierHandle(_hoveredSlot.SlotOrientation);
        }

        bezier.line.EditPoint(1, Input.mousePosition,
            Vector3.zero, destinationOrientation * handleScale, 2);
        bezier.GeometyUpdateFlagUp();
    }

    private void OnNodeSlotHovered(NodeSlotEventListener.NodeSlotHoveredEvent e)
    {
        _hoveredSlot = e.Slot;
    }

    private Vector3 SlotOrientationToBezierHandle(NodeSlot.Orientation orientation)
    {
        return orientation switch
        {
            NodeSlot.Orientation.Down => new Vector3(-1, 0, 0),
            NodeSlot.Orientation.Left => new Vector3(-1, 0, 0),
            NodeSlot.Orientation.Right => new Vector3(0, 1, 0),
            NodeSlot.Orientation.Up => new Vector3(0, -1, 0),
            _ => Vector3.zero
        };
    }
}