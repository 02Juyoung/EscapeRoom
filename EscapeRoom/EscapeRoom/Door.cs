

using EscapeRoom.Inventory;
using EscapeRoom.Player;
using EscapeRoom.UI;

namespace EscapeRoom.Door
{
    public class CDoor
    {
        private CUI ui;

        public bool IsLocked { get; private set; } = true;
        public (int x1, int y1, int x2, int y2) DoorPosition { get; private set; }

        public string DoorName { get; private set; }


        public CDoor(int x1, int y1, int x2, int y2, string doorName)
        {
            DoorPosition = (x1, y1, x2, y2);
            DoorName = doorName;
            IsLocked = true;

        }

        public bool CheckDoor(int x, int y)
        {
            if (DoorPosition.x1 == DoorPosition.x2) // 세로 문
            {
                int minY = Math.Min(DoorPosition.y1, DoorPosition.y2);   //Math.Min 둘 중 더 작은 값
                int maxY = Math.Max(DoorPosition.y1, DoorPosition.y2);   //Math.Max 더 큰 값 찾는 메서드
                return x == DoorPosition.x1 && y >= minY && y <= maxY;
            }
            else // 가로 문
            {
                int minX = Math.Min(DoorPosition.x1, DoorPosition.x2);
                int maxX = Math.Max(DoorPosition.x1, DoorPosition.x2);
                return y == DoorPosition.y1 && x >= minX && x <= maxX;
            }
        }
        public void Unlock()
        {
            IsLocked = false;
        }

        public bool Open(CInventory inventory, CUI ui)
        {
            if(!IsLocked)
            {
                return true;
            }
            if(inventory.CheckItem("열쇠"))
            {
                inventory.RemoveItem("열쇠");
                IsLocked = false;
                ui.InteractionMsg("문이 열렸다.");
                return true;
            }           
            else
            {
                ui.InteractionMsg("문이 잠겨 있다. 열쇠가 필요합니다.");
                return false;
            }
        }

    }
}
