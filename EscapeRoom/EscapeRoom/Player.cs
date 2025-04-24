
using EscapeRoom.Place;
using EscapeRoom.Inventory;
using EscapeRoom.Item;

namespace EscapeRoom.Player
{
    public class CPlayer
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int maxX;
        public int maxY;

        private CPlace place;


        public CInventory Inventory { get; private set; }


        public CPlayer(int startX, int startY, int maxX, int maxY)
        {
            X = startX;
            Y = startY;
            this.maxX = maxX;
            this.maxY = maxY;
            
            Inventory = new CInventory();

        }

        

        public void Draw()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(X, Y);
            Console.Write('▣');
            Console.ResetColor();
        }

        public void Clear()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write("  ");
                          
        }
        public void Move(ConsoleKey key)
        {
            Clear();

            int newX = X;
            int newY = Y;
         

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (Y > 0) Y--;
                    break;
                case ConsoleKey.DownArrow:
                    if (Y < maxY - 1) Y++;
                    break;
                case ConsoleKey.LeftArrow:
                    if (X > 0) X--;
                    break;
                case ConsoleKey.RightArrow:
                    if (X < maxX - 1) X++;
                    break;
            }

            if (place.IsMovable(newX, newY))
            {
                Clear();
                X = newX;
                Y = newY;
                Draw();
               
            }
        }
        
    }
}
