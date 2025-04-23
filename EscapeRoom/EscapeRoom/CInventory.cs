using EscapeRoom.Item;


namespace EscapeRoom.Inventory
{
    public class CInventory
    {

       public List<CItem> Items { get; private set; } = new List<CItem>();

       public void AddItem(CItem item)
       {
            var existingItem = Items.FirstOrDefault(i => i.Name == item.Name);
            if (existingItem == null)
            {
                // 아이템이 없으면 새로운 아이템을 추가
                Items.Add(item);
            }
            else
            {
                // 아이템이 있으면 그 아이템의 갯수를 증가
                existingItem.Quantity++;
            }
       }

       public void RemoveItem(CItem item)
       {

            var existingItem = Items.FirstOrDefault(i => i.Name == item.Name);
            if (existingItem != null)
            {
                // 아이템 갯수를 하나 감소시킨다.
                existingItem.Quantity--;

                // 갯수가 0이면 아이템을 리스트에서 삭제한다.
                if (existingItem.Quantity <= 0)
                {
                    Items.Remove(existingItem);
                }
            }
       }
        public void RemoveItem(string name) //이름(strinig)으로 아이템 제거하는 메서드
        {
            var existingItem = Items.FirstOrDefault(i => i.Name == name);
            if (existingItem != null)
            {
                existingItem.Quantity--;
                if (existingItem.Quantity <= 0)
                {
                    Items.Remove(existingItem);
                }
            }
        }


        //특정 아이템이 있는지 확인하는 메서드
        public bool CheckItem(string itemName)
        {
            return Items.Any(item => item.Name == itemName);
        }


        public List<string> ShowItem()
        {
            List<string> messages = new List<string>();

            if (Items.Count == 0)
            {
                messages.Add("[소지품] : 없음");
            }
            else
            {
                messages.Add("[소지품]");
                for (int i = 0; i < Items.Count; i++)
                {
                    string itemMessage = $"{i + 1}. {Items[i].Name} ({Items[i].Quantity}개)";
                    messages.Add(itemMessage);
                }
            }

            return messages;
        }

    }
}
