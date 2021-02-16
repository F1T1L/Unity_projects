using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Hacker : MonoBehaviour
{
    int level;
    string PSW, menuHint ="U may type 'menu' at any time.";
    string[] level1Passwords = { "books", "aisle", "self", "password", "font", "borrow" };
    string[] level2Passwords = { "prisoner", "handcuffs", "holster", "uniform", "arrest" };
    enum Screen { MainMenu, WaitingForPSW, Win};
    Screen currentScreen;

    // Start is called before the first frame update
    void Start()
    {
        ShowMainMenu();
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    void OnUserInput(string input)
    {
      
        if (input == "menu")
        {
            ShowMainMenu();
        }
        else if (input == "close" || input == "exit" || input == "quit")
        {
            Terminal.WriteLine("End of session.");
            Application.Quit();
           
        }
        else if (currentScreen == Screen.MainMenu)
        {
            RunMainMenu(input);
        }
        else if (currentScreen == Screen.WaitingForPSW)
        {
            CheckPSW(input);
        }
        else if (currentScreen == Screen.Win)
        {            
            ShowMainMenu();
        }
    }

    void CheckPSW(string input)
    {
        if(input == PSW.ToString())
        {
            DisplayWinScreen();
        }
        else
        {
            AskForPSW();

        }
        
    }
    void DisplayWinScreen()
    {
        currentScreen = Screen.Win;
        Terminal.ClearScreen();
        ShowReward();        
    }

     void ShowReward()
    {
        switch (level)
        {
            case 1:
                Terminal.WriteLine(@"
──────▄▀▄─────▄▀▄
─────▄█░░▀▀▀▀▀░░█▄
─▄▄──█░░░░░░░░░░░█──▄▄
█▄▄█─█░░▀░░┬░░▀░░█─█▄▄█
                
");               
                break;
            case 2:
                Terminal.WriteLine(@"
▒▒▄▀▀▀▀▀▄▒▒▒▒▒▄▄▄▄▄▒▒▒
▒▐░▄░░░▄░▌▒▒▄█▄█▄█▄█▄▒
▒▐░▀▀░▀▀░▌▒▒▒▒▒░░░▒▒▒▒
▒▒▀▄░═░▄▀▒▒▒▒▒▒░░░▒▒▒▒
▒▒▐░▀▄▀░▌▒▒▒▒▒▒░░░▒▒▒▒
");                
                break;
            case 3:
                Terminal.WriteLine(@"
─────█─▄▀█──█▀▄─█─────
────▐▌──────────▐▌────
────█▌▀▄──▄▄──▄▀▐█────
───▐██──▀▀──▀▀──██▌───
──▄████▄──▐▌──▄████▄──
");
                break;
            default:
                break;
        }
        Terminal.WriteLine("You guessed password!");
        Terminal.WriteLine("Press <ENTER> to go MENU");
    }

    void RunMainMenu(string input)
    {
        bool isValidLevelNumber = (input == "1" || input == "2" || input == "3");
        if (isValidLevelNumber)
        {
            level = int.Parse(input);
            AskForPSW();
        }        
        else if (input == "007")
        {
            Terminal.WriteLine("Hello Mr.Bond");
        }
        else
        {
            Terminal.WriteLine("Please choose a valid target!");
            Terminal.WriteLine(menuHint);
        }
    }

    void ShowMainMenu()
    {
        currentScreen = Screen.MainMenu;
        Terminal.ClearScreen();
        Terminal.WriteLine("RUSH_B SYKA BLYATb!" +
            "\n Its time to HACK!");
        Terminal.WriteLine("Press button to choose your target:");
        Terminal.WriteLine("1. Pentagon");
        Terminal.WriteLine("2. KGB");
        Terminal.WriteLine("3. NASA");
        
    }
    void AskForPSW()
    {
        currentScreen = Screen.WaitingForPSW;
        Terminal.ClearScreen();
        SetRandomPSW();
        Terminal.WriteLine("Enter PSW, hint(Anagram): " + PSW.Anagram());
        Terminal.WriteLine(menuHint);
    }
    void SetRandomPSW()
    {
        switch (level)
        {
            case 1:
                PSW = level1Passwords[UnityEngine.Random.Range(0, level1Passwords.Length)];
                break;
            case 2:
                PSW = level2Passwords[UnityEngine.Random.Range(0, level2Passwords.Length)];
                break;
            case 3:
                PSW = level2Passwords[UnityEngine.Random.Range(0, level2Passwords.Length)];
                break;
            default:
                Debug.LogError("Invalid level");
                break;
        }
    }
}

