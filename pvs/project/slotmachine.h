#include "Hardware.h"
#include "Game.h"
#include "Symbols.h"

enum slotPlayModeEnum { SLOT_NOINPUT, SLOT_GETBET, SLOT_SPIN };

#define WHEELS 3

/* Timing constants that ontrol how the reels spin */
#define START_DELAY_TIME 10
#define INCREMENT_DELAY_TIME 5
#define PAUSE_TIME 1000
#define MAX_DELAY_BEFORE_STOP 100
#define MIN_SPIN_TIME 1000
#define MAX_SPIN_TIME 3000
#define FLASH_REPEAT 10
#define FLASH_TIME 150

/* spinDigit holds the information for each wheel */
struct spinDigit 
{
  unsigned long delayTime;
  unsigned long spinTime;
  unsigned long frameTime;
  uint8_t row;
  uint8_t symbol;
  bool stopped;
  int stopDigit;
};

spinDigit spin[WHEELS]; 
//This array is used to create the unique wheel characters for the 1602 display
byte reels[WHEELS][8];

//Smiley faces are wild cards
//Payouts are halved if they contain one or more smiley faces
//Three smiley faces are treated as THREE_SYMBOL_PAYOUT
#define THREE_SEVEN_PAYOUT  600
#define THREE_SYMBOL_PAYOUT 122
#define TWO_SEVEN_PAYOUT    50
#define ONE_SEVEN_PAYOUT    3
#define TWO_SYMBOL_PAYOUT   2

slotPlayModeEnum slotPlayMode = SLOT_GETBET;


void slotMachineSetup(); 
bool slotMachineLoop(); 
void processSpin();
unsigned long spinTheWheels();
unsigned long highlightWinAndCalculatePayout();
void flashSymbol(uint8_t symbol);
void displayWheelSymbol(int wheel);
uint8_t getReelRow(uint8_t row);

void slotMachineSetup() 
{
  //Set up each wheel. 
  for (uint8_t j = 0; j < WHEELS; j++)
  {
    spin[j].row = random(0, REEL_SYMBOLS) << 3;  //Start each wheel on a random symbol
  }

  // Print slot machine screen
  lcd.clear();
  lcd.setCursor(0,0);
  lcd.print("[ ? ][ ? ][ ? ]");

  //Animate balance upto current balance
  Serial.println("creditBalance = " + String(creditBalance));
  animateBank(0, creditBalance, DIGIT_DELAY_TIME, 50);
  Serial.println("creditBalance = " + String(creditBalance));
 
  //Show the openning set of wheels
  for (uint8_t j = 0; j < WHEELS; j++)
  {
    displayWheelSymbol(j);
  }
  
  slotPlayMode = SLOT_GETBET;
  bet = BET_MIN;
  displayBet(bet);
}

bool slotMachineLoop() 
{
  //Get button pressed
  lastButtonPressed = buttonPressed(NULL);
  while (lastButtonPressed != BTN_HIT && lastButtonPressed != BTN_NONE)
  {
    switch (slotPlayMode) 
    {
      case SLOT_GETBET: 
        if (processGetBet() == BTN_STAND) 
        {
          processSpin(); 
        }
        break;
        
      case SLOT_SPIN: 
        processSpin(); 
        break;
    }
    lastButtonPressed = buttonPressed(NULL);
  }
  return (lastButtonPressed == BTN_HIT);
}

void processSpin()
{
  unsigned long payout = spinTheWheels();
  long newBalance = min(creditBalance + (payout * bet) - bet, 99999);
  Serial.println("creditBalance = " + String(creditBalance) + ", payout = " + String(payout) + ", newBalance = " + String(newBalance));
  creditBalance = animateBank(creditBalance, newBalance, DIGIT_DELAY_TIME, (abs(creditBalance - newBalance) > 30) ? bet : 1);
  if (creditBalance < 0)
  {
    playLoseSound();
    creditBalance = animateBank(creditBalance, STARTING_CREDIT_BALANCE, DIGIT_DELAY_TIME, 10);
  }
  slotPlayMode = SLOT_GETBET;
}

