#include <iostream>
#include <fstream>
#include <string>
#include <vector>

//linked list node definition
typedef struct LinkNode
{
	struct LinkNode *pPrev;
	struct LinkNode *pNext;
	int *pData; //dynamically allocated data.
} LinkNode_t;

//global pointer to head of linked list
LinkNode_t *g_pLinkedList;

typedef struct BinaryTreeNode
{
	struct BinaryTreeNode *pLeft;
	struct BinaryTreeNode *pRight;
	int data;
	int largestPath;
	
}BinaryTreeNode_t;

char* Convert(const char *pHexString);
void KillNode(LinkNode_t *pNodeToRemove);
int BTTSum(BinaryTreeNode_t* root, int depth);
BinaryTreeNode_t* CreateTree(const char* _filename);

int main()
{
	std::cout << "Hello World!\n";
	char num[] = "3F456A";
	int currentMax = -1;
	BinaryTreeNode_t* _root = nullptr;
	std::string file;
	file = "test-tree.txt";
	
	_root = CreateTree(file.c_str());

	int max = 0;
	max = BTTSum(_root,0);
	std::cout << max;
	int i = 0;
}

char* Convert(const char *pHexString)
{
		int len = strlen(pHexString);
		int base = 1;
		int temp = 0;
		char pDecString[] = "";
		for (int i = len - 1; i >= 0; i--) 
		{
			if (pHexString[i] >= '0' && pHexString[i] <= '9') 
			{
				temp += (pHexString[i] - 48)*base;
				base = base * 16;
			}
			else if (pHexString[i] >= 'A' && pHexString[i] <= 'F') 
			{
				temp += (pHexString[i] - 55)*base;
				base = base * 16;
			}
		}
		*pDecString = temp;
		return &(*pDecString);
	
}

void KillNode(LinkNode_t *pNodeToRemove)
{

	//Bunch of edge cases for a DLL to check 4.
	//If Head
	//If LastNode
	//If Tail
	//Else Im a Normal Node

	if (pNodeToRemove->pPrev == NULL) // I am The head, lets see if I am also the tail...
	{
		if (pNodeToRemove->pNext == NULL) // I am the only node. When I delete my self, I need to clean up all pointers
		{
			g_pLinkedList = NULL; // Set the link list pointer to null
			delete(pNodeToRemove->pData); //Free the Data.
			delete(pNodeToRemove); // Lastly Clean up my own pointer.
		}
		else//I am the Head but not the last node;
		{
			g_pLinkedList = pNodeToRemove->pNext; //Update the head pointer;
			pNodeToRemove->pNext->pPrev = NULL; //Update the next guys Prev pointer that WAS looking at me to NULL
			delete(pNodeToRemove->pData); //Free the Data.
			delete(pNodeToRemove); // Lastly Clean up my own pointer.
		}
	}
	else if (pNodeToRemove->pPrev != NULL && pNodeToRemove->pNext == NULL) // I am the tail
	{
		pNodeToRemove->pPrev->pNext = NULL; //Update the guy looking at me
		delete(pNodeToRemove->pData); //Free the Data.
		delete(pNodeToRemove); // Lastly Clean up my own pointer.
	}
	else //Just a normal block
	{
		pNodeToRemove->pPrev->pNext = pNodeToRemove->pNext; //Update the guy looking at me to look past me
		pNodeToRemove->pNext->pPrev = pNodeToRemove->pPrev; //Update the guy looking at me to look ahead of me
		delete(pNodeToRemove->pData); //Free the Data.
		delete(pNodeToRemove); // Lastly Clean up my own pointer.
	}
}

//Travereses to the first Leaf node, Checks the sum of it self with the one above it for which is greater. 
//Following the path of the greatest sum back up to the root.
int BTTSum(BinaryTreeNode_t* root, int depth)
{


	if (root == NULL) //Early out
		return -1;

	if (root->pLeft == nullptr && root->pRight == nullptr) //Traverse to end to make sure I am aleaf
	{
		return root->data; //Return my value
	}

	int left, right = 0;

	if (root->pLeft->largestPath == 0)//Check to see if weve already checked down this path
		left = BTTSum(root->pLeft, depth + 1); //Recursively through the left path
	else
		left = root->pLeft->largestPath; //Lefts largest path sum has already been calculated, use it instead of retraversing.

	if (root->pRight->largestPath == 0)//Check to see if weve already checked down this path
		right = BTTSum(root->pRight,depth+1); //Recursively travel the right path
	else
		right = root->pRight->largestPath; //Rights largest path has already been calculated, use it instead of retraversing.


	int largestSum = root->data;
	largestSum += (left > right ? left : right);
	root->largestPath = largestSum;
	//if(depth < 3)
		//std::cout << "Largest Sum at Depth: " << depth << " is " << largestSum << '\n';
	return largestSum; //If my Left is > then my Right. Add my data to his and go up a node. Else Use the Right and go up a node.
	//This adds my value to the next highest of the two, given me the highest sum as I traverse back to the root.

}

//Traverses down the list of data from the given file.
//Creates the Nodes, and attaches them as it goes.
//It keeps track of the Previous Nodes so it can assign their children after the children are created.

BinaryTreeNode_t* CreateTree(const char* _filename)
{
	std::fstream fs;
	BinaryTreeNode_t *_rootNode = nullptr;
	fs.open(_filename, std::fstream::in);
	char * input = new char();
	int data = 0;
	int depthCount = 1;
	int nodeCount = 0;
	std::vector< BinaryTreeNode_t*> previousDepth;
	std::vector< BinaryTreeNode_t*> currentDepth;

	if (fs.is_open())
	{
		while (!fs.eof())
		{
			fs >> data; //read in the data
			BinaryTreeNode_t * currNode = new BinaryTreeNode_t(); // New Node 
			currNode->data = data;  // Set Data

			if (_rootNode == nullptr) //check if this is root
			{
				_rootNode = currNode; //set the root = to current node.
				currentDepth.push_back(currNode); //Push back this node on the currenList
				nodeCount++; //Increment the count
				continue; //Skip the other crap because this is the root.
			}

			if (nodeCount >= depthCount) //Check if we are on the next row of data
			{
				depthCount++; 
				nodeCount = 0; //reset node count
				previousDepth = currentDepth;  // Set previous to current
				currentDepth.clear(); // clear it out.
			}
			
			currentDepth.push_back(currNode);
			if (nodeCount == 0)
			{ 
				previousDepth[0]->pLeft = currNode;  //This is left most node of the row
			}
			else
			{
				previousDepth[nodeCount - 1]->pRight = currNode; // Setting the right node
				if(nodeCount < previousDepth.size()) //Check if we are not on right most
					previousDepth[nodeCount]->pLeft= currNode; // Set the Left node.
			}
			nodeCount++; 
			

			int i = 0;
		}

		fs.close();
	}
	return _rootNode;

}