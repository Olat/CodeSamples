#include <cstdio>
#include <stdio.h>
#include <assert.h>
#include <stdlib.h>
#include <time.h>
#include <new>
#include <memory.h>
#include <locale.h>

#define malloc nonmalloc  // preventing use of malloc

//-----------------------------------------------------------------------------
//-----------------------------------------------------------------------------
// CHANGES ABOVE THIS LINE ARE NOT ALLOWED (except for testing)
//-----------------------------------------------------------------------------
//-----------------------------------------------------------------------------

template<typename T>
class Container
{
public:
    Container(char* pBuffer, unsigned bufferSize);
    ~Container();

    T* Add();
    void Remove(T* pElement);

    unsigned Count() const;
    unsigned Capacity() const;

    bool IsEmpty() const;
    bool IsFull() const;

    T const* operator [] (unsigned index) const;
    T* operator [] (unsigned index);

    void Sort(int (*Compare)(T const* pX, T const* pY));

    //Bookkeeping member variables
private:
    char* m_pBuffer;          //The Buffer
    unsigned m_bufferSize;    //BufferSize
    unsigned m_elementCount;  //How many elements in the container
    unsigned m_elementSize;   //Size of the Elements
};

// ----- write your code below
template<typename T>
Container<T>::Container(char* pBuffer, unsigned bufferSize) : m_pBuffer(pBuffer), m_bufferSize(bufferSize), m_elementCount(0), m_elementSize(sizeof(T))
{

}
template<typename T>
Container<T>::~Container()
{
    //Loop through each element in the list
    //Call their destructors.
    for (unsigned i = 0; i < m_elementCount; i++)
    {
        T* pElement = reinterpret_cast<T*>(m_pBuffer + i * m_elementSize);
        pElement->~T();
    }
    //Call delete on the now empty buffer.
    delete[] m_pBuffer;
    m_pBuffer = nullptr;

}
template<typename T>
T* Container<T>::Add()
{
    //Early out
    if (IsFull() || m_elementCount >= 65536)
    {
        printf("Container is full.");
        return nullptr;
    }
    for (unsigned i = 0; i < m_bufferSize / m_elementSize; i++)
    {
        T* pCurr = reinterpret_cast<T*>(m_pBuffer + i * m_elementSize); // Grab the Current one.
        if (pCurr->GetAddress() == 0) //Make sure this spot is empty
        {
            T* pElement = new (m_pBuffer + i * m_elementSize) T();
            ++m_elementCount; //Add one to the bookkeeping.
            return pElement;
        }

    }

    return nullptr;

}
template<typename T>
void Container<T>::Remove(T* pElement)
{
    //Some pointer math to get the index
    unsigned index = reinterpret_cast<char*>(pElement) - m_pBuffer;
    //convert that into an int

    index /= m_elementSize;

    T* pCurr = reinterpret_cast<T*>(m_pBuffer + index * m_elementSize); // Grab the Current one.
    //Call its deconstructor
    pCurr->~T();

    //Whoops, I guess I was thinking to far ahead. Not supposed to shift them
    //This would take care of Swiss Cheese, but it changes the address of the item.

      // //Loop through the container, starting at the spot we removed one.
      // //Start shifting them all to the left by one
      // for (unsigned i = index; i< m_elementCount; ++i) 
      // {
      //   T* pCurr = reinterpret_cast<T*>(m_pBuffer + i * m_elementSize); // Grab the Current one.
      //   T* pNext = reinterpret_cast<T*>(m_pBuffer + (i + 1) * m_elementSize); // Grab the Next one.
      //   *pCurr = *pNext;
      //   pNext->~T(); // Clean out the spot for the next one 
      // }

    --m_elementCount; //Book Keeping.

}
template<typename T>
unsigned Container<T>::Count() const
{
    return m_elementCount;
}
template<typename T>
unsigned Container<T>::Capacity() const
{
    return 65536;
}
template<typename T>
bool Container<T>::IsEmpty() const
{
    return m_elementCount == 0;
}
template<typename T>
bool Container<T>::IsFull() const
{
    return m_elementCount == Capacity();
}
template<typename T>
T const* Container<T>::operator [] (unsigned index) const
{
    //Looking for the Nth Object, wont always be at the Nth spot due to swiss cheese
    //Find the first empty spot and put the element in.
    unsigned newIndex = index;

    for (unsigned i = 0; i <= newIndex; i++)
    {
        T* pCurr = reinterpret_cast<T*>(m_pBuffer + i * m_elementSize); // Grab the Current one.
        if (pCurr->GetAddress() == 0) // This is an Empty Spot
            newIndex++;
    }

    return reinterpret_cast<T*>(m_pBuffer + newIndex * m_elementSize);
}

