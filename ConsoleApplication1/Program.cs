using System;

namespace ConsoleApplication1
{
    class Matrix
    {
        //Initalized Variables
        private int[,] grid = null;
        private int iterator = 0;

        //Constuctor
        public Matrix(int columnLength, int rowLength)
        {
            if (columnLength > 0 && rowLength > 0)
            {
                grid = new int[columnLength, rowLength];
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }

        //Overloading Operations
        public static Matrix operator +(Matrix lhs, Matrix rhs)
        {
            if (lhs.grid.GetLength(0) != rhs.grid.GetLength(0))
            {
                throw new InvalidOperationException();
            }
            else
            {
                if (lhs.grid.GetLength(1) != rhs.grid.GetLength(1))
                {
                    throw new InvalidOperationException();
                }
                else
                {
                    Matrix returnMatrix = new Matrix(lhs.grid.GetLength(0), lhs.grid.GetLength(1));
                    for (int i = 0; i < returnMatrix.grid.GetLength(1); i++)
                    {
                        for (int j = 0; j < returnMatrix.grid.GetLength(0); j++)
                        {
                            returnMatrix.grid[j, i] = lhs.grid[j, i] + rhs.grid[j, i];
                        }
                    }
                    return returnMatrix;
                }
            }
        }

        //Methods
        public int getCurrent()
        {
            int rowLength = grid.GetLength(0);
            int column = iterator / rowLength;   //we can find what column the iterator is on by dividing it by rowLength
            return grid[iterator % rowLength, column]; //we can find the row position by taking the modulus of iterator with rowLength
        }
        public int getFirst()
        {
            Iterator = 0;
            return getCurrent();
        }
        public int getLast()
        {
            Iterator = grid.Length - 1;
            return getCurrent();
        }
        public int getNext()
        {
            Iterator = ++iterator;
            return getCurrent();
        }
        public int gerPrev()
        {
            Iterator = --iterator;
            return getCurrent();
        }
        public bool beginning()
        {
            if (iterator == 0)
            {
                return true;
            }
            {
                return false;
            }
        }
        public bool end()
        {
            if (iterator == (grid.Length - 1))
            {
                return true;
            }
            return false;
        }
        public int[] list()  //returns all values in the Matrix in the form of an Array
        {
            int[] listArray = new int[grid.Length];
            int lastValue = grid.Length - 1;
            getFirst();
            while (Iterator < lastValue)
            {
                listArray[Iterator] = getCurrent();
                getNext();
            }
            listArray[lastValue] = getLast();
            return listArray;
        }
        public void fill(int value) //Fills whole Grid with a value
        {
            getLast();
            int lastValue = grid.Length - 1;
            getFirst();
            while (Iterator < lastValue)
            {
                setValue(value);
                getNext();
            }
            setValue(value);


        }
        public void setValue(int value) //Places value at iterator's position
        {
            int rowNumber = iterator / grid.GetLength(0);
            grid[iterator % grid.GetLength(0), rowNumber] = value;
        }

        //Conversion Methods from String to Int
        private bool validString(ref string data)
        {
            //Initalized only characters we want to read
            char[] allowedChars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ' ' };

            //Checks every char data has, if it's not an allowed char validString returns false immediately 
            for (int i = 0; i < data.Length; i++)
            {
                bool checkForUnallowedChar = false;
                for (int j = 0; j < allowedChars.Length; j++)
                {
                    if (data[i] == allowedChars[j])
                    {
                        checkForUnallowedChar = true;
                    }
                }

                if (checkForUnallowedChar == false)
                {
                    return false;
                }
            }
            return true;
        }
        public bool readInData(string data)
        {
            if (validString(ref data) == true)
            {
                string[] DataSegments = data.Split();
                if(DataSegments.Length == grid.Length)
                {
                    getFirst();
                    for (int i = 0; i < DataSegments.Length; i++)
                    {
                        setValue(int.Parse(DataSegments[i]));
                        getNext();
                    }
                    return true;
                }
            }
            return false;
        }

        //Properties
        public int Iterator
        {
            get
            {
                return iterator;
            }
            set
            {
                if(value >= grid.Length)
                {
                    iterator = grid.Length - 1;
                }
                else if(value < 0)
                {
                    iterator = 0;
                }
                else
                {
                    iterator = value;
                }
            }
        }
        public int Columns
        {
            get
            {
               return grid.GetLength(0);
            }
        }
        public int Rows
        {
            get
            {
                return grid.GetLength(1);
            }
        }
    }
    

    class Program
    {
        static void Main(string[] args)
        {
            bool applicationRunning = true;
            while(applicationRunning == true)
            {
                applicationRunning = StartApplication();
            }
        }
        public static bool StartApplication()
        {
            bool acceptedInput = true;
            int userInput = 0;
            int columnLength = 0;
            int rowLength = 0;

            do
            {
                Console.WriteLine("Please input the Column length");
                if (!int.TryParse(Console.ReadLine(), out userInput))   //Checks for a valid int for columnLength
                {
                    acceptedInput = false;
                    Console.WriteLine("That was not a valid input, please input an integer value");
                }
                else
                {
                    do
                    {
                        columnLength = userInput;
                        Console.WriteLine("Please input the Row Length");
                        if (!int.TryParse(Console.ReadLine(), out userInput))   //Checks for a valid int for rowLength
                        {
                            acceptedInput = false;
                            Console.WriteLine("That was not a valid input, please input an integer value");
                        }
                        else
                        {
                            acceptedInput = true;
                            rowLength = userInput;
                            Matrix matrixOne = new Matrix(columnLength, rowLength);
                            Console.WriteLine("Builing Matrix I");
                            Build(ref matrixOne);
                            displayMatrix(matrixOne);
                            Console.WriteLine("Building Matrix II");
                            Matrix matrixTwo = new Matrix(columnLength, rowLength);
                            Build(ref matrixTwo);
                            displayMatrix(matrixTwo);

                            Matrix matrixLast = matrixTwo + matrixOne;
                            displayMatrix(matrixLast);
                        }
                    }
                    while (acceptedInput == false);
                }
            }
            while (acceptedInput == false);

            Console.WriteLine("Would you like to restart? [Y = 0/N = 1]");
            do
            {
                if(int.TryParse(Console.ReadLine(), out userInput))
                {
                    if(userInput == 1)
                    {
                        return false;
                    }
                    else if (userInput == 0)
                    {
                        return true;
                    }
                    else
                    {
                        acceptedInput = false;
                    }
                }
                else
                {
                    acceptedInput = false;
                }
            } while (acceptedInput == false);
            return false;
        }

        public static void Build(ref Matrix obj)
        {
            obj.getFirst();
            bool acceptedInput = true;
            do
            {
                if (obj.readInData(Console.ReadLine()))
                {
                    acceptedInput = true;
                }
                else
                {
                    acceptedInput = false;
                    Console.WriteLine("Please input a list of integers");
                }
            }
            while (acceptedInput == false);
        }
        public static void displayMatrix(Matrix obj)
        {
            int[] gridArray = obj.list();
            for (int i = 0; i < gridArray.Length; i++)
            {
                if ((i % obj.Rows) == 0)
                {
                    Console.WriteLine();
                }
                Console.Write(gridArray[i]);
            }
            Console.WriteLine();
        }
    }
}

