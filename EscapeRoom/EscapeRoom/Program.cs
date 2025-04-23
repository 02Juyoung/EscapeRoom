
using EscapeRoom.Kitchen;
using EscapeRoom.Place;
using EscapeRoom.UI;

namespace EscapeRoom
{

    internal class Program
    {
        

        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            CUI ui = new CUI();
            
            CPlace place = new CPlace(ui);
           

            place.InitPlayer();
    

            ui.ShowPlayerPosition(place.Player.X, place.Player.Y);

            place.InitializeFurniture();
            place.Carpet();
            place.MapItems();


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
                        Console.Clear();
                        ui.ShowMessage("주방으로 이동합니다...", 3);
                        Thread.Sleep(1000);

                        // 여기서 CPlace 대신 CKitchen 같은 걸로 교체 가능
                        CKitchen kitchen = new CKitchen(ui, place.Player);
                        Console.Clear();
                        kitchen.DrawRoom();
                    }


                }

                place.DrawFurniture();  // 가구 및 상호작용
                ui.ShowInventory(place.Player.Inventory); //인벤토리 표시

                Thread.Sleep(50);
            }

         


        }
    }
}
