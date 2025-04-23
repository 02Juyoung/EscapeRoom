
using EscapeRoom.Place;
using EscapeRoom.Inventory;
using EscapeRoom.Item;

namespace EscapeRoom.Player
{
    public class CPlayer
    {
        public int X { get; set; }
        public int Y { get; set; }

        private int maxX;
        private int maxY;

        private CPlace place;


        public CInventory Inventory { get; private set; }


        public CPlayer(int startX, int startY, int maxX, int maxY, CPlace place)
        {
            X = startX;
            Y = startY;
            this.maxX = maxX;
            this.maxY = maxY;
            this.place = place;
            Inventory = new CInventory();
        }
        public void Draw()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write('▣');
        }

        public void Clear()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write("  ");
            place.LineFurniture();
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
        
        public void InitPlayer(string doorName)
        {
            (X, Y) = PlayerPosition(doorName); // doorName에 맞는 위치로 초기화
            Draw();
        }
        public (int X, int Y) PlayerPosition(string doorName)
        {
            if (doorName == "LivingRoomDoor")
                return (29, 7);  
            if (doorName == "KitchenDoor")
                return (0, 7);  
            return (14, 7);    
        }
    }
}
