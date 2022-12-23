#pragma once

#define BET_INC 2
#define BET_MIN 2
#define BET_MAX 20
int bet = BET_MIN;              //Amount been wagered

#define DIGIT_DELAY_TIME 50

#define STARTING_CREDIT_BALANCE 1000    // Number of credits you have at "factory reset".
long creditBalance = 0;                 // Current balance
buttonEnum lastButtonPressed;   //Stores the last button pressed

buttonEnum processGetBet(); 
void displayBankAndBet(long bank, int bet);
long animateBank(long oldBalance, long newBalance, long delayTime, int rate);
void displayBet(int bet);
void displayCount(int count, int line);
void displayNumber(int x, int y, long number, int padding, bool leadingZeros);

buttonEnum processGetBet() 
{
  // Prompt user.
  switch (lastButtonPressed) 
  {
    case BTN_MORE:
      if ((bet < BET_MAX) && (bet < creditBalance))
        bet += BET_INC;
      else
        playBadKeySound();
      break;

    case BTN_LESS:
      if (bet > BET_MIN)
        bet -= BET_INC;
      else
        playBadKeySound();
      break;

    case BTN_STAND:
      if (bet > BET_MAX || bet > creditBalance) 
      {
        lastButtonPressed = BTN_NONE;
        playBadKeySound();
      }
      break;
  }
  displayBet(bet);
  return lastButtonPressed;
};

void displayBankAndBet(long bank, int bet)
{
  lcdClearRow(1);
  animateBank(bank, bank, DIGIT_DELAY_TIME, 50);
  displayBet(bet);
};

long animateBank(long oldBalance, long newBalance, long delayTime, int rate)
{
  unsigned int difference;
  int8_t direction;
  if (oldBalance != newBalance)
  {
    if (oldBalance > newBalance)
    {
      difference = oldBalance - newBalance;
      direction = -1;
    }
    else
    {
      difference = newBalance - oldBalance;
      direction = 1;
    }
    
    for (unsigned int i = 0; i < difference; i += rate)
    {
      oldBalance = oldBalance + direction * rate;
      lcd.setCursor(0, 1);
      lcd.print((oldBalance >= 0) ? " " : "-");
      lcd.print("$     ");
      displayNumber(2, 1, abs(oldBalance), 0, false);
      beepDigit();
      delay(delayTime);
    }
  }
  lcd.setCursor(0, 1);
  lcd.print((newBalance >= 0) ? " " : "-");
  lcd.print("$     ");
  displayNumber(2, 1, abs(newBalance), 0, false);
  return newBalance;
}

void displayBet(int bet)
{
  lcd.setCursor(9,1);
  lcd.print("Bet:$  ");
  lcd.setCursor(14,1);
  lcd.print(bet);
}

void displayCount(int count, int line) 
{
  if (count > 9)
    lcd.setCursor(14, line);
  else
    lcd.setCursor(15, line);
  lcd.print(count);
};

void displayNumber(int x, int y, long number, int padding, bool leadingZeros)
{
  bool negative = (number < 0);
  number = abs(number);
  if (padding == 0)
  {
    padding = (number > 0) ? floor(log10(number)) + 1 : 1;
  }
  x = x + padding + ((negative) ? 1 : 0);
  bool first = true;
  for (int i = 0; i < padding; i++)
  {
    x--;
    lcd.setCursor(x, y);
    if (number != 0 || first)
    {
      lcd.print(number % 10);
      number = number / 10;
      first = false;
    }
    else if (negative && !leadingZeros)
    {
      lcd.print("-");
      negative = false;
    }
  else
    {
      lcd.print((leadingZeros) ? "0" : " ");
    }
  }
  if (negative && leadingZeros)
  {
    x--;
    lcd.setCursor(x, y);
    lcd.print("-");
  }
}
