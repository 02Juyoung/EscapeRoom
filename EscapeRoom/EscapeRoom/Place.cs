using EscapeRoom.Player;
using EscapeRoom.UI;
using EscapeRoom.Furniture;
using EscapeRoom.Item;
using EscapeRoom.Door;
using EscapeRoom.Kitchen;
using static EscapeRoom.Program;



namespace EscapeRoom.Place
{
    public abstract class CPlace
    {
        private int mapWidth = 30;
        private int mapHeight = 15;

        protected CPlayer player;

        private CUI ui;
       

        protected List<CFurniture> furnitures { get; private set; }
        protected List<CDoor> doors { get; private set; }
        protected Dictionary<(int, int), CItem> mapItems { get; private set; }


        public CPlace(CUI ui) // 생성자에 ui 객체를 전달받도록 수정
        {
            this.ui = ui;  // ui를 필드에 저장
            
            furnitures = new List<CFurniture>();
            doors = new List<CDoor>();
            mapItems = new Dictionary<(int, int), CItem>();

        }
        public void AddDoor(CDoor door)  // 문을 리스트에 추가
        {
            doors.Add(door); 
        }

        public abstract void InitializeFurniture();


        public abstract void MapItems();


        public virtual void DrawRoom()
        {
            LineFurniture();
            foreach (var furniture in furnitures)
            {
                furniture.Draw();
            }
        }

        public abstract void LineFurniture();

        public void DrawFurniture()
        {
            foreach (var furniture in furnitures)
            {
                // 가구의 위치에 이름 출력
                for (int i = 0; i < furniture.Positions.Count; i++)
                {
                    var pos = furniture.Positions[i];
                    Console.SetCursorPosition(pos.X, pos.Y);

                    if (i < furniture.Name.Length) //가구 이름의 길이만큼 출력
                    {
                        //각 자리마다 한 글자씩 출력
                        Console.Write(furniture.Name[i]);
                    }
                    else
                    {
                        // 나머지 위치는 도형으로 출력
                        Console.Write("■");
                    }
                }
            }
            //문 출력
            foreach (var door in doors)
            {
                for (int y = door.DoorPosition.y1; y <= door.DoorPosition.y2; y++)
                {
                    for (int x = door.DoorPosition.x1; x <= door.DoorPosition.x2; x++)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write("■");  // 문을 표시할 때 사용 (문 앞에 표시할 문자를 결정)
                    }
                }
            }
            player.Draw();
        }


        public CPlayer Player => player;     

        public bool IsMovable(int newX, int newY) //플레이어가 이동할 수 있는 곳인지 확인
        {
            if (newX < 0 || newX >= mapWidth || newY < 0 || newY >= mapHeight)
            {
                return false;
            }

            foreach (var furniture in furnitures)
            {
                foreach (var pos in furniture.Positions)
                {
                    if (newX == pos.Item1 && newY == pos.Item2)
                    {
                        return false;  // 가구 영역에는 이동할 수 없도록
                    }
                }
            }

            return true;
        }

        public bool MovePlayer(ConsoleKey key) //플레이어 이동 제한
        {
            int newX = player.X;
            int newY = player.Y;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    newY--;
                    break;
                case ConsoleKey.DownArrow:
                    newY++;
                    break;
                case ConsoleKey.LeftArrow:
                    newX--;
                    break;
                case ConsoleKey.RightArrow:
                    newX++;
                    break;
            }
            if (IsMovable(newX, newY))
            {
                player.Clear();
                player.X = newX;
                player.Y = newY;
          
                player.Draw();
                
            }

            //문 상호작용 후 방 이동이 됐는지 확인하는 메서드
            if (key == ConsoleKey.Spacebar)
            {
                foreach (var door in doors)
                {
                    if (door.CheckDoor(player.X, player.Y))
                    {
                        if (door.Open(player, ui))
                        {                           
                            return true;
                        }
                        
                    }
                }
            }

            var furniture = CheckFurniturePosition();
            if (furniture != null)
            {
                if (key == ConsoleKey.Spacebar && furniture.CanInteract(newX, newY)) // 스페이스바 눌렀을 때 상호작용
                {
                    furniture.InteractionFurniture(player, ui); // 가구와 상호작용
                }
            }

            if (key == ConsoleKey.Spacebar)
            {
                if (mapItems.TryGetValue((newX, newY), out CItem item))
                {
                    if (!item.Name.Contains("힌트"))
                    {
                        player.Inventory.AddItem(item);
                        ui.ShowMessage($"{item.Name}을(를) 획득했습니다!              ",3);
                        mapItems.Remove((newX, newY));  // 아이템 제거
                    }
                    

                }
            }
            return false;
            
        }
        public CFurniture CheckFurniturePosition()
        {
            foreach (var furniture in furnitures)
            {
                if (furniture.CanInteract(player.X, player.Y))
                {
                    return furniture;
                }
            }
            return null;
        }
   

    }
}
    
       
    



