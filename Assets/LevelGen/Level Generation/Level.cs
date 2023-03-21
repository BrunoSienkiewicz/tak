using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;
using System;


public class Level
{
    public List<Room> pool;
    public Dictionary<RoomTrait,int> roomCount;
}

public class LevelInstance
{
    Rng rng = new Rng(1);
    public List<RoomInstance> rooms;
    Level description;
    public Dictionary<RoomTrait,int> roomCount;

    public Room GetRandomRoom(RoomTrait include = RoomTrait.All, RoomTrait exclude = RoomTrait.None)
    {
        List<Room> options = (from room in description.pool
                                    where (room.traits & include)!=0 && (room.traits & exclude)==0
                                    select room).ToList<Room>();

        return options[rng.Next(0,options.Count)];
    }



    public LevelInstance(Level description)
    {
        this.description = description;
        for (int i = 1; i<=(int)RoomTrait.Exit; i=i<<1)
        {
            roomCount.Add((RoomTrait)i,0);
        }
    }
}

/*
    public RoomInstance GetRandomRoomWithSealedGates()
    {
        var options = (from room in rooms
                        where room.sealedGateCount>0
                        select room).ToList();
        var selected = options[rng.Next(0,options.Count)];
        return selected;
    }
    public void CreateRoom(Room room, RoomInstance anchor=null)
    {
        RoomInstance curr = new RoomInstance(room);
        curr.ConnectRooms(anchor);
        rooms.Add(curr);
    }

    public bool CheckForAllRequirements()
    {
        foreach (var i in roomCount.Keys)
        {
            if (RequirementSatisfied(i)==-1)
                return false; 
        }
        return true;
    }
    public void Generate()
    {
        CreateRoom(GetRandomRoom(RoomTrait.Entrance));
        bool viable = true;
        while (!CheckForAllRequirements())
        {
            Room curr;
            IEnumerable<RoomTrait> flags;
            do
            {
                curr = GetRandomRoom(RoomTrait.Action,RoomTrait.Entrance | RoomTrait.Exit);
                flags = curr.traits.GetFlags();
                foreach (var flag in flags)
                {
                    if (RequirementSatisfied(flag)==1)
                    {
                        viable = false;
                        break;
                    }
                }
            } while (!viable);
            CreateRoom(curr,GetRandomRoomWithSealedGates());
            foreach (var i in flags)
            {
                roomCount[i]++;
            }
        }
    }


*/