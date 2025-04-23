
using EscapeRoom.Place;
using EscapeRoom.Furniture;
using EscapeRoom.Item;
using EscapeRoom.Player;
using EscapeRoom.UI;


namespace EscapeRoom.Kitchen
{
    public class CKitchen
    {
        private CUI ui;
        private CPlayer player;
        private List<CFurniture> furnitures;
        private Dictionary<(int, int), CItem> mapItems;

        public CKitchen(CUI ui, CPlayer player)
        {
            this.ui = ui;
            this.player = player;
            furnitures = new List<CFurniture>();
            this.mapItems = new Dictionary<(int, int), CItem>();

            InitializeFurniture(); //가구 초기화
        }
       
        public void InitializeFurniture()
        {
            // 부엌에 가구 추가
            var table = new CFurniture("식탁");
            table.AddPosition(5, 5);
            furnitures.Add(table);

            // 추가적인 가구 설정 가능
        }
        public void MapItems()
        {
            // 예시: 부엌에 아이템 추가
            var knife = new CItem("칼", 1);
            mapItems[(7, 5)] = knife;
        }

        public void DrawRoom()
        {
            // 모든 가구들을 화면에 그리기
            foreach (var furniture in furnitures)
            {
                furniture.Draw();
            }
            
        }

        

    }
}
