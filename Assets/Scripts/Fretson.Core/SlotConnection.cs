namespace Fretson.Core
{
    public class SlotConnection
    {
        public NodeSlot Source;
        public NodeSlot Destination;

        public SlotConnection(NodeSlot slotA, NodeSlot slotB)
        {
            if (slotA.DataDirection == NodeSlot.Direction.Out)
            {
                Source = slotA;
                Destination = slotB;
            }
            else
            {
                Source = slotB;
                Destination = slotA;
            }
        }

        public bool Connects(NodeSlot slotA, NodeSlot slotB)
        {
            return Source == slotA && Destination == slotB || Source == slotB && Destination == slotA;
        }
    }
}