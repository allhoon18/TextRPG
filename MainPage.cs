using System;
using static System.Net.Mime.MediaTypeNames;

public class MainPage
{
    static void Main()
    {
        //Gamemanager 실행
        Gamemanager gamemanager = new Gamemanager();
        gamemanager.GameSystem();
    }
}
