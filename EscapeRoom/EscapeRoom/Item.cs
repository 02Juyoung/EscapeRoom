

namespace EscapeRoom.Item
{
    public class CItem
    {     
        public string Name { get; set; }
        public int Number { get; set; }
        public int Quantity { get; set; }
        public string HintMessage { get; set; }

        public CItem(string name, int number, string hintMsg = "")
        {         
            Name = name;
            this.Number = number;
            Quantity = 1;
            HintMessage = hintMsg;
        }
    }
}
