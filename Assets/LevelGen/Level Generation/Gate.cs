
public class Gate //Describes things like Gate's position in the room
{

}

public class GateInstance
{
    public Gate description;
    public RoomInstance room;
    public GateInstance other; 
    public bool open = true;

    public GateInstance(RoomInstance room, Gate description, GateInstance other = null)
    {
        this.description = description;
        this.room = room;
        this.other = other;
    }

    public static void ConnectGates(GateInstance a, GateInstance b)
    {
        a.other = b;
        b.other = a;
    }
}
