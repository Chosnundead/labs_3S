#include "Hardware.h"
#include "Game.h"
#include "SlotMachine.h"
#include "Blackjack.h"
#include "Craps.h"

enum gameModeEnum { GAME_SLOT, GAME_BLACKJACK, GAME_CRAPS };
gameModeEnum gameMode;
bool inMenu = true;
#define MENU_FLASH_RATE 200;
unsigned long menuTimeout;
bool menuState = false;

void setup() 
{
  Serial.begin(115200);

  hardwareSetup();

  creditBalance = STARTING_CREDIT_BALANCE;

  menuSetup();
}

void loop() 
{
  if (inMenu)
  {
    lastButtonPressed = processMenu();
    if (lastButtonPressed == BTN_HIT || lastButtonPressed == BTN_STAND)
    {
      lastButtonPressed = BTN_NONE;
      switch(gameMode)
      {
        case GAME_SLOT: slotMachineSetup(); break;
        case GAME_BLACKJACK: blackjackSetup(); break;
        case GAME_CRAPS: crapsSetup(); break;
      }
      inMenu = false;
    }
  }
  else
  {
    bool exitGame = false;
    switch(gameMode)
    {
      case GAME_SLOT: exitGame = slotMachineLoop(); break;
      case GAME_BLACKJACK: exitGame = blackjackLoop(); break;
      case GAME_CRAPS: exitGame = crapsLoop(); break;
    }
    if (exitGame)
    {
      menuSetup();
    }
  }
}

void menuSetup()
{
  lcd.clear();
  lcd.print("SolodkiyBondarev");
  
  //Slot machine 7
  for (int y = 0; y < 8; y++)
  {
    reels[0][y] = getReelRow((SEVEN << 3) + y);
  }
  lcd.createChar(0, &reels[0][0]);
  //Craps 3 & 4
  lcd.createChar(3, &dice[THREE][0]);
  lcd.createChar(4, &dice[FOUR][0]);
  //Black jack
  lcd.createChar(DIAMOND, &cards[DIAMOND][0]);  //1
  lcd.createChar(SPADE, &cards[SPADE][0]);      //2
  
  inMenu = true;
  displayMenu(true);
}

void displayMenu()
{
  displayMenu(false);
}

void displayMenu(bool force)
{
  menuState = menuState | force;
  force = force || (millis() > menuTimeout);
  if (force) 
  {
    menuTimeout = millis() + MENU_FLASH_RATE;
    bool on = menuState;
    menuState = !menuState;

    lcd.setCursor(0, 1);
    if (!on || gameMode != GAME_SLOT)
    {
      //3 sevens
      lcd.write((byte)0);
      lcd.write((byte)0);
      lcd.write((byte)0);
    }
    else
    {
      lcd.print("   ");
    }
    
    lcd.setCursor(6, 1);
    if (!on || gameMode != GAME_BLACKJACK)
    {
      //Blackjack
      lcd.print("J");
      lcd.write(DIAMOND);
      lcd.print("A");
      lcd.write(SPADE);
    }
    else
    {
      lcd.print("    ");
    }
    
    lcd.setCursor(13, 1);
    if (!on || gameMode != GAME_CRAPS)
    {
      //Craps
      lcd.write((byte)4);
      lcd.print(" ");
      lcd.write((byte)3);
    }
    else
    {
      lcd.print("   ");
    }
  }
}

buttonEnum processMenu() 
{
  // Prompt user.
  bool force = false;
  lastButtonPressed = buttonPressed(displayMenu);
  switch (lastButtonPressed) 
  {
    case BTN_MORE:
      gameMode = (gameMode == GAME_CRAPS) ? GAME_SLOT : (gameModeEnum)((int)gameMode + 1);
      force = true;
      break;

    case BTN_LESS:
      gameMode = (gameMode == GAME_SLOT) ? GAME_CRAPS : (gameModeEnum)((int)gameMode - 1);
      force = true;
      break;
  }
  displayMenu(force);
  return lastButtonPressed;
};
