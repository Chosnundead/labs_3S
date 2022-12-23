#include "Hardware.h"
#include "Game.h"
#include "Symbols.h"

enum crapsPlayModeEnum { CRAPS_NOINPUT, CRAPS_GETBET, CRAPS_THROW };

crapsPlayModeEnum crapsPlayMode = CRAPS_GETBET;
int firstSum;

void crapsSetup();
bool crapsLoop();
void processThrow(bool first);
int newRoll(int lastRoll);
int roll(int x1, int x2, int y);

void crapsSetup()
{
  //Define all the new card characters
  lcd.createChar(ONE, &dice[ONE][0]);
  lcd.createChar(TWO, &dice[TWO][0]);
  lcd.createChar(THREE, &dice[THREE][0]);
  lcd.createChar(FOUR, &dice[FOUR][0]);
  lcd.createChar(FIVE, &dice[FIVE][0]);
  lcd.createChar(SIX, &dice[SIX][0]);

  lcd.clear();
  lcd.setCursor(0,0);
  lcd.print("[?] [?]  [ ] [ ]");

  //Animate balance upto current balance
  animateBank(0, creditBalance, DIGIT_DELAY_TIME, 50);
 
  crapsPlayMode = CRAPS_GETBET;
  bet = BET_MIN;
  displayBet(bet);
}

bool crapsLoop()
{
  //Get button pressed
  lastButtonPressed = buttonPressed(NULL);
  while (lastButtonPressed != BTN_HIT && lastButtonPressed != BTN_NONE)
  {
    switch (crapsPlayMode) 
    {
      case CRAPS_GETBET: 
        if (processGetBet() == BTN_STAND) 
        {
          processThrow(true);
        }
        break;
        
      case CRAPS_THROW: 
        processThrow(false);
        break;
    }
    lastButtonPressed = buttonPressed(NULL);
  }
  return (lastButtonPressed == BTN_HIT);
}

void processThrow(bool first)
{
  int payout = 0;
  if (crapsPlayMode == CRAPS_THROW)
  {
    int secondSum = roll(10, 14, 0);
    if (secondSum == firstSum)
    {
      //Player wins
      payout = bet;
      playWinSoundShort();
    }
    else if (secondSum == 7)
    {
      //Player loses
      payout = -bet;
      playLoseSound();
    }
  }
  else
  {
    //Clear second dice
    lcd.setCursor(10, 0);
    lcd.print(" ");
    lcd.setCursor(14, 0);
    lcd.print(" ");
    
    firstSum = roll(1, 5, 0);
    if (firstSum == 7 || firstSum == 11)
    {
      //Automatic Win
      payout = bet;
      playWinSoundShort();
    }
    else if (firstSum == 2 || firstSum == 3 || firstSum == 12)
    {
      //Automatic Loss
      payout = -bet;
      playLoseSound();
    }
    else
    {
      //Throw second pair
      crapsPlayMode = CRAPS_THROW;
    }
  }
  if (payout != 0)
  {
    long newBalance = min(creditBalance + payout, 99999);
    Serial.println("creditBalance = " + String(creditBalance) + ", payout = " + String(payout) + ", newBalance = " + String(newBalance));
    creditBalance = animateBank(creditBalance, newBalance, DIGIT_DELAY_TIME, 1);
    if (creditBalance < 0)
    {
      playLoseSound();
      creditBalance = animateBank(creditBalance, STARTING_CREDIT_BALANCE, DIGIT_DELAY_TIME, 10);
    }
    crapsPlayMode = CRAPS_GETBET;
  }
}

int newRoll(int lastRoll)
{
  int roll = lastRoll;
  while (roll == lastRoll)
  {
    roll = random(1, 7);
  }
  return roll;
}

int roll(int x1, int x2, int y)
{
  int d = 0;
  int dice1 = -1;
  int dice2 = -1;
  for (int i = 0; i < (random(7, 30)); i++) 
  {
    dice1 = newRoll(dice1);
    lcd.setCursor(x1, y);
    lcd.write(dice1 - 1);
    dice2 = newRoll(dice2);
    lcd.setCursor(x2, y);
    lcd.write(dice2 - 1);
    playRollTone();
    delay(d);
    d = d + 50;
  }
  return dice1 + dice2;
}
