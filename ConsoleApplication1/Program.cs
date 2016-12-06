﻿using System;

namespace ConsoleApplication1
{
    class Matrix
    {
        //Initalized Variables
        private int [,] grid = null;
        private int iterator = 0;

        //Constuctor
        public Matrix(int columnLength, int rowLength)
        {
            grid = new int[columnLength, rowLength];
        }
        
        //Overloading Operations
        public static Matrix operator+(Matrix lhs, Matrix rhs)
        {
            if(lhs.Grid.GetLength(0) != rhs.Grid.GetLength(0))
            {
                throw new InvalidOperationException();
            }
            else
            {
                if(lhs.Grid.GetLength(1) != rhs.Grid.GetLength(1))
                {
                    throw new InvalidOperationException();
                }
                else
                {
                    Matrix returnMatrix = new Matrix(lhs.Grid.GetLength(0),lhs.Grid.GetLength(1));
                    for(int i = 0; i < returnMatrix.Grid.GetLength(1); i++)
                    {
                        for(int j = 0; j < returnMatrix.Grid.GetLength(0); j++)
                        {
                            returnMatrix.Grid[j, i] = lhs.Grid[j, i] + rhs.Grid[j,i];
                        }
                    }
                    return returnMatrix;
                }
            }
        }
        
        //Methods
        public int getFirst()
        {
            Iterator = 0;
            return Iterator;
        }
        public int getLast()
        {
            Iterator = Grid.Length;
            return Iterator;
        }
        public int getNext()
        {
            Iterator = ++iterator;
            return Iterator;
        }
        public int gerPrev()
        {
            Iterator = --iterator;
            return Iterator;
        }
        public void List()  //Lists all the values in grid to Console
        {
            for (int i = 0; i < grid.GetLength(1); i++)
            {
                for (int j = 0; j < grid.GetLength(0); j++)
                {
                    Console.Write(grid[j, i]);
                    Console.Write(" ");
                }
                Console.WriteLine("");
            }
        }
        public void Fill(int Pos, int Length, int Value)    //Fills a selected area with a selected value, 
                                                            //if the length of the Fill exceeds a row length then it will continue on the following row
        {
            if (Pos + Length > Grid.Length)
            {
                throw new IndexOutOfRangeException();
            }
            else
            {
                if(Pos < 0)
                {
                    throw new IndexOutOfRangeException();
                }
                else
                {
                    int rowNumber = Pos / Grid.GetLength(0);
                    for(int i = 0; i < Length; i++)
                    {
                        if (i + Pos > Grid.GetLength(0))
                        {
                            Pos = 0;
                        }
                        Grid[i + Pos, rowNumber] = Value;
                    }
                }
            }
        }
        public void Build( ) //One by one the Grid will set values for each block
        {
            bool buildFinsished = true;
            bool acceptedInput = true;
            do
            {
                for (int i = 0; i < Grid.GetLength(1); i++)
                {
                    for (int j = 0; j < Grid.GetLength(0); j++)
                    {
                        int userInput = 0;
                        do
                        {
                            Console.Write("Current Pos [");
                            Console.Write(j);
                            Console.Write(", ");
                            Console.Write(i);
                            Console.Write("]");
                            Console.WriteLine();
                            if (!int.TryParse(Console.ReadLine(), out userInput))
                            {
                                acceptedInput = false;
                                Console.WriteLine("That was not a valid input, please input an integer value");
                            }
                            else
                            {
                                acceptedInput = true;
                            }
                        }
                        while (acceptedInput == false);

                        Grid[j, i] = userInput;
                        List();
                    }
                }

                do
                {
                    Console.WriteLine("Save Grid? [Y/N]");
                    ConsoleKeyInfo keyID = Console.ReadKey();
                    if (keyID.Key == ConsoleKey.Y)
                    {
                        buildFinsished = true;
                    }
                    else if (keyID.Key == ConsoleKey.N)
                    {
                        buildFinsished = false;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("That was not an accepted input");
                    }
                } while (acceptedInput == false);
                Console.WriteLine();
            }
            while (buildFinsished == false);
          
        }


        //Properties
        private int[,] Grid
        {
            get
            {
                return grid;
            }
            set
            {
                grid = value;
            }
        }
        public int Iterator
        {
            get
            {
                int rowNumber = iterator / Grid.GetLength(0);
                return Grid[iterator % Grid.GetLength(0), rowNumber];
            }
            set
            {
                if(value > Grid.Length)
                {
                    iterator = Grid.Length;
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
                if (!int.TryParse(Console.ReadLine(), out userInput)) //Checks for a valid int
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
                        if (!int.TryParse(Console.ReadLine(), out userInput))
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
                            matrixOne.Build();
                            Console.WriteLine("Building Matrix II");
                            Matrix matrixTwo = new Matrix(columnLength, rowLength);
                            matrixTwo.Build();

                            Matrix matrixLast = matrixTwo + matrixOne;
                            matrixLast.List();
                        }
                    }
                    while (acceptedInput == false);
                }
            }
            while (acceptedInput == false);
            return true;
        }
    }
}

