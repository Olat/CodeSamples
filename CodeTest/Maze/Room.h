#pragma once
#include "stdafx.h"
class Room
{
	string name;
	Room* exits[4];

	bool HasRoomBeenExplored(Room* _room, vector<Room*> _path);

public:
	//Default Constructor
	Room();
	Room(string _name);

	//Functions
	enum EXIT { NORTH = 0, EAST, SOUTH, WEST };
	Room* GetExit(EXIT _direction);
	void AddExit(EXIT _direction, Room* _exit);
	void AddExits(Room* _north, Room* _east, Room* _south, Room* _west);
	bool PathExistsTo(const string& _endingRoomName);

};




