using System;

public class InputKey
{
    //키 입력을 저장
	private ConsoleKeyInfo key;
    //키 입력에 따라 바뀌는 커서 값
	public int cursor = 0;
    //선택 여부를 확인
	public bool IsSelect = false;

    //max_Index를 입력받아 일정 범위를 커서가 넘어가지 않도록 함
    public void MoveCursor(int max_Index)
	{
        Console.WriteLine("위, 아래 방향키로 이동 / Enter로 선택");
        key = Console.ReadKey(true);

        switch (key.Key)
        {
            case ConsoleKey.DownArrow:
                //max_Index를 초과하면 커서를 0으로 함
                if (cursor < max_Index)
                    cursor++;
                else
                    cursor = 0;
                break;

            case ConsoleKey.UpArrow:
                //커서가 0이 되면 커서를 max_Index로 함
                if (cursor > 0)
                    cursor--;
                else
                    cursor = max_Index;
                break;

            case ConsoleKey.Enter:
                //현재 커서 위치에 있는 선택지를 선택
                IsSelect = true;
                break;

            default:
                //지정되지 않은 키를 입력했을 경우 전시
                Console.WriteLine("올바른 키를 입력하세요.");
                Thread.Sleep(1000);
                cursor = 0;
                break;
        }
    }
}
