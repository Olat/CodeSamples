#include <iostream>
#include <string>

using namespace std;
void SortLetters(std::string& input, const std::string& sortOrder);

const int LettersInAlphabet = 26;

int main()
{
    /*
    void SortLetters(std::string& input, const std::string& sortOrder)
    Example:
    std::string inputStr(“sort the letters in this string”);
    SortLetters(inputStr, “ isetlgornh”);
 
    Result: The input string would be changed to “     iiisssseeettttttlgorrrnnhh”.
    */


    //---Testing custom input ---// 

    //string input = "abcddeefffgghhhhhijklmmnnooooppqq";
    //string order = "qopnmlkabcijghef";

    //cout << "Input string is: " << input <<"\n";
    //cout << "Sort string is: " << order << "\n";

    //SortLetters(input, order);

    //cout << "Sorted string is: " << input << "\n\n";

    //cout << "\n\n";

    //-----Testing to see if Spaces WERE my issue, and I was right --- //

    //string input2 = "thelazybrowndogsitsontheredtree";
    //string order2 = "zywtsronlihgedba";

    //cout << "Input string is: " << input2 << "\n";
    //cout << "Sort string is: " << order2 << "\n";
    //SortLetters(input2, order2);
    //cout << "Sorted string is: " << input2 << "\n\n";
    // 
    // 

    //----- Testing with spaces implemented to see if fix works and it does!!!!! -- //

    string input2 = "the 1st - ! lazy brown dog sits on the 2nd red tree";
    string order2 = "1 zywtsronl2ihgedba";


    cout << "Input string is: " << input2 << "\n";
    cout << "Sort string is: " << order2 << "\n";
    SortLetters(input2, order2);
    cout << "Sorted string is: " << input2 << "\n\n";

    //-- Testing your example --//
    string inputStr("sort the letters in this string");

    cout << "Input string is: " << inputStr << "\n";
    cout << "Sort string is: " << " isetlgornh" << "\n";
    SortLetters(inputStr, " isetlgornh");
    cout << "Sorted string is: " << inputStr << "\n\n";

    //----------------------------------------Test 2 --------------------------------//

}


//Estimated time 1hr and 40min - Trial / Error 10 mins, research 10 mins, implement my findings 10 mins, 40 mins trying different things, 10 mins for Upgrade!
//then trying to fix the edge case of " " blank spaces 
//Blank spaces gave me a bit of trouble in testing out different strings.

void SortLetters(std::string& input, const std::string& sortOrder)
{
    //I dont want to parse the string many times over. As this will exponentially slow down my function
    //I am counting the letters in the string every, so after the order is figured out, I can just 
    //dump the letters in order by the count of them.
    //So if there are 5 A's when its A's turn I can just Burp out 5 of  them and move on.

    //My count array, filled with 0's
    //26 letters, + white space = 27
    //---Upgrade---//
    //Decided to then upgrade to CHAR_MAX to take in everything.
   //int count[LettersInAlphabet + 1] = { 0 };
    int count[CHAR_MAX] = { 0 };


    //Looping through the input
    for (int i = 0; i < input.length(); i++)
        //using the character codes, as an offset into the array. Making A = idx 0, B is idx 1, so forth and son on.
        //the ++ increments the count of that spot in the Array.        
    {

        //otherwise its a letter were good to go.
        count[input[i]]++;
    }

    int idx = 0;
    //We loop through the sort order,
    for (int i = 0; i < sortOrder.length(); i++)
        //Starting with the first letter, we loop through the number of times i need to set it
    {
        for (int j = 0; j < count[sortOrder[i]]; j++)
            //We set that order, and then go onto the next letter in the Sort Order
            //We post increment the Idx to move on properly when we are done.
            input[idx++] = sortOrder[i];

    }

    int n = 0; //debug spot
}
