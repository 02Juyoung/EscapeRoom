﻿

using System;
using EscapeRoom.Inventory;

namespace EscapeRoom.UI
{
    public class CUI
    {
        private int startX = 33; //UI가 시작될 좌표
        private int uiWidth = 50;
        private int uiHeight = 20;

        public void DrawFrame()
        {
            for(int y = 0; y < uiHeight; y++)
            {
                Console.SetCursorPosition(startX-1, y);
                Console.Write("|");
                Console.SetCursorPosition(0, 16);                
            }
            Console.Write("──────────────────────────────");
        }
        public void ShowMessage(string message, int line)
        {

            Console.SetCursorPosition(startX, line);
            Console.Write(message);
        }

        public void ShowSystemMessage()
        {
            ShowMessage("[System Message]", 0);

        }
       
        public void ShowPlayerPosition(int x, int y)
        {
            ShowMessage($"플레이어 위치: ({x}, {y})      ", 2);
        }

        public void InteractionMsg(string message) // 상호작용 메세지
        {
            for (int i = 4; i < uiHeight; i++)
            {
                ShowMessage(new string(' ', uiWidth), i);  // 공백으로 덮기
            }

            string[] lines = message.Split('\n');
            int lineOffset = 4;

            for (int i = 0; i < lines.Length && i + lineOffset < uiHeight; i++)
            {
                ShowMessage(lines[i], i + lineOffset);
            }
        }

        public void ShowInventory(CInventory inventory)
        {
            int startY = 17;
            int maxLines = uiHeight - startY;

            // 출력 클리어
            for (int i = 0; i < maxLines; i++)
            {
                Console.SetCursorPosition(0, startY + i);
                Console.Write(new string(' ', startX));
            }

            List<string> messages = inventory.ShowItem();

            for (int i = 0; i < messages.Count && i < maxLines; i++)
            {
                Console.SetCursorPosition(0, startY + i);
                Console.Write(messages[i]);
            }
        }

        public void Clear()
        {
            for (int i = 0; i < uiHeight; i++)
            {
                Console.SetCursorPosition(startX, i);
                Console.Write(new string(' ', uiWidth));
            }
        }

        public void ClearPlace() //Place 부분만 지우기
        {
            for (int y = 0; y < 15; y++)
            {
                Console.SetCursorPosition(0, y);
                Console.Write(new string(' ', 31));
            }
        }

    }
}
