#include "Room.h"
Room::Room()
{
	this->name = "New Room";
	exits[NORTH] = NULL;
	exits[EAST] = NULL;
	exits[SOUTH] = NULL;
	exits[WEST] = NULL;

}
Room::Room(string _name)
{
	this->name = _name;
	exits[NORTH] = NULL;
	exits[EAST] = NULL;
	exits[SOUTH] = NULL;
	exits[WEST] = NULL;
}
//Connects a new room to a single exit.
void Room::AddExit(EXIT _direction, Room* _exit)
{
	exits[_direction] = _exit;
}
//Connects a new room to multiple exits.
//Example: newRoom = new room()
//         newRoom->AddExits(NULL,Room2,Room1,NULL)
void Room::AddExits(Room* _north, Room* _east, Room* _south, Room* _west)
{
	exits[NORTH] = _north;
	exits[EAST] = _east;
	exits[SOUTH] = _south;
	exits[WEST] = _west;
}
Room* Room::GetExit(EXIT _direction)
{
	return exits[_direction];
}

//This takes in the child you are looking at and compares it to 
//the path you have been on
bool Room::HasRoomBeenExplored(Room* _room, vector<Room*> _path)
{
	if (_room == NULL) //If there is no exit there, mark it as a deadend
		return true;

	for (unsigned int i = 0; i < _path.size(); i++)
	{
		if (_room == _path[i])
			return true;
	}
	return false;
}


bool Room::PathExistsTo(const std::string& _endingRoomName)
{
	//This is the start of the path, the current Room
	vector<Room*> path;
	//Keep track of where were going stating with the start room
	path.push_back(this);

	//This is ALL paths available, so we can do some recursion!
	queue<vector<Room*>> availablePaths;
	availablePaths.push(path);

	//A list of rooms we have already searched.
	vector<Room*> searchedRooms;
	searchedRooms.push_back(this);

	//While we still have a path to look at, keep looking.
	while (!availablePaths.empty())
	{
		path = availablePaths.front();
		availablePaths.pop();

		//The room we are curently at is at the end of the current path were checking.
		Room* currRoom = path[path.size() - 1];
		//Check to see if we found the room
		if (currRoom->name == _endingRoomName)
		{
			cout << "The Path was : " << "\n";
			for (unsigned int i = 0; i < path.size(); i++)
			{
				cout << "\t" << path[i]->name << "\n";
			}
			return true;
		}

		//Checks the current rooms children to see if they are in the Searched list.
		if (currRoom->HasRoomBeenExplored(currRoom->GetExit(NORTH), searchedRooms) == false) //If the room hasnt been explored.
		{
			//If not we then say this is a possible new path
			//Push it back to the availablePath Queue for more searching.
			vector<Room*> newPath(path.begin(), path.end());
			newPath.push_back(currRoom->GetExit(NORTH));
			availablePaths.push(newPath);
			searchedRooms.push_back(currRoom->GetExit(NORTH));
		}
		if (currRoom->HasRoomBeenExplored(currRoom->GetExit(EAST), searchedRooms) == false)
		{
			vector<Room*> newPath(path.begin(), path.end());
			newPath.push_back(currRoom->GetExit(EAST));
			availablePaths.push(newPath);
			searchedRooms.push_back(currRoom->GetExit(EAST));
		}
		if (currRoom->HasRoomBeenExplored(currRoom->GetExit(SOUTH), searchedRooms) == false)
		{
			vector<Room*> newPath(path.begin(), path.end());
			newPath.push_back(currRoom->GetExit(SOUTH));
			availablePaths.push(newPath);
			searchedRooms.push_back(currRoom->GetExit(SOUTH));
		}
		if (currRoom->HasRoomBeenExplored(currRoom->GetExit(WEST), searchedRooms) == false)
		{
			vector<Room*> newPath(path.begin(), path.end());
			newPath.push_back(currRoom->GetExit(WEST));
			availablePaths.push(newPath);
			searchedRooms.push_back(currRoom->GetExit(WEST));
		}


	}
	return false;
}