template<typename T>
T* Container<T>::operator [] (unsigned index)
{
    unsigned newIndex = index;

    for (unsigned i = 0; i <= newIndex; i++)
    {
        T* pCurr = reinterpret_cast<T*>(m_pBuffer + i * m_elementSize); // Grab the Current one.
        if (pCurr->GetAddress() == 0) // This is an Empty Spot
            newIndex++;
    }

    return reinterpret_cast<T*>(m_pBuffer + newIndex * m_elementSize);
}

template<typename T>
void Container<T>::Sort(int (*Compare)(T const* pX, T const* pY))
{
    //Bubble Sort.
    for (unsigned i = 0; i < m_elementCount; i++)
    {
        for (unsigned j = 0; j < m_elementCount - i - j; j++)
        {
            T* pElement1 = reinterpret_cast<T*>(m_pBuffer + j * m_elementSize);
            T* pElement2 = reinterpret_cast<T*>(m_pBuffer + (j + 1) * m_elementSize);

            if (Compare(pElement1, pElement2) > 0)
            {
                T temp = *pElement1; //T = the dereferenced (actual data) of Element 1
                *pElement1 = *pElement2;  // Set 1 to 2;
                *pElement2 = temp; // set 2 to the temp which was (1's data)
            }
        }
    }
}

//-----------------------------------------------------------------------------
//-----------------------------------------------------------------------------
// CHANGES BELOW THIS LINE ARE NOT ALLOWED (except for testing)
//-----------------------------------------------------------------------------
//-----------------------------------------------------------------------------

#pragma GCC diagnostic ignored "-Wmaybe-uninitialized"

//-----------------------------------------------------------------------------

// element's class to be stored in the Container
class TestClass
{
    size_t          m_addr;       // address of itself, to check address peristence
    unsigned        m_allocID;    // allocation ID
    static bool     ms_allowAdd;
    static unsigned ms_numDestructorCalls;
    static unsigned ms_numConstructorCalls;

public:
    TestClass()
    {
        // if this trips then either we're adding alements when not allowed
        // or a previous added element was not destructed properly
        assert(ms_allowAdd && (m_addr == 0) && (m_allocID == 0));
        m_addr = (size_t)this;
        m_allocID = 0;
        ++ms_numConstructorCalls;
    }

    ~TestClass()
    {
        // clears members, which are checked upon construction
        m_addr = 0;
        m_allocID = 0;
        ++ms_numDestructorCalls;
    }

    void SetAllocID(unsigned allocID)
    {
        m_allocID = allocID;
    }

    unsigned GetAllocID() const
    {
        return m_allocID;
    }

    size_t GetAddress() const
    {
        return m_addr;
    }

    static unsigned GetNumCtorCalls()
    {
        return ms_numConstructorCalls;
    }

    static unsigned GetNumDtorCalls()
    {
        return ms_numDestructorCalls;
    }

protected:
    static void AllowAdd(bool bAllowed)
    {
        ms_allowAdd = bAllowed;
    }

    friend void StressTest(Container<TestClass>& oContainer, unsigned const nNumAllocs);
};

bool TestClass::ms_allowAdd = false;
unsigned TestClass::ms_numDestructorCalls = 0;
unsigned TestClass::ms_numConstructorCalls = 0;

//-----------------------------------------------------------------------------

static unsigned const gs_bufferSize = 1024 * 1024;
static char gs_pBuffer[gs_bufferSize];

bool IsInBuffer(void const* pAddr)
{
    // returns true of the address passed in is the buffer passed to the container
    return ((char const*)pAddr >= (char const*)&gs_pBuffer[0]) && ((char const*)pAddr <= ((char const*)&gs_pBuffer[gs_bufferSize]));
}

//-----------------------------------------------------------------------------

int CompareTestClass(TestClass const* pX, TestClass const* pY)
{
    if (pX->GetAllocID() < pY->GetAllocID())
    {
        return -1;
    }
    else if (pX->GetAllocID() > pY->GetAllocID())
    {
        return 1;
    }

    return 0;
}

//-----------------------------------------------------------------------------

