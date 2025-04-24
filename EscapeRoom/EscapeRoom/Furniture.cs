
using EscapeRoom.Inventory;
using EscapeRoom.Item;
using EscapeRoom.Player;
using EscapeRoom.UI;
using EscapeRoom.Door;


namespace EscapeRoom.Furniture
{
    public class CFurniture
    {
        public string Name { get; private set; }

        public List<(int X, int Y)> Positions { get; private set; }

        public List<CItem> Items { get; private set; } //가구 안에 있을 아이템들

        
        public bool IsLocked { get; private set; } = false;

        public CFurniture(string name, string? doorName = null)
        {
            Name = name;
            Positions = new List<(int, int)>();
            Items = new List<CItem>();
            


            if (name == "서랍장")
            {
                IsLocked = true;
                Items.Add(new CItem("주방 문 열쇠", 1));
                Items.Add(new CItem("정체불명의 알약", 2));
            }
            else if (name == "신발장")
            {
                Items.Add(new CItem("-힌트", 1, "진실은 감추려해도 드러나게 마련이다."));
            }
            else if (name == "달력")
            {
                Items.Add(new CItem("-달력을 볼까?", 1, "2xxx년 9월 9일"));
            }
            else if (name == "진열장")
            {
                Items.Add(new CItem("정체불명의 알약", 1));
            }
            else if (name == "싱크대")
            {
                Items.Add(new CItem("정체불명의 알약",1));
            }
            else if (name == "TV")
            {
                Items.Add(new CItem("-TV를 볼까?", 1, "가정폭력으로 구속된 김XX, 자취를 감추다..."));
            }
            else if (name == "선반")
            {
                IsLocked = true;
                Items.Add(new CItem("출구 열쇠", 1));
                Items.Add(new CItem("정체불명의 알약", 2));
            }
            else if (name == "식탁")
            {
                Items.Add(new CItem("-쪽지가 있다", 1, "┼ ↓ 2 ← 6 무슨 뜻이지?"));
            }


        }
        public void Unlock()
        {
            IsLocked = false;
        }

        public void AddPosition(int x, int y)
        {
            Positions.Add((x, y));
        }
        public void AddItem(CItem item)
        {
            Items.Add(item);
        }

        public bool CanInteract(int playerX, int playerY)
        {     
            if (Name == "서랍장")
            {
                return playerX >= 24 && playerY <= 3;
            }
            else if (Name == "신발장")
            {
                return playerX >= 24 && playerY >= 11 && playerY <= 14;
            }
            else if (Name == "달력")
            {
                return playerX <= 8 && playerY >= 12 && playerY <= 14;
            }
            else if (Name == "진열장")
            {
                return playerX <= 5 && playerY <= 3;
            }
            else if (Name == "싱크대")
            {
                return playerX >= 24 && playerY == 3;
            }
            else if (Name == "TV")
            {
                return playerX >= 6 && playerX <= 16 && playerY >= 12;
            }
            else if (Name == "선반")
            {
                return playerX <= 23 && playerX >= 15 && playerY <= 3;
            }
            else if (Name == "식탁")
            {
                return playerX >=10 && playerX <= 24 && playerY >= 5 && playerY <= 9; 
            }
                return false;
            
        }

        public void InteractionFurniture(CInventory inventory, CUI ui)
        {
            string message = $"{Name} 확인\n";

            if (IsLocked)
            {
                if (inventory.CheckItem($"{Name} 열쇠"))
                {
                    Unlock();
                    inventory.RemoveItem($"{Name} 열쇠");
                    ui.InteractionMsg("자물쇠가 열렸다.");
                }
                else
                {
                    ui.InteractionMsg("자물쇠로 잠겨 있다. 열쇠가 필요하다.");
                    return;
                }
            }

            if (Name == "싱크대")
            {
                var pillItem = Items.FirstOrDefault(i => i.Name == "정체불명의 알약" && !i.IsUsed);

                if (pillItem != null)
                {
                    ui.InteractionMsg("싱크대가 더럽다. 정리할까?\n1. 정리한다");
                    var key = Console.ReadKey(true).Key;

                    if (key == ConsoleKey.D1)
                    {                    
                                     
                        pillItem.IsUsed = true;
                        ui.ShowInventory(inventory);
                        return; // 나머지 출력 생략하고 종료
                    }
                }
                else
                {
                    ui.InteractionMsg("더 이상 가져갈만한 게 없다.");
                    return;
                }
            }


            if (Items.Count == 0)
            {
                message = "아무것도 없다.";
                ui.InteractionMsg(message);
                return;
            }

            for (int i = 0; i < Items.Count; i++)
            {
                message += $"{i + 1}. {Items[i].Name}\n";
            }
            message += "\n해당 번호를 눌러주세요.";
            ui.InteractionMsg(message);

            Console.SetCursorPosition(0, 20);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, 20);

            string input = Console.ReadLine();
            if (int.TryParse(input, out int itemNumber))
            {
                if (itemNumber >= 1 && itemNumber <= Items.Count)
                {
                    CItem selectedItem = Items[itemNumber - 1];

                    if (selectedItem.Name.Contains("-"))
                    {
                        ui.InteractionMsg(selectedItem.HintMessage); // 힌트 메시지 출력                       
                    }
                    else
                    {
                        // 아이템을 인벤토리에 추가하고, 해당 아이템을 가구에서 제거
                        inventory.AddItem(selectedItem);
                        Items.Remove(selectedItem);

                        ui.InteractionMsg($"{selectedItem.Name}를 챙겼다.");
                        ui.ShowInventory(inventory);
                    }                      
                }
                else
                {
                    ui.InteractionMsg("잘못된 번호입니다.");
                }
            }
            else
            {
                ui.InteractionMsg("잘못된 입력입니다. 숫자를 입력해주세요.");
            }
        }


        public void Draw()
        {
            // 각 좌표에 맞게 글자 출력
            for (int i = 0; i < Positions.Count && i < Name.Length; i++)
            {
                var pos = Positions[i];
                Console.SetCursorPosition(pos.X, pos.Y);
                Console.Write(Name[i]);
            }
        }
    }
}
