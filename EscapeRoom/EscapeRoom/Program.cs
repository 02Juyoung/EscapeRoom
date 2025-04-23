
using EscapeRoom.Kitchen;
using EscapeRoom.LivingRoom;
using EscapeRoom.Place;
using EscapeRoom.UI;
using EscapeRoom.Player;
using EscapeRoom.Inventory;

namespace EscapeRoom
{

    internal class Program
    {
        

        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            CUI ui = new CUI();
            CPlayer player = new CPlayer(14, 7, 30, 15);
            CPlace livingRoom = new CLivingRoom(ui, player);
            CPlace kitchen = new CKitchen(ui, player);
            CPlace place = livingRoom;

            livingRoom.InitializeFurniture();
            livingRoom.MapItems();

            kitchen.InitializeFurniture();
            kitchen.MapItems();

            CInventory inventory = new CInventory();

            place.DrawRoom();

            
            place.Player.Draw();

            ui.ShowPlayerPosition(place.Player.X, place.Player.Y);


            bool isRunning = true;

            while (isRunning)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;

                    bool changedRoom = place.MovePlayer(key);

                    ui.ShowPlayerPosition(place.Player.X, place.Player.Y);
                    ui.ShowSystemMessage();  // 시스템 메시지 업데이트

                    if (changedRoom)
                    {
                        ui.ClearPlace();

                        // 문을 통해 방을 이동했을 때
                        string doorName = string.Empty;

                        if (place == livingRoom)
                        {
                            place = kitchen;
                            doorName = "KitchenDoor";
                        }
                        else if (place == kitchen)
                        {
                            place = livingRoom;
                            doorName = "LivingRoomDoor";
                        }

                     
                        place.DrawRoom();
                        place.InitPlayer(doorName);

                        
                    }

                }
                place.DrawFurniture();  // 가구 및 상호작용
                ui.ShowInventory(inventory); //인벤토리 출력
                Thread.Sleep(50);
            }
                   
        }
    }
}