unsigned long spinTheWheels()
{
  unsigned long payout = 0;
  //Reset wheels for the spin
  unsigned long totalTime = millis();
  for (uint8_t j = 0; j < WHEELS; j++)
  {
    totalTime = totalTime + random(MIN_SPIN_TIME, MAX_SPIN_TIME);
    spin[j].delayTime = START_DELAY_TIME;
    spin[j].spinTime = totalTime;
    spin[j].frameTime = millis() + spin[j].delayTime;
    spin[j].stopped = false;
  }
  
  bool allStopped = false;
  while (!allStopped)
  {
    //Scroll each symbol up
    for (uint8_t j = 0; j < WHEELS; j++)
    {
      if (!spin[j].stopped && millis() > spin[j].frameTime)
      {
        spin[j].frameTime = millis() + spin[j].delayTime;

        displayWheelSymbol(j);
        spin[j].row = (spin[j].row + 1) % TOTAL_REEL_SYMBOL_ROWS;

        beepWheel();
        
        if (millis() > spin[j].spinTime)
        {
          //Stop if delayTime exceeds MAX_DELAY_BEFORE_STOP
          //Only stop on complete symbol
          if (spin[j].delayTime > MAX_DELAY_BEFORE_STOP && (spin[j].row % 8) == 1)
          {
            spin[j].stopped = true;
            spin[j].symbol = spin[j].row >> 3;
            if (j == (WHEELS - 1))
            {
              //All wheels are now stopped
              allStopped = true;
              payout = highlightWinAndCalculatePayout();
            }
          }
          else if (spin[j].delayTime <= MAX_DELAY_BEFORE_STOP)
          {
            spin[j].delayTime = spin[j].delayTime + INCREMENT_DELAY_TIME;
          }
        }
      }
    }
    yield();
  }
  return payout;
}

unsigned long highlightWinAndCalculatePayout()
{
  unsigned long payout = 0;
  uint8_t matches = 0;
  uint8_t symbol = 255;
  uint8_t sevens = 0;
  uint8_t faces = 0;
  uint8_t bestMatch = 0;
  for (uint8_t y = 0; y < WHEELS; y++)
  {
    matches = 1;
    if (spin[y].symbol == FACE)
    {
      faces++;
      if (symbol == 255)
      {
        symbol = FACE;
      }
    }
    else if (spin[y].symbol == SEVEN)
    {
      sevens++;
      matches = sevens;
      symbol = SEVEN;
    }
    else if (sevens == 0)
    {
      for (uint8_t x = 0; x < WHEELS; x++)
      {
        if (spin[y].symbol == spin[x].symbol && y != x)
        {
          matches++;
          symbol = spin[y].symbol;
        }
      }
      if (symbol == 255 || symbol == FACE)
      {
        symbol = spin[y].symbol;
      }
    }
    if (matches > bestMatch)
    {
      bestMatch = matches;
    }
  }
  Serial.println("Matches " + String(bestMatch) + ", Sevens " + String(sevens) + ", Faces " + String(faces) + ", Symbol " + String(symbol));
  if (bestMatch == 0 && faces > 0)
  {
    bestMatch = 1;
  }
  if (sevens > 0)
  {
    sevens += faces;
  }
  bestMatch += faces;
  if (bestMatch > 0)
  {
    switch (sevens)
    {
      case 3: payout = THREE_SEVEN_PAYOUT; playWinSoundLong(5); break;
      case 2: payout = TWO_SEVEN_PAYOUT; playWinSoundLong(3); break;
      case 1: payout = ONE_SEVEN_PAYOUT; playWinSoundLong(2); break;
      default:
        switch (bestMatch)
        {
          case 3: payout = THREE_SYMBOL_PAYOUT; playWinSoundLong(4); break;
          case 2: payout = TWO_SYMBOL_PAYOUT; playWinSoundLong(1); break;
        }
        break;
    }
    if (faces > 0 && faces < 3)
    {
      payout = payout >> 1;
    }
    if (payout > 0)
    {
      flashSymbol(symbol);
    }
  }
  return payout;
}

void flashSymbol(uint8_t symbol)
{
  Serial.println("Flashing " + String(symbol));
  bool on = true;
  uint8_t row = symbol << 3;
  for (uint8_t r = 0; r < FLASH_REPEAT; r++)
  {
    for (uint8_t j = 0; j < WHEELS; j++)
    {
      if (spin[j].symbol == symbol)
      {
        for (int8_t i = 7; i >= 0; i--)
        {
          reels[j][i] = (on) ? 0 : getReelRow((row + i) % TOTAL_REEL_SYMBOL_ROWS);
        }
        lcd.createChar(j, &reels[j][0]);
        lcd.setCursor(j * 5 + 2, 0);
        lcd.write(j);
      }
      else if (spin[j].symbol == FACE)
      {
        for (int8_t i = 7; i >= 0; i--)
        {
          reels[j][i] = (on) ? 0 : getReelRow(((FACE << 3) + i) % TOTAL_REEL_SYMBOL_ROWS);
        }
        lcd.createChar(j, &reels[j][0]);
        lcd.setCursor(j * 5 + 2, 0);
        lcd.write(j);
      }
    }
    on = !on;
    delay(FLASH_TIME);
  }
}

void displayWheelSymbol(int wheel)
{
  for (int8_t i = 7; i >= 0; i--)
  {
    reels[wheel][i] = getReelRow((spin[wheel].row + i) % TOTAL_REEL_SYMBOL_ROWS);
  }
  lcd.createChar(wheel, &reels[wheel][0]);
  lcd.setCursor(wheel * 5 + 2, 0);
  lcd.write(wheel);
}

uint8_t getReelRow(uint8_t row)
{
  #ifdef REELS_IN_FLASH
    return pgm_read_byte(reel + row);
  #else
    return reel[row];
  #endif
}
