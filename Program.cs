using System;

namespace Battle_Tank_Game
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("-----------------------------------------------------------------------");
            Console.WriteLine("                  Battle Ship Game by Cinta Maharany                 ");
            Console.WriteLine("                      Teknik Informatika 2021                      ");
            Console.WriteLine("-----------------------------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("...You have to find and destroy all the ship for maintan your area...");
            Console.WriteLine("            ...All for one, and one for all!!!...            ");
            Console.WriteLine("                       Let's get srarted                       ");
            
           //bikin variable yamg dibutuhkan
           int dessertLength = 5;
           char tank = 't';
           char sand ='~';
           char hit ='X';
           char miss = '0';
           int tankTotal = 3;

           //Buat Array 2D untuk penempatan tank
           char[,] dessert = createDessert(dessertLength, sand, tank, tankTotal);

            //print array 2D ke console 
            printDessert(dessert, sand, tank);

            //Jumlah tank yang tersembunyi
            int unknownTankDetected = tankTotal;

           //Game play
           while (unknownTankDetected > 0) {
               int[] guessCoordinates = getUserCoordinate(dessertLength);
               char locationViewUpdate = verifyGuessAndTarget(guessCoordinates, dessert, tank, sand, hit, miss);
               if (locationViewUpdate == hit) {
                   unknownTankDetected--;
               }
               dessert = updateDessert(dessert, guessCoordinates, locationViewUpdate);
               printDessert(dessert, sand, tank);
           }
           Console.WriteLine("Game Over, Goodbye...");

        }

            //print array 2D ke layar console
            private static void printDessert(char[,] dessert, char sand, char tank)
            {
            
                Console.Write("  ");
                for (int i = 0; i < 5; i++) {
                    Console.Write(i + 1 + " ");
                }
                Console.WriteLine();

                for (int row = 0; row < 5; row++){
                    Console.Write(row + 1 + " ");

                    for(int column = 0; column < 5; column++) {
                        char position = dessert[row, column];
                        if (position == tank) {
                            Console.Write(position + " ");
                        }
                    }
                    Console.WriteLine();
                }
            }

            //cek validasi tebakan user (miss/kena/sudah hancur)
            private static char verifyGuessAndTarget(int[] guessCoordinates, char[,] dessert, char tank, char sand, char hit, char miss)
            {
                string message;
                int row = guessCoordinates[0];
                int column = guessCoordinates[1];
                char target = dessert [row, column];

                if (target == tank) {
                    message = "TANK SABOTAGED!!!...";
                    target = hit;
                }else if (target == sand ) {
                    message = "MISS OUT...";
                    target = miss;
                }else {
                    message = "YOU PAST OFF!!!...";
                }
                Console.WriteLine(message);
                return target;
            }
            
            //update tampilan array 2D berdasarkan hasil tebakan user 
            private static char[,] updateDessert(char[,] dessert, int[] guessCoordinates, char locationViewUpdate)
            {
                int row = guessCoordinates[0];
                int column = guessCoordinates[1];
                dessert[row, column] = locationViewUpdate;
                return dessert;
            }

            //Tebakan User (UserInput)\
            private static int[] getUserCoordinate(int dessertLenght)
            {
                int row;
                int column;

                do{
                    Console.Write("Pilih Baris : ");
                    row = Convert.ToInt32(Console.ReadLine());
                } while (row < 0 || row > dessertLenght + 1);
                do{
                    Console.Write("Pilih Kolom : ");
                    column = Convert.ToInt32(Console.ReadLine());
                } while (column < 0 || column > dessertLenght + 1);

                return new []{row - 1, column - 1};
            }

            //Buat Array 2D
            private static char[,] createDessert(int dessertLength, char sand, char tank, int tankTotal)
            {
                char[,] dessert = new char[dessertLength, dessertLength];

                for (int row = 0; row < dessertLength; row++){
                    for (int col = 0; col < dessertLength; col++){
                        dessert[row, col] = sand;
                    }
                }
                return placeTank(dessert, tankTotal, sand, tank);
            }

            //Meletakan 3 buah tank di dalam Array 2D
            private static char[,] placeTank(char[,] dessert, int tankTotal, char sand, char tank)
            {
                int placedTanks = 0;
                int dessertLenght = 5;

                while (placedTanks < tankTotal) 
                {
                    int[] tankLocation = generateTankCoordinate(dessertLenght);
                    char positionOK =  dessert[tankLocation[0], tankLocation[1]];

                    if(positionOK == sand){
                        dessert[tankLocation[0], tankLocation[1]] = tank;
                        placedTanks++;
                    }
                }
                return dessert;
            }

            //Generate Random position di dalam aaray 2D
            private static int[] generateTankCoordinate(int dessertLenght)
            {
                Random rnd = new Random ();
                int[] coordinates = new int[2];
                for (int a = 0; a < coordinates.Length; a++) 
                {
                    coordinates[a] = rnd.Next(dessertLenght);
                }
                return coordinates;
            }
         }
    } 

