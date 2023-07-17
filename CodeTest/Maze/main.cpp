// Maze.cpp : This file contains the 'main' function. Program execution begins and ends there.
#include "stdafx.h"

//Time: 30 mins Designing and implementinmg functions.
//Research 1hr - Finding a simple solution. Source::https://stackoverflow.com/questions/713508/find-the-paths-between-two-given-nodes
//Implementation 1hr - changing it to more readable code and using Room *'s instead of int and char
//Testing 1 hr -  Test cases, and printing out test cases. Added Rand to see better varied results.

int main()
{

    srand(time(NULL));
    const int MazeSize = 200;
    Room* theMaze[MazeSize] = { NULL };


    // theMaze
    for (size_t i = 0; i < MazeSize; i++)
    {
        string name = "Room: " + to_string(i);
        Room* room = new Room(name);
        theMaze[i] = room;
    }


    for (int i = 0; i < MazeSize; i++)
    {
        for (int d = 0; d < 4; d++)
        {
            int j = rand() % (MazeSize * 2);

            if (j < MazeSize)
                theMaze[i]->AddExit((Room::EXIT)d, theMaze[j]);

        }
    }


    //Test cases

    bool isPath = false;
    cout << "\nWe're looking for a path from Room: 0 to Room: 4 \n";
    isPath = theMaze[0]->PathExistsTo("Room: 4");
    if (isPath == false)
        cout << "It was a dead end...\n\n";

    int numTimes = rand() % 30;
    for (int i = 0; i < numTimes; i++)
    {
        string roomToCheck = "Room: " + to_string(rand() % MazeSize);
        int randomRoom = rand() % MazeSize;

        cout << "We're looking for a path from Room: " << randomRoom << " to " << roomToCheck << "\n";
        isPath = theMaze[randomRoom]->PathExistsTo(roomToCheck);
        if (isPath == false)
            cout << "It was a dead end...\n\n";
    }

    for (size_t i = 0; i < MazeSize; i++)
    {
        delete theMaze[i];
    }

    int i = 0; //debug line

}


