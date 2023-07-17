//This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include <bitset>
#include <string>
using namespace std;

bool AreDigitsUnique(unsigned int value);

int main()
{

	unsigned int x = 0;
	x = 11111;
	unsigned int y = 0;
	y = 123456;
	unsigned int z = 0;
	z = 4051;
	unsigned int t = 0;
	t = 4220678145;
	unsigned int d = 0;
	d = 161206;

	bool areTheyUnique = true;

	areTheyUnique = AreDigitsUnique(x);
	if (areTheyUnique)
		cout << "The Value " << x << " is unique! \n\n";
	else
		cout << "The Value " << x << " is NOT unique! \n\n";

	areTheyUnique = AreDigitsUnique(y);
	if (areTheyUnique)
		cout << "The Value " << y << " is unique! \n\n";
	else
		cout << "The Value " << y << " is NOT unique! \n\n";

	areTheyUnique = AreDigitsUnique(z);
	if (areTheyUnique)
		cout << "The Value " << z << " is unique! \n\n";
	else
		cout << "The Value " << z << " is NOT unique! \n\n";

	areTheyUnique = AreDigitsUnique(t);
	if (areTheyUnique)
		cout << "The Value " << t << " is unique! \n\n";
	else
		cout << "The Value " << t << " is NOT unique! \n\n";

	areTheyUnique = AreDigitsUnique(d);
	if (areTheyUnique)
		cout << "The Value " << d << " is unique! \n\n";
	else
		cout << "The Value " << d << " is NOT unique! \n\n";

}

bool AreDigitsUnique(unsigned int value)
{
	int singleValue;
	bool uniqueIntegers = true;
	bitset<10> bitCheck;


	//First idea was to use a string to parse the number and use the [] to compare, and set some bools.
	//This then gave me the idea that well, BITS are bools. I found this <bitCheck> that allows
	//Easy manipulation of bits, like an array.
	//Toggling each bit 1 for every number 0 - 9
	//If the Bit is already toggled on, then its not unique,
	//If it reaches the end and doesnt return false, all numbers must be unique.

	//Possible bug / issue: Compiler changes numbers that are passed with a 0 in front. IE 0123, doesnt come out as 0123.

	//Estimated time: 30 mins - 15 mins of coding / testing, aprox 10 mins of looking up ways to easily parse and bit operations I didnt remember off the top of my head.
	//5 Min Upgrade!
	//Upgrade! Found out I was using an idx on a switch and sped it up by removing the switch and just using the idx. 

	while (value > 0)
	{
		//Using Modulus we get the remainder of the number, meaning the last digit.
		//Save that last Digint into a int, and then switch.
		singleValue = (value % 10);



		//Check for Early out, if the bit is already on were no Unique.
		if (bitCheck[singleValue])
			return false;
		else
			//Else turn on the bit and break to contiue the while loop
			bitCheck[singleValue] = 1;



		//Divide the number by 10, which will truncate the last digit 1002 becomces 100, store it back on it self. to be updated at the end of the loop.

		value = value / 10;

	}

	//If you have gotten here, then all numbers are unique.
	return true;

}


