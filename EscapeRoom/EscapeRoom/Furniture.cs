
using EscapeRoom.Item;
using EscapeRoom.Player;
using EscapeRoom.UI;


namespace EscapeRoom.Furniture
{
    public class CFurniture
    {
        public string Name { get; private set; }

        public List<(int X, int Y)> Positions { get; private set; }

        public List<CItem> Items { get; private set; } //가구 안에 있을 아이템들


        public CFurniture(string name)
        {
            Name = name;
            Positions = new List<(int, int)>();
            Items = new List<CItem>();

            if (name == "서랍장")
            {
                Items.Add(new CItem("열쇠",1));
                Items.Add(new CItem("지도",2));
            }
            else if (name == "신발장")
            {
                Items.Add(new CItem("-힌트", 1, "진실은 감추려해도 드러나게 마련이다."));
            }
            else if (name == "달력")
            {
                Items.Add(new CItem("-달력을 볼까?", 1, "2xxx년 9월 9일"));
            }


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
            // 서랍장의 상호작용 범위: (24, 0)~(29, 3)
            if (Name == "서랍장")
            {
                return playerX >= 24 && playerX <= 29 && playerY >= 0 && playerY <= 3;
            }
            else if (Name == "신발장")
            {
                return playerX >= 24 && playerX <= 29 && playerY >= 11 && playerY <= 14;
            }
            else if (Name == "달력")
            {
                return playerX >= 0 && playerX <= 8 && playerY >= 12 && playerY <= 14;
            }
                return false;

        }

        public void InteractionFurniture(CPlayer player, CUI ui)
        {
            string message = $"{Name} 확인\n";
            for (int i = 0; i < Items.Count; i++)
            {
                message += $"{i + 1}. {Items[i].Name}\n";
            }
            message += "\n번호를 눌러 아이템을 가져가세요.";
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
                        player.Inventory.AddItem(selectedItem);
                        Items.Remove(selectedItem);

                        ui.InteractionMsg($"{selectedItem.Name}를 챙겼다.");
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
