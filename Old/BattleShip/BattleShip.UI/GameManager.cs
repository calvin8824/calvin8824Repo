using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;
using BattleShip.BLL.Ships;

namespace BattleShip.UI
{
    class GameManager
    {
        private int playerPlacedShips = 0;
        public void Run()
        {
            Player p1 = new Player();
            Player p2 = new Player();

            Welcome();

            p1.Name = GetName("Player One, enter your name ");
            Console.Clear();

            p1.FirstMate = GetName("Player One, enter your first mate's name ");
            Console.Clear();

            p2.Name = GetName("Player two, enter your name: ");
            Console.Clear();

            p2.FirstMate = GetName("Player two, enter your first mate's name: ");
            Console.Clear();

            //Places into p2's board.
            Console.WriteLine($"{p1.FirstMate}: Admiral {p1.Name}, place your ships.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            PlaceShips(p2);

            //Places into p1's board.
            Console.Clear();
            Console.WriteLine($"{p2.FirstMate}: Admiral {p2.Name}, place your ships.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            PlaceShips(p1);

            //Sets a turn counter. May be hidden.
            //Tracks turn and maybe winner.
            int turn = 0;
            //Tracks the most recent shot result to check for victory.
            ShotStatus shotStatus = ShotStatus.Invalid;

            //Picks first player
            Random r = new Random();
            turn = r.Next(2);

            BeginScreen();

            //Clears for random player.
            if (turn % 2 == 0)
            {
                Console.Clear();
                Console.WriteLine($"{p2.FirstMate}: Admiral {p2.Name}'s turn.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"{p1.FirstMate}: Admiral {p1.Name}'s turn.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();                
            }

            do
            {
                if (turn % 2 == 0)
                {
                    shotStatus = FiringSolution(p1);

                    if (shotStatus != ShotStatus.Victory)
                    {
                        Console.Clear();
                        Console.WriteLine($"{p2.FirstMate}: Admiral {p2.Name}'s turn.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    }
                }
                else
                {
                    shotStatus = FiringSolution(p2);
                    
                    if (shotStatus != ShotStatus.Victory)
                    {
                        Console.Clear();
                        Console.WriteLine($"{p1.FirstMate}: Admiral {p1.Name}'s turn.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    }
                }
                turn++;
            } while (shotStatus != ShotStatus.Victory);

            if (turn % 2 == 1)
            {
                Victory(p1);
            }
            else
            {
                Victory(p2);
            }
        }
        
        //Displays welcome screen. 
        private void Welcome()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(@" #     #                                                           
 #  #  # ###### #       ####   ####  #    # ######    #####  ####  
 #  #  # #      #      #    # #    # ##  ## #           #   #    # 
 #  #  # #####  #      #      #    # # ## # #####       #   #    # 
 #  #  # #      #      #      #    # #    # #           #   #    # 
 #  #  # #      #      #    # #    # #    # #           #   #    # 
  ## ##  ###### ######  ####   ####  #    # ######      #    ####  
                                                                   
 ######                                    #####                   
 #     #   ##   ##### ##### #      ###### #     # #    # # #####   
 #     #  #  #    #     #   #      #      #       #    # # #    #  
 ######  #    #   #     #   #      #####   #####  ###### # #    #  
 #     # ######   #     #   #      #            # #    # # #####   
 #     # #    #   #     #   #      #      #     # #    # # #       
 ######  #    #   #     #   ###### ######  #####  #    # # #       
                                                                ");
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        //Displays welcome screen.
        private void BeginScreen()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(@"
 ######                                   
 #     #   ##   ##### ##### #      ###### 
 #     #  #  #    #     #   #      #      
 ######  #    #   #     #   #      #####  
 #     # ######   #     #   #      #      
 #     # #    #   #     #   #      #      
 ######  #    #   #     #   ###### ###### 
                                          
          ###       ###       ###         
          ###       ###       ###         
          ###       ###       ###         
           #         #         #          
                                          
          ###       ###       ###         
          ###       ###       ###         
                                          
");
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        //Reads player names.
        private string GetName(string prompt)
        {
            string name = null;

            Console.WriteLine(prompt);
            bool valid = false;
            do
            {
                name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Please enter a valid name: ");
                }
                else
                {
                    name = name.Trim();
                    valid = true;
                }
            } while (!valid);

            return name;
        }

        //Places ships onto the selected player's board.
        private void PlaceShips(Player player)
        {
            ShipDirection orientation = ShipDirection.Down;
            ShipPlacement shipPlacement = ShipPlacement.NotEnoughSpace;
            
            //Requires the player to place each ship, in order, one at a time.
            do
            {
                string prompt = "Place your destroyer. (Size: 2)";
                var request = new PlaceShipRequest
                {
                    Coordinate = RequestCoordinate(player, prompt, 2, out orientation),
                    Direction = orientation,
                    ShipType = ShipType.Destroyer
                };
                shipPlacement = player.Board.PlaceShip(request);

                if (shipPlacement != ShipPlacement.Ok)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Warning: You cannot move there.");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            } while (shipPlacement != ShipPlacement.Ok);
            playerPlacedShips++;

            //Requires the player to place each ship, in order, one at a time.
            do
            {
                string prompt = "Place your cruiser. (Size: 3)";
                var request = new PlaceShipRequest
                {
                    Coordinate = RequestCoordinate(player, prompt, 3, out orientation),
                    Direction = orientation,
                    ShipType = ShipType.Cruiser
                };
                shipPlacement = player.Board.PlaceShip(request);

                if (shipPlacement != ShipPlacement.Ok)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Warning: You cannot move there.");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            } while (shipPlacement != ShipPlacement.Ok);
            playerPlacedShips++;

            //Requires the player to place each ship, in order, one at a time.
            do
            {
                string prompt = "Place your submarine. (Size: 3)";
                var request = new PlaceShipRequest
                {
                    Coordinate = RequestCoordinate(player, prompt, 3, out orientation),
                    Direction = orientation,
                    ShipType = ShipType.Submarine
                };
                shipPlacement = player.Board.PlaceShip(request);

                if (shipPlacement != ShipPlacement.Ok)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Warning: You cannot move there.");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            } while (shipPlacement != ShipPlacement.Ok);
            playerPlacedShips++;

            //Requires the player to place each ship, in order, one at a time.
            do
            {
                string prompt = "Place your battleship. (Size: 4)";
                var request = new PlaceShipRequest
                {
                    Coordinate = RequestCoordinate(player, prompt, 4, out orientation),
                    Direction = orientation,
                    ShipType = ShipType.Battleship
                };
                shipPlacement = player.Board.PlaceShip(request);

                if (shipPlacement != ShipPlacement.Ok)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Warning: You cannot move there.");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            } while (shipPlacement != ShipPlacement.Ok);
            playerPlacedShips++;

            //Requires the player to place each ship, in order, one at a time.
            do
            {
                string prompt = "Place your carrier. (Size: 5)";
                var request = new PlaceShipRequest
                {
                    Coordinate = RequestCoordinate(player, prompt, 5, out orientation),
                    Direction = orientation,
                    ShipType = ShipType.Carrier
                };
                shipPlacement = player.Board.PlaceShip(request);

                if(shipPlacement != ShipPlacement.Ok)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Warning: You cannot move there.");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            } while (shipPlacement != ShipPlacement.Ok);
            playerPlacedShips++;

            PlayerShipDisplay(player, "All ships placed.\nPress any key to continue...");
            Console.ReadKey();

            //Clears number of ships placed.
            playerPlacedShips = 0;

            //Hides the board
            Console.Clear();
        }

        //For display w/o player coordinate marker.
        private void PlayerShipDisplay(Player player, string prompt)
        {
            Console.Clear();

            Console.WriteLine("   | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10|");
            Console.WriteLine("--------------------------------------------");
            for(int i = 1; i < 11; i++)
            {
                Console.Write(" " + IndexToChar(i) + " ");
             
                for (int j = 1; j < 11; j++)
                {
                    Coordinate coord = new Coordinate(i, j);
                    if (CheckForShips(player, coord) == ShotStatus.Hit)
                    {
                        Console.Write("| ");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("# ");
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.Write("|   ");
                    }
                }
                Console.WriteLine("|");
                Console.WriteLine("--------------------------------------------");
            }
            Console.WriteLine(prompt);
        }

        //For use with player coordinates. Delete maybe...
        private void PlayerBattleDisplay(Player player, string prompt, Coordinate pos)
        {
            Console.Clear();

            Console.WriteLine("   | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10|");
            Console.WriteLine("--------------------------------------------");
            for (int i = 1; i < 11; i++)
            {
                Console.Write(" " + IndexToChar(i) + " ");

                for (int j = 1; j < 11; j++)
                {
                    Coordinate coord = new Coordinate(i, j);
                    if (coord.Equals(pos))
                    {
                        Console.Write("| ");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write("# ");
                        Console.ForegroundColor = ConsoleColor.Green;
                    }                    
                    else if (player.Board.CheckCoordinate(coord) == ShotHistory.Hit)
                    {
                        Console.Write("| ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("X ");
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else if (player.Board.CheckCoordinate(coord) == ShotHistory.Miss)
                    {
                        Console.Write("| ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("O ");
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.Write("|   ");
                    }
                }
                Console.WriteLine("|");
                Console.WriteLine("--------------------------------------------");
            }
            Console.WriteLine(prompt);
        }

        //For use with player coordinates and ship size.
        private void PlayerShipDisplay(Player player, string prompt, Coordinate pos, int size, ShipDirection orientation)
        {
            int[,] boardArray = new int[11,11];
            string orientationReminder = "";
            Console.Clear();

            for (int i = 1; i < 11; i++)
            {
                for (int j = 1; j < 11; j++)
                {
                    Coordinate coord = new Coordinate(i, j);

                    if (coord.Equals(pos))
                    {
                        boardArray[i, j] = 2;
                        if(orientation == ShipDirection.Left)
                        {
                            for (int k = 1; k < size; k++)
                            {
                                if(j - k > 0)
                                {
                                    boardArray[i, j - k] = 2;
                                }
                            }
                        }
                        else if (orientation == ShipDirection.Right)
                        {
                            for (int k = 1; k < size; k++)
                            {
                                if (j + k <= 10)
                                {
                                    boardArray[i, j + k] = 2;
                                }
                            }

                        }
                        else if (orientation == ShipDirection.Up)
                        {
                            for (int k = 1; k < size; k++)
                            {
                                if (i - k > 0)
                                {
                                    boardArray[i - k, j] = 2;
                                }
                            }

                        }
                        else if (orientation == ShipDirection.Down)
                        {
                            for (int k = 1; k < size; k++)
                            {
                                if (i + k <= 10)
                                {
                                    boardArray[i + k, j] = 2;
                                }
                            }

                        }
                        else
                        {
                            Console.WriteLine("Problem, see PlayerShipDisplay()");
                        }
                        
                    }
                    else if (CheckForShips(player, coord) == ShotStatus.Hit)
                    {
                        boardArray[i, j] = 1;
                    }
                    else if(boardArray[i, j] == 0)
                    {
                        boardArray[i,j] = 3;
                    }
                }
            }

            Console.WriteLine("   | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10|");
            Console.WriteLine("--------------------------------------------");
            for (int i = 1; i < 11; i++)
            {
                Console.Write(" " + IndexToChar(i) + " ");

                for (int j = 1; j < 11; j++)
                {                    
                    if (boardArray[i, j] == 2)
                    {
                        Console.Write("| ");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write("# ");
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else if (boardArray[i, j] == 1)
                    {
                        Console.Write("| ");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("# ");
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else if(boardArray[i, j] == 3)
                    {
                        Console.Write("|   ");
                    }
                    else
                    {
                        Console.WriteLine("Error: See PlayerShipDisplay()");
                    }
                }
                Console.WriteLine("|");
                Console.WriteLine("--------------------------------------------");
            }

            if (orientation == ShipDirection.Up)
            {
                orientationReminder = "Up";
            }
            else if (orientation == ShipDirection.Down)
            {
                orientationReminder = "Down";
            }
            else if (orientation == ShipDirection.Left)
            {
                orientationReminder = "Left";
            }
            else if (orientation == ShipDirection.Right)
            {
                orientationReminder = "Right";
            }
            else
            {
                Console.WriteLine("Problem, see PlayerShipDisplay()");
            }

            Console.WriteLine($"Orientation is: {orientationReminder}");
            Console.WriteLine(prompt);
        }
        
        //Marks all ships on board.
        private ShotStatus CheckForShips(Player player, Coordinate coordinate)
        {
            for (int i = 0; i < playerPlacedShips; i++)
            {
                if (player.Board.Ships[i].BoardPositions.Contains(coordinate))
                {
                    return ShotStatus.Hit;
                }
            }

            return ShotStatus.Miss;
        }

        //Converts the intex to the expected row-related char.
        private string IndexToChar(int j)
        {
            string tempChar = "";
            switch(j)
            {
                case 1:
                    tempChar = "A";
                    break;
                case 2:
                    tempChar = "B";
                    break;
                case 3:
                    tempChar = "C";
                    break;
                case 4:
                    tempChar = "D";
                    break;
                case 5:
                    tempChar = "E";
                    break;
                case 6:
                    tempChar = "F";
                    break;
                case 7:
                    tempChar = "G";
                    break;
                case 8:
                    tempChar = "H";
                    break;
                case 9:
                    tempChar = "I";
                    break;
                case 10:
                    tempChar = "J";
                    break;
                default:
                    tempChar = "Broken --> See IndexToChar()";
                    break;
            }

            return tempChar;
        }

        //Requests a direction from the player.
        //Redundant//Deprecated 
        private ShipDirection RequestOrientation(Player player, string prompt)
        {
            ShipDirection orientation = ShipDirection.Up;
            int selector = 0;
            bool valid = false;
            do
            {
                string stringDirection = null;

                PlayerShipDisplay(player, prompt);

                Console.WriteLine("Please choose an orientation for your ship.");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("(Hint: Select by pressing the space bar.)");
                Console.ForegroundColor = ConsoleColor.Green;

                switch (selector)
                {
                    case 0:
                        stringDirection = "Up";
                        orientation = ShipDirection.Up;
                        break;
                    case 1:
                        stringDirection = "Down";
                        orientation = ShipDirection.Down;
                        break;
                    case 2:
                        stringDirection = "Left";
                        orientation = ShipDirection.Left;
                        break;
                    case 3:
                        stringDirection = "Right";
                        orientation = ShipDirection.Right;
                        break;
                    default:
                        stringDirection = "Bad Direction --> See DirectionString()";
                        break;
                }

                Console.Write("Current Direction is: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"{stringDirection}");
                Console.ForegroundColor = ConsoleColor.Green;
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    valid = true;
                }

                //Increments the selector, but keeps it below 4.
                selector = ((selector + 1) % 4);                
            } while (!valid);

            return orientation;
        }

        //Has the user choose a coordinate to place their ship.
        private Coordinate RequestCoordinate(Player player,  string prompt, int size, out ShipDirection orientation)
        {
            int y = 5;
            int x = 5;
            int selector = 0;
            bool valid = false;
            ShipDirection lastOrientation = ShipDirection.Up;

            do
            {
                PlayerShipDisplay(player, prompt, new Coordinate(x, y), size, lastOrientation);
                Console.WriteLine("Select position with the arrow keys.\nChange orientation with spacebar.");
                Console.Write("Press Enter to select current position: ");
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                Console.WriteLine();

                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    if (x > 1)
                    {
                        if (CheckShipArea(x - 1, y, lastOrientation, size))
                        {
                            x--;
                        }
                    }
                    /*
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Warning: You cannot move there.");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    }
                    */
                }
                else if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    if (y > 1)
                    {
                        if (CheckShipArea(x, y - 1, lastOrientation, size))
                        {
                            y--;
                        }
                    }
                    /*
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Warning: You cannot move there.");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    }
                    */
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    if (x < 10)
                    {
                        if (CheckShipArea(x + 1, y, lastOrientation, size))
                        {
                            x++;
                        }
                    }
                    /*
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Warning: You cannot move there.");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    }
                    */
                }
                else if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    if (y < 10)
                    {
                        if (CheckShipArea(x, y + 1, lastOrientation, size))
                        {
                            y++;
                        }
                    }
                    /*
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Warning: You cannot move there.");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    }
                    */
                }
                else if (keyInfo.Key == ConsoleKey.Spacebar)
                {
                    do
                    {
                        if (CheckShipAreaBackEnd(x, y, lastOrientation, size))
                        {
                            selector = ((selector + 1) % 4);
                        }
                        else
                        {
                            if(lastOrientation == ShipDirection.Down)
                            {
                                while(!CheckShipAreaBackEnd(x, y, lastOrientation, size))
                                {
                                    x--;
                                }
                            }
                            else if (lastOrientation == ShipDirection.Right)
                            {
                                while (!CheckShipAreaBackEnd(x, y, lastOrientation, size))
                                {
                                    y--;
                                }
                            }
                            else if (lastOrientation == ShipDirection.Up)
                            {
                                while (!CheckShipAreaBackEnd(x, y, lastOrientation, size))
                                {
                                    x++;
                                }
                            }
                            else if (lastOrientation == ShipDirection.Left)
                            {
                                while (!CheckShipAreaBackEnd(x, y, lastOrientation, size))
                                {
                                    y++;
                                }
                            }
                            selector = ((selector + 1) % 4);
                        }
                        switch (selector)
                        {
                            case 0:
                                lastOrientation = ShipDirection.Up;
                                break;
                            case 1:
                                lastOrientation = ShipDirection.Down;
                                break;
                            case 2:
                                lastOrientation = ShipDirection.Left;
                                break;
                            case 3:
                                lastOrientation = ShipDirection.Right;
                                break;
                            default:
                                Console.WriteLine("Error: See RequestCoordinate()");
                                break;
                        }

                    } while (!CheckShipAreaBackEnd(x , y, lastOrientation, size));
                    
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    valid = true;
                }
                else
                {
                    Console.WriteLine("\nThat is not a valid move!");
                }
            } while (!valid);


            orientation = lastOrientation;
            return new Coordinate(x, y);
        }
        
        //Checks if the ship fits in the specified area.
        //Backend only.
        private bool CheckShipAreaBackEnd(int x, int y, ShipDirection orientation, int size)
        {
            bool doesFit = false;
            if (orientation == ShipDirection.Left)
            {
                if (y - size + 1 > 0)
                {
                    doesFit = true;
                }
            }
            else if (orientation == ShipDirection.Right)
            {
                if (y + size - 1 <= 10)
                {
                    doesFit = true;
                }
            }
            else if (orientation == ShipDirection.Up)
            {
                if (x - size + 1 > 0)
                {
                    doesFit = true;
                }
            }
            else if (orientation == ShipDirection.Down)
            {
                if (x + size - 1 <= 10)
                {
                    doesFit = true;
                }
            }

            return doesFit;
        }

        //Checks if the ship fits in the specified area.
        private bool CheckShipArea(int x, int y, ShipDirection orientation, int size)
        {
            bool doesFit = false;
            if (orientation == ShipDirection.Left)
            {   
                if (y - size + 1 > 0)
                {
                    doesFit = true;
                }
            }
            else if (orientation == ShipDirection.Right)
            {
                if (y + size - 1 <= 10)
                {
                    doesFit = true;
                }
            }
            else if (orientation == ShipDirection.Up)
            {
                if (x - size + 1> 0)
                {
                    doesFit = true;
                }
            }
            else if (orientation == ShipDirection.Down)
            {
                if (x + size - 1 <= 10)
                {
                    doesFit = true;
                }
            }

            /*
            if (!doesFit)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Warning: You cannot move there.");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            */
            return doesFit;
        }

        //Requests a coordinate from the player (for aiming).
        private Coordinate RequestCoordinate(Player player, string prompt)
        {
            int y = 5;
            int x = 5;
            bool valid = false;

            do
            {
                PlayerBattleDisplay(player, prompt, new Coordinate(x, y));
                Console.WriteLine("Select position with the arrow keys.");
                Console.Write("Press Enter to select current position: ");
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                Console.WriteLine();

                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    if (x > 1)
                    {
                        x--;
                    }
                    /*
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Warning: You cannot move there.");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    }
                    */
                }
                else if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    if (y > 1)
                    {
                        y--;
                    }
                    /*
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Warning: You cannot move there.");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    }
                    */
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    if (x < 10)
                    {
                        x++;
                    }
                    /*
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Warning: You cannot move there.");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    }
                    */
                }
                else if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    if (y < 10)
                    {
                        y++;
                    }
                    /*
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Warning: You cannot move there.");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    }
                    */
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    valid = true;
                }
                else
                {
                    Console.WriteLine("\nThat is not a valid move!");
                }
            } while (!valid);

            return new Coordinate(x, y);
        }
        
        //Works out player shots and whether or not they're valid.
        private ShotStatus FiringSolution(Player player)
        {
            Coordinate coord;
            FireShotResponse effect = new FireShotResponse();
            effect.ShotStatus = ShotStatus.Invalid;

            do
            { 
                coord = RequestCoordinate(player, $"{player.FirstMate}: Choose a firing solution: ");
                effect = player.Board.FireShot(coord);
            } while (effect.ShotStatus == ShotStatus.Invalid || effect.ShotStatus == ShotStatus.Duplicate);

            Console.WriteLine($"{player.FirstMate}: Firing solution plotted!!!");

            if(effect.ShotStatus == ShotStatus.Hit)
            {
                Console.WriteLine($"{player.FirstMate}: Fire mission successful. Target hit.");
            }
            else if (effect.ShotStatus == ShotStatus.HitAndSunk)
            {
                Console.WriteLine($"{player.FirstMate}: Fire mission successful. Target destroyed.");
            }
            else if (effect.ShotStatus == ShotStatus.Victory)
            {
                Console.WriteLine($"{player.FirstMate}: Mission successful. We've won.");
            }
            else if (effect.ShotStatus == ShotStatus.Miss)
            {
                Console.WriteLine($"{player.FirstMate}: Salvo off target. Missed.");
            }
            else
            {
                Console.WriteLine("Error: See FireSolution()");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            return effect.ShotStatus;
        }

        //Displays victory splash screen.
        private void Victory(Player player)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(@"
 #     #                                    
 #     # #  ####  #####  ####  #####  #   # 
 #     # # #    #   #   #    # #    #  # #  
 #     # # #        #   #    # #    #   #   
  #   #  # #        #   #    # #####    #   
   # #   # #    #   #   #    # #   #    #   
    #    #  ####    #    ####  #    #   #   
                                            
");

            Console.WriteLine($"\n{player.FirstMate}: Admiral {player.Name}, you've triumphed.\nPress any key to quit...");

            Console.ReadKey();
        }

    }
}
