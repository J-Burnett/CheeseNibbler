using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheeseNibbler
{
    class Program
    {
        static void Main(string[] args)
        {
            //create instance of the game
            CheeseNibbler game = new CheeseNibbler();
            game.PlayGame();

            //keeps console open
            Console.ReadKey();
        }
    }

    public class Point
    {
        public enum PointStatus
        {
            Empty, Cheese, Mouse
        }

        //X coordinate value
        public int X {get; set;}
        //Y coordinate value
        public int Y{get;set;}
        //Checks for mouse and cheese
        public PointStatus Status { get; set; }

        /// <summary>
        /// Sets the coordinates of the cell and sets the status to empty
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>

        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
            this.Status = PointStatus.Empty;
        }
    }

    /// <summary>
    /// Main point of the game play
    /// </summary>
    public class CheeseNibbler
    {
        //the grid itself
        public Point[,] Grid { get; set; }
        //represents the mouse
        public Point Mouse { get; set; }
        //represents the cheese
        public Point Cheese { get; set; }
        //holds what round is taking place
        public int Round { get; set; }
        
        //Default constructor
        public CheeseNibbler()
        {
            Random rng = new Random();
           
            //initialize the grid
            this.Grid = new Point[10,10];
            this.Round = 1;
       
            //loop through every x, y value in grid
            for(int y = 0; y < 10; y++)
            {
                for(int x = 0; x < 10; x++)
                {
                    //setting a new Point, into each
                    //coordinate of the grid
                    this.Grid[x, y] = new Point(x, y);
                }
            }

            //initialize the mouse and randomly place it on the grid
            int mouseXCoordinate = rng.Next(0, 10);
            int mouseYCooridnate = rng.Next(0, 10);

            //setting Mouse to random coordinates
            this.Mouse = Grid[mouseXCoordinate, mouseYCooridnate];
            this.Mouse.Status = Point.PointStatus.Mouse;

            //initialize the cheese and randomly place it on the grid, making sure that it 
            //does not occupay the same spot as the mouse


            do
            {
                //Create random cheese coordinates to check
                int cheeseXCoordinate = rng.Next(10);
                int cheeseYCoordinate = rng.Next(10);
                this.Cheese = Grid[cheeseXCoordinate, cheeseYCoordinate];
            }
            //Condition to be met to place the cheese on the grid
            while (this.Cheese.Status != Point.PointStatus.Empty);

            //adds cheese to grid with random coordinates
            this.Cheese.Status = Point.PointStatus.Cheese;
        }

        /// <summary>
        /// Displays the grid to the console
        /// </summary>
        public void DrawGrid()
        {
            //Clear the console
            Console.Clear();

            //loop through every point on grid
            for(int y = 0; y < 10; y++)
            {
                for(int x = 0; x < 10; x++)
                {
                    //check the type of each point
                    switch(Grid[x,y].Status)
                    {
                        case Point.PointStatus.Mouse:
                            Console.Write("[   m  ]");
                            break;
                        case Point.PointStatus.Cheese:
                            Console.Write("[   c  ]");
                            break;
                        default:
                            Console.Write("[      ]");
                            break;
                    }
                }
                Console.WriteLine();
            }
        }

        public ConsoleKey GetUserMove()
        {
            Console.WriteLine("Use the numpad to move the mouse!");

            //putting a single key stroke into a variable
            //(true) keeps the character from being written to the console
            ConsoleKey playerInput = Console.ReadKey(true).Key;

            bool validInput = false;
            while(!validInput)
            {
                //checks for valid input
                switch (playerInput)
                {
                    case ConsoleKey.NumPad4://left
                    case ConsoleKey.NumPad6://right
                    case ConsoleKey.NumPad8://up
                    case ConsoleKey.NumPad2://down
                    case ConsoleKey.NumPad7://upper left
                    case ConsoleKey.NumPad9://upper right
                    case ConsoleKey.NumPad1://lower left
                    case ConsoleKey.NumPad3://lower right
                        validInput = true;
                        return playerInput;
                    default:
                        //invalid
                        Console.WriteLine("\nNot a valid move");
                        Console.WriteLine("Use the numpad to move the mouse");
                        playerInput = Console.ReadKey(true).Key;
                        return playerInput;
                }
            }
            return playerInput;
        }

        /// <summary>
        /// A function that checks if a the user input is a valid move
        /// </summary>
        /// <param name="playerInput">Player's move</param>
        /// <returns></returns>
        public bool ValidMove(ConsoleKey playerInput)
        {
            //look at each type of keypress
            switch(playerInput)
            {
                //checks if the move is on the grid
                case ConsoleKey.NumPad4:
                    return this.Mouse.X > 0;//left
                case ConsoleKey.NumPad6:
                    return this.Mouse.X < 9;//right
                case ConsoleKey.NumPad2:
                    return this.Mouse.Y < 9;//down
                case ConsoleKey.NumPad8:
                    return this.Mouse.Y > 0;//up
                case ConsoleKey.NumPad7:
                    return this.Mouse.Y > 0 && this.Mouse.X > 0;//upper left
                case ConsoleKey.NumPad9:
                    return this.Mouse.Y > 0 && this.Mouse.X < 9;//upper right
                case ConsoleKey.NumPad1:
                    return this.Mouse.Y < 9 && this.Mouse.X > 0;//down left
                case ConsoleKey.NumPad3:
                    return this.Mouse.Y < 9 && this.Mouse.X < 9;//down right
                default:
                    break;
            }
            return false;
        }

        public bool MoveMouse(ConsoleKey playerInput)
        {
            int previousX = this.Mouse.X;
            int previousY = this.Mouse.Y;

            if(ValidMove(playerInput))
            {
                switch (playerInput)
                {
                    case ConsoleKey.NumPad4://left
                        Mouse.X -= 1;
                        break;
                    case ConsoleKey.NumPad6://right
                        Mouse.X += 1;
                        break;
                    case ConsoleKey.NumPad2://down
                        Mouse.Y += 1;
                        break;
                    case ConsoleKey.NumPad8://up
                        Mouse.Y -= 1;
                        break;
                    case ConsoleKey.NumPad7://upper left
                        Mouse.X -= 1;
                        Mouse.Y -= 1;
                        break;
                    case ConsoleKey.NumPad9://upper right
                        Mouse.X += 1;
                        Mouse.Y -= 1;
                        break;
                    case ConsoleKey.NumPad1://down left
                        Mouse.X -= 1;
                        Mouse.Y += 1;
                        break;
                    case ConsoleKey.NumPad3://down right
                        Mouse.X += 1;
                        Mouse.Y += 1;
                        break;
                    default:
                        break;
                }
            }
            Point checkForCheese = this.Grid[Mouse.X, Mouse.Y];

            if (checkForCheese.Status == Point.PointStatus.Cheese)
            {
                this.Grid[previousX, previousY].Status = Point.PointStatus.Empty;
                this.Grid[Mouse.X, Mouse.Y].Status = Point.PointStatus.Mouse;
                return true;
            }
            else
            {
                this.Grid[previousX, previousY].Status = Point.PointStatus.Empty;
                this.Grid[Mouse.X, Mouse.Y].Status = Point.PointStatus.Mouse;
                return false;
            }
        }

        public void PlayGame()
        {
            //end condition
            bool hasFoundCheese = false;

            //play game while cheese has not been found
            while (!hasFoundCheese)
            {
                //Draw the grid
                DrawGrid();
                //Get valid user input
                ConsoleKey playerMove = GetUserMove();
                //move the mouse and determine
                //if the game has ended
                hasFoundCheese = MoveMouse(playerMove);
                this.Round++;//increment the round counter
            }

            this.DrawGrid();
            Console.WriteLine("You found the cheese in {0} rounds", this.Round);
        }
    }
}
