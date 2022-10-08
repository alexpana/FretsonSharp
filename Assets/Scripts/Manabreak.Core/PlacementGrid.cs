using System.Collections.Generic;
using System.Linq;
using Core;
using Core.PubSub;
using Messaging;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
///     Handles building obstruction and snapping
/// </summary>
public class PlacementGrid
{
    /// <summary>
    ///     Size of a grid cell in world units
    /// </summary>
    public readonly int GridCellSize;

    /// <summary>
    ///     The size of the map (in number of cells) used to detect out-of-map interactions
    /// </summary>
    public readonly int MapSize;

    /// <summary>
    ///     Every obstruction in the game is collected in this list
    /// </summary>
    public readonly List<RectInt> Obstructions = new();

    /// <summary>
    ///     A value map containing obstruction data which can be used outside the grid
    ///     to display obstructions on the terrain for example.
    /// </summary>
    private readonly ValueMap<bool> _obstructionMap;

    private IPublisher<ObstructionEvent> _obstructionPub;

    public PlacementGrid(int cellSize, int mapSizeMeters, ValueMap<bool> obstructionMap)
    {
        Assert.IsTrue(mapSizeMeters % cellSize == 0, $"The map size must be a multiple of cellSize({cellSize}).");

        GridCellSize = cellSize;
        MapSize = mapSizeMeters / cellSize;
        _obstructionMap = obstructionMap;
    }

    [Inject]
    public void Initialize(IPublisher<ObstructionEvent> addedPublisher)
    {
        _obstructionPub = addedPublisher;
    }

    public Vector2Int WorldToGrid(Vector3 worldPosition)
    {
        return new Vector2Int(
            Mathf.FloorToInt(worldPosition.x / GridCellSize),
            Mathf.FloorToInt(worldPosition.z / GridCellSize));
    }

    public Vector3 GridToWorld(Vector2Int gridPosition)
    {
        return new Vector3(gridPosition.x * GridCellSize, 0f, gridPosition.y * GridCellSize);
    }

    public void Obstruct(RectInt footprint)
    {
        // TODO: check if footprint is outside the map
        var obstruction = new RectInt(footprint.position, footprint.size);
        Obstructions.Add(obstruction);
        _obstructionPub.Publish(new ObstructionEvent(ObstructionEvent.EventType.Added, obstruction));
        UpdateConstructionMap();
    }

    public void RemoveObstruction(RectInt footprint)
    {
        var gridObstructionIndex = Obstructions.FindIndex(obstruction => obstruction.Equals(footprint));
        if (gridObstructionIndex >= 0)
        {
            var obstruction = Obstructions[gridObstructionIndex];
            Obstructions.RemoveAt(gridObstructionIndex);
            _obstructionPub.Publish(new ObstructionEvent(ObstructionEvent.EventType.Removed, obstruction));
            UpdateConstructionMap();
        }
    }

    public bool IsObstructed(RectInt footprint)
    {
        return Obstructions.Any(gridObstruction => gridObstruction.Overlaps(footprint));
    }

    private void UpdateConstructionMap()
    {
        if (_obstructionMap == null) return;

        _obstructionMap.Clear();
        foreach (var rectInt in Obstructions) _obstructionMap.SetValue(rectInt, false);

        _obstructionMap.Apply();
    }

    public class ObstructionEvent : IMessage
    {
        public enum EventType
        {
            Added,
            Removed
        }

        public readonly RectInt Obstruction;

        public readonly EventType Type;

        public ObstructionEvent(EventType type, RectInt obstruction)
        {
            Type = type;
            Obstruction = obstruction;
        }
    }
}