
using EscapeRoom.Kitchen;
using EscapeRoom.LivingRoom;
using EscapeRoom.Place;
using EscapeRoom.UI;
using EscapeRoom.Player;
using EscapeRoom.Inventory;
using EscapeRoom.Door;

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
            ui.ShowInventory(inventory);

            place.DrawRoom();
            ui.DrawFrame();
            ui.ShowSystemMessage();


            place.Player.Draw();
            

            ui.ShowPlayerPosition(place.Player.X, place.Player.Y);


            bool isRunning = true;

            while (isRunning)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;

                    var result = place.MovePlayer(key);

                    ui.ShowPlayerPosition(place.Player.X, place.Player.Y);

                    if (result.isExitOpened)
                    {
                        Thread.Sleep(500);
                        Console.Clear();
                        Console.SetCursorPosition(10, 7);
                        Console.WriteLine("탈출에 성공했습니다!");
                        Thread.Sleep(2000);
                        isRunning = false;
                        continue;
                    }

                    if (result.roomChanged)
                    {
                        ui.ClearPlace();
                        ui.InteractionMsg("이동 중...");
                        Thread.Sleep(500);
                        ui.InteractionMsg("            ");

                        string doorName = "";

                        if (place == livingRoom)
                        {
                            place = kitchen;
                            doorName = "주방 문";
                        }
                        else if (place == kitchen)
                        {
                            place = livingRoom;
                            doorName = "거실 문";
                        }

                        place.DrawRoom();
                        place.InitPlayer(doorName);
                    }

                    place.DrawLine(player);
                }

                place.DrawFurniture();
                Thread.Sleep(50);


            }
                   
        }
    }
}
