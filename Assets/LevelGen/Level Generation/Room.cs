using System;
using System.Collections.Generic;
using System.Linq;
[Flags]
public enum RoomTrait {
    None = 0,
    Battle = 1 << 0,
    Treasure = 1 << 1,
    Event = 1 << 2,
    Shop = 1 << 3,
    Entrance = 1 << 4,
    Exit = 1 << 5,
    All = 0xFFFFFF,
    Action = Battle | Treasure | Event | Shop
}

public static class Extensions
{
    public static IEnumerable<RoomTrait> GetFlags(this RoomTrait traits)
    {
        return from flag in (IEnumerable<RoomTrait>)Enum.GetValues(typeof(RoomTrait))
                where traits.HasFlag(flag)
                select flag;
    }
}
public class Room{
    string id;
    public List<Gate> gates;
    public RoomTrait traits;

    public Room(string name, float x, float y, float z, RoomTrait traits)
    {
        id = name;
        gates = new List<Gate>();
        this.traits = traits;
    }
}

public class RoomInstance{
    private Room description;
    public List<GateInstance> gates;
    public IEnumerable<GateInstance> sealedGates => from gate in gates
                                                    where gate.other == null
                                                    select gate;
    public int sealedGateCount => sealedGates.Count<GateInstance>();
    public void ConnectRooms(GateInstance other)
    {
        this.sealedGates.First<GateInstance>().other = other;
    }

    public void ConnectRooms(RoomInstance other=null)
    {
        if (other==null) return;
        this.sealedGates.First<GateInstance>().other = other.sealedGates.First<GateInstance>();
    }

    public RoomInstance(Room room)
    {
        description = room;
        gates = new List<GateInstance>();
        foreach (Gate curr in description.gates)
        {
            gates.Add(new GateInstance(this,curr));
        }
    }


}