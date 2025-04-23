
using EscapeRoom.Place;
using EscapeRoom.Furniture;
using EscapeRoom.Item;
using EscapeRoom.Player;
using EscapeRoom.UI;
using EscapeRoom.Door;


namespace EscapeRoom.Kitchen
{
    public class CKitchen : CPlace
    {
        
        public CKitchen(CUI ui, string doorName = "") : base(ui)
        {
            
        }
       
        public override void InitializeFurniture()
        {
            //식탁 추가
            var table = new CFurniture("식탁");
            table.AddPosition(16, 7);
            table.AddPosition(18, 7);
            for (int x = 12; x <= 22; x++)
            {
                table.AddPosition(x, 6);
                table.AddPosition(12, 7);               
                table.AddPosition(14, 7);
                table.AddPosition(20, 7);
                table.AddPosition(22, 7);
                table.AddPosition(x, 8);
            }
            furnitures.Add(table);

            //싱크대 추가
            var sink = new CFurniture("싱크대");
            sink.AddPosition(25,1);
            sink.AddPosition(27,1);
            sink.AddPosition(29,1);
            for (int x = 23; x <= 29; x++)
            {
                sink.AddPosition(x, 0);
                sink.AddPosition(x, 2);
                sink.AddPosition(23, 1);
            }
            furnitures.Add(sink);

            //선반 추가
            var shelf = new CFurniture("선반");
            shelf.AddPosition(19,1);
            shelf.AddPosition(21,1);
            for (int x = 17; x <= 22; x++)
            {
                shelf.AddPosition(x, 0);
                shelf.AddPosition(x, 2);
                shelf.AddPosition(17, 1);
            }
            furnitures.Add(shelf);

            //TV 추가
            var tv = new CFurniture("TV");
            tv.AddPosition(10,14);
            tv.AddPosition(12,14);
            for (int x = 8; x <= 14; x++)
            {
                tv.AddPosition(x, 13);
                tv.AddPosition(8, 14);
                tv.AddPosition(14,14);
            }
            furnitures.Add(tv);

            //안방 문 생성
            var bedroomDoor = new CDoor(0, 6, 0, 9,"BedRoomDoor");
            AddDoor(bedroomDoor);

            //거실 문 생성
            var livingroomDoor = new CDoor(29, 6, 29, 9, "LivingRoomDoor");
            livingroomDoor.Unlock();
            AddDoor(livingroomDoor);
        }
        public override void MapItems()
        {
            // 예시: 부엌에 아이템 추가
            var knife = new CItem("칼", 1);
            mapItems[(0, 4)] = knife;
        }

        public override void LineFurniture()
        {
            //창문
            // 상단 경계 (─)
            for (int x = 2; x <= 10; x++)
            {
                Console.SetCursorPosition(x, 0);  // (2, 0) 부터 (10, 0)까지
                Console.Write("─");
            }

            // 하단 경계 (─)
            for (int x = 2; x <= 10; x++)
            {
                Console.SetCursorPosition(x, 3);  // (2, 3) 부터 (10, 3)까지
                Console.Write("─");
            }

            // 좌측 경계 (│)
            for (int y = 1; y <= 2; y++)
            {
                Console.SetCursorPosition(2, y);  // (2, 1) 부터 (2, 2)까지
                Console.Write("│");
            }

            // 우측 경계 (│)
            for (int y = 1; y <= 2; y++)
            {
                Console.SetCursorPosition(10, y);  // (10, 1) 부터 (10, 2)까지
                Console.Write("│");
            }

            // 중앙 교차점 (┼)
            Console.SetCursorPosition(6, 1);
            Console.Write("┼");

            Console.SetCursorPosition(6, 2);
            Console.Write("┼");
        }     

        public void DrawRoom()
        {
            base.DrawRoom();
            
            foreach (var furniture in furnitures)
            {
                furniture.Draw();
            }
            
        }

        

    }
}
