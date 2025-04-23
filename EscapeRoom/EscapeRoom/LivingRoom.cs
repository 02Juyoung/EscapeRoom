

using EscapeRoom.Door;
using EscapeRoom.Furniture;
using EscapeRoom.Item;
using EscapeRoom.UI;
using EscapeRoom.Place;
using EscapeRoom.Player;

namespace EscapeRoom.LivingRoom
{
    public class CLivingRoom : CPlace
    {  
        public CLivingRoom(CUI ui, CPlayer player) : base(ui, player)
        {
            

        }

        public override void InitializeFurniture()
        {
            // 서랍장 생성
            var drawer = new CFurniture("서랍장");
            drawer.AddPosition(25, 1);
            drawer.AddPosition(27, 1);
            drawer.AddPosition(29, 1);
            for (int x = 25; x <= 29; x++)
            {
                drawer.AddPosition(x, 0);
                drawer.AddPosition(x, 2);
            }

            furnitures.Add(drawer);

            // 신발장 생성
            var shoeCabinet = new CFurniture("신발장");
            shoeCabinet.AddPosition(25, 13);
            shoeCabinet.AddPosition(27, 13);
            shoeCabinet.AddPosition(29, 13);

            for (int x = 25; x <= 29; x++)
            {
                shoeCabinet.AddPosition(x, 12);
                shoeCabinet.AddPosition(x, 14);
            }


            furnitures.Add(shoeCabinet);

            //진열장 생성
            var showcase = new CFurniture("진열장");
            showcase.AddPosition(0, 1);
            showcase.AddPosition(2, 1);
            showcase.AddPosition(4, 1);

            for (int x = 0; x <= 4; x++)
            {
                showcase.AddPosition(x, 0);
                showcase.AddPosition(x, 2);
            }

            furnitures.Add(showcase);

            //달력 생성
            var carlendar = new CFurniture("달력");
            carlendar.AddPosition(2, 14);
            carlendar.AddPosition(4, 14);

            for (int x = 0; x <= 6; x++)
            {
                carlendar.AddPosition(0, 14);
                carlendar.AddPosition(6, 14);
                carlendar.AddPosition(x, 13);
            }
            furnitures.Add(carlendar);

            //주방 문 생성
            var kitchenDoor = new CDoor(0, 6, 0, 9,"KitchenDoor");
            AddDoor(kitchenDoor);


        }

        public override void MapItems()
        {
            // (9, 9) 위치에 열쇠 아이템 추가
            var keyItem = new CItem("열쇠", 1);
            mapItems[(9, 9)] = keyItem;
        }
        public override void LineFurniture()
        {
            //카펫
            //상단 경계
            for (int x = 7; x <= 21; x++)
            {
                Console.SetCursorPosition(x, 5);  // (7, 5) 위치부터 시작
                Console.Write("_");
            }

            // 하단 경계
            for (int x = 7; x <= 21; x++)
            {
                Console.SetCursorPosition(x, 10);  // (7, 10) 위치
                Console.Write("_");
            }

            // 좌측 경계
            for (int y = 6; y <= 10; y++)  // (6, 9)까지 세로
            {
                Console.SetCursorPosition(7, y);  // (7, 6) ~ (7, 9)
                Console.Write("|");
            }

            // 우측 경계
            for (int y = 6; y <= 10; y++)  // (6, 9)까지 세로
            {
                Console.SetCursorPosition(21, y);  // (21, 6) ~ (21, 9)
                Console.Write("|");
            }

        }
      

        public override void DrawRoom()
        {
            base.DrawRoom();
            foreach (var door in doors)
            {
                for (int y = door.DoorPosition.y1; y <= door.DoorPosition.y2; y++)
                {
                    for (int x = door.DoorPosition.x1; x <= door.DoorPosition.x2; x++)
                    {
                        Console.SetCursorPosition(x, y);
                        
                        if (door.IsLocked)
                        {
                            Console.Write("■");  // 잠긴 문
                        }
                        else
                        {
                            Console.Write("▒");  // 열린 문
                        }
                    }
                }
            }
        }
       

    }
}
