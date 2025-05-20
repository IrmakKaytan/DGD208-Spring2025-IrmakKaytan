using System;
using System.Collections.Generic;

namespace DGD208_Spring2025_IrmakKaytan;

public class Game
{
    private readonly Menu mainMenu;
    private bool isRunning;

    public Game()
    {
        mainMenu = Menu.CreateMainMenu();
        isRunning = true;
    }

    public void Run()
    {
        Console.WriteLine("Welcome to ByteMonster: Digital Pet Viruses!");

        while (isRunning)
        {
            Console.Clear();
            var choice = mainMenu.Show();

            switch (choice)
            {
                case 1:
                    ShowAdoptionMenu();
                    break;
                case 2:
                    ShowViewPetsMenu();
                    break;
                case 3:
                    ShowUseItemMenu();
                    break;
                case 4:
                    ShowAbout();
                    break;
                case 5:
                    isRunning = false;
                    break;
            }

            if (isRunning)
            {
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }
    }

    private void ShowAdoptionMenu()
    {
        var adoptionMenu = Menu.CreateAdoptionMenu();
        Console.Clear();
        adoptionMenu.Show();
    }

    private void ShowViewPetsMenu()
    {
        Console.WriteLine("View Pets menu will be implemented later.");
    }

    private void ShowUseItemMenu()
    {
        Console.WriteLine("Use Item menu will be implemented later.");
    }

    private void ShowAbout()
    {
        Console.WriteLine("\nByteMonster: Digital Pet Viruses");
        Console.WriteLine("Created by: Irmak Kaytan");
        Console.WriteLine("Student Number: 2305041084");
    }
}