void StressTest(Container<TestClass>& oContainer, unsigned const nNumAllocs)
{
    int nSeed = 0;
    unsigned const nCapacity = oContainer.Capacity();

    unsigned nNballocs = 0;
    unsigned nNbFrees = 0;
    srand(nSeed);

    printf("Issuing %'d allocations\n", nNumAllocs);

    ///======  pre-fill up 3/4 of the container.
    TestClass::AllowAdd(true);
    for (unsigned i = 0; i < 3 * nCapacity / 4; ++i)
    {
        TestClass* pObj = oContainer.Add();
        pObj->SetAllocID(nNballocs);
        nNballocs++;
    }

    printf("Allocations: %'d \n", nNumAllocs);

    TestClass::AllowAdd(false);

    ///======  loop until we reach a certain number of allocations
    while (nNballocs < nNumAllocs)
    {
        ///======  randomly add or remove object from the managed container
        bool bAdd = ((rand() & 0x1f) >= 16) ? true : false;

        if (bAdd && oContainer.IsFull())
        {
            bAdd = false;
        }
        else if (!bAdd && oContainer.IsEmpty())
        {
            bAdd = true;
        }

        if (bAdd)
        {
            TestClass::AllowAdd(true);
            TestClass* pObj = oContainer.Add();
            TestClass::AllowAdd(false);

            assert(IsInBuffer(pObj));
            pObj->SetAllocID(nNballocs);
            assert(pObj->GetAddress() == (size_t)pObj);  ///======  sanity check
            nNballocs++;
        }
        else
        {
            int nIndex = (oContainer.Count() > 1) ? rand() % (oContainer.Count() - 1) : 0;

            TestClass* pObj = oContainer[nIndex];
            assert(pObj->GetAddress() == (size_t)pObj);  ///======  sanity check

            Container<TestClass> const& constContainer = oContainer;
            TestClass const* pConstObj = constContainer[nIndex];
            assert(pConstObj == pObj);  ///======  sanity check of const operator[]

            oContainer.Remove(pObj);
            assert(pObj->GetAddress() == 0);      ///======  if this assert trips then you haven't called the destructor.

            nNbFrees++;
        }

        if ((nNballocs & 32767) == 0)
        {
            printf("Container => %d allocs, %d frees\r", nNballocs, nNbFrees);
        }
    }
    printf("Container => %'d allocs, %'d frees\r", nNballocs, nNbFrees);

    // check that constructors and destructors calls
    assert(nNballocs == TestClass::GetNumCtorCalls());
    assert(nNbFrees == TestClass::GetNumDtorCalls());

    printf("\nTesting Sort: %'d elements\n", oContainer.Count());
    oContainer.Sort(CompareTestClass);  // sort by alloc ID

    ///======  sanity check after the sort
    for (unsigned i = 0; i < oContainer.Count() - 1; ++i)
    {
        unsigned const id0 = oContainer[i]->GetAllocID();
        unsigned const id1 = oContainer[i + 1]->GetAllocID();
        assert(id0 < id1);
    }

    printf("Clean up\n");

    ///======  clean up
    while (oContainer.Count() > 0)
    {
        TestClass* pObj = oContainer[0];
        assert(pObj->GetAddress() == (size_t)pObj);  ///======  sanity check
        oContainer.Remove(pObj);
    }
    assert(oContainer.Count() == 0);

    assert(TestClass::GetNumCtorCalls() == TestClass::GetNumDtorCalls());
}

//-----------------------------------------------------------------------------
// prevent usage of new/new[] and malloc
// Note that inplace new is not only allowed but also necessary

void* operator new(size_t size)
{
    assert(false);
    return (void*)-1;
}

void* operator new[](size_t size)
{
    assert(false);
    return (void*)-1;
}

void* nomalloc(size_t size)
{
    assert(false);
    return nullptr;
}

//-----------------------------------------------------------------------------

int main(int, char* [])
{
    setlocale(LC_NUMERIC, "");
    assert(sizeof(Container<TestClass>) < 512);  ///======  this is an arbitrary, but why should we need more?

    memset(gs_pBuffer, 0, gs_bufferSize);
    Container<TestClass> container(gs_pBuffer, gs_bufferSize);
    assert(container.Capacity() <= 65536);

    printf("Managed Container Capacity: %'d\n", container.Capacity());

    clock_t oStartTime = clock();
    StressTest(container, 20000000);
    clock_t oEndTime = clock();

    double fElapsedTime = (double)(oEndTime - oStartTime) / CLOCKS_PER_SEC;

    printf("\nTime elapsed: %f seconds\n", fElapsedTime);

    return EXIT_SUCCESS;
}
