using System;

public class InputKey
{
	private ConsoleKeyInfo key;
	public int cursor = 0;
	public bool IsSelect = false;
	
	public void MoveCursor(int max_Index)
	{
        Console.WriteLine("위, 아래 방향키로 이동 / Enter로 선택");
        key = Console.ReadKey(true);

        switch (key.Key)
        {
            case ConsoleKey.DownArrow:
                if (cursor < max_Index)
                    cursor++;
                else
                    cursor = 0;
                break;

            case ConsoleKey.UpArrow:
                if (cursor > 0)
                    cursor--;
                else
                    cursor = max_Index;
                break;

            case ConsoleKey.Enter:
                IsSelect = true;
                break;

            default:
                Console.WriteLine("올바른 키를 입력하세요.");
                Thread.Sleep(1000);
                cursor = 0;
                break;
        }
    }
}
