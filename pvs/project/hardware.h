#pragma once

#include <LiquidCrystal.h>
#include <TimerFreeTone.h>  // https://bitbucket.org/teckel12/arduino-timer-free-tone/wiki/Home

//LCD Screen
#define LCD_RS 9
#define LCD_EN 10
#define LCD_D4 12
#define LCD_D5 13
#define LCD_D6 A1
#define LCD_D7 A2

#define HIT_PIN 5
#define STAND_PIN 4
#define LESS_PIN 3
#define MORE_PIN 2

#define TONE_PIN 11
#define BUZZER_DDR  DDRB
#define BUZZER_PORT PORTB
#define BUZZER_PIN  DDB3

enum buttonEnum { BTN_NONE, BTN_HIT, BTN_STAND, BTN_LESS, BTN_MORE };

//Initialize the LCD
LiquidCrystal lcd(LCD_RS, LCD_EN, LCD_D4, LCD_D5, LCD_D6, LCD_D7);

void hardwareSetup();
void lcdClearRow(int row);
buttonEnum buttonPressed(void (*pBackgroundFunction)());
bool IsSinglePress(int pin, void (*pBackgroundFunction)());
void beepWheel();
void beepDigit(); 
void playSiren();
void playRollTone();
void playRolledTone();
void playBadKeySound();
void playWinSoundLong(uint8_t repeat);
void playWinSoundShort();
void playLoseSound();

void hardwareSetup()
{
  Serial.begin(115200);
  
  pinMode(TONE_PIN, OUTPUT);
  pinMode(HIT_PIN, INPUT_PULLUP);
  pinMode(STAND_PIN, INPUT_PULLUP);
  pinMode(LESS_PIN, INPUT_PULLUP);
  pinMode(MORE_PIN, INPUT_PULLUP);

  //Setup random seed
  randomSeed(analogRead(A0));
  
  // set up the LCD's number of columns and rows:
  lcd.begin(16, 2);
  lcd.clear();
}

void lcdClearRow(int row) 
{
  if (row >= 0 && row < 2) 
  {
    lcd.setCursor(0, row);
    for (int x = 0; x < 16; x++) 
    {
      lcd.print(" ");
    }
    lcd.setCursor(0, row);
  }
};

buttonEnum buttonPressed(void (*pBackgroundFunction)())
{
  if (IsSinglePress(HIT_PIN, pBackgroundFunction))
    return BTN_HIT;
  else if (IsSinglePress(STAND_PIN, pBackgroundFunction))
    return BTN_STAND;
  else if (IsSinglePress(MORE_PIN, pBackgroundFunction))
    return BTN_MORE;
  else if (IsSinglePress(LESS_PIN, pBackgroundFunction))
    return BTN_LESS;
  else
    return BTN_NONE;
}

bool IsSinglePress(int pin, void (*pBackgroundFunction)())
{
  bool pressed = false;
  if (digitalRead(pin) == LOW)
  {
    delay(10);
    if (digitalRead(pin) == LOW)
    {
      while (digitalRead(pin) == LOW)
      {
        if (pBackgroundFunction != NULL)
        {
          pBackgroundFunction();
        }
      }
      pressed = true;
    }
  }
  return pressed;
}

void beepWheel() 
{                                     // Beep and flash LED green unless STATE_AUTO
  BUZZER_PORT |= (1 << BUZZER_PIN);   // turn on buzzer
  delay(20);
  BUZZER_PORT &= ~(1 << BUZZER_PIN);  // turn off the buzzer
}

void beepDigit() 
{                                     // Beep and flash LED green unless STATE_AUTO
  BUZZER_PORT |= (1 << BUZZER_PIN);   // turn on buzzer
  delay(5);
  BUZZER_PORT &= ~(1 << BUZZER_PIN);  // turn off the buzzer
}

void playSiren() 
{
  #define MAX_NOTE 4978               // Maximum high tone in hertz. Used for siren.
  #define MIN_NOTE 31                 // Minimum low tone in hertz. Used for siren.
  
  for (int note = MIN_NOTE; note <= MAX_NOTE; note += 5)
  {                       
    TimerFreeTone(TONE_PIN, note, 1);
  }
}

void playRollTone()
{
  TimerFreeTone(TONE_PIN, 300,50); 
}

void playRolledTone()
{
  TimerFreeTone(TONE_PIN, 600, 150);
}

void playBadKeySound()
{
  TimerFreeTone(TONE_PIN, 440,100);
}

void playWinSoundLong(uint8_t repeat)
{
  for (uint8_t i = 0; i < repeat; i++)
  {
    playSiren();
  }
}

void playWinSoundShort()
{
  //TimerFreeTone(TONE_PIN,880,300);
  TimerFreeTone(TONE_PIN,880,100); //A5
  TimerFreeTone(TONE_PIN,988,100); //B5
  TimerFreeTone(TONE_PIN,523,100); //C5
  TimerFreeTone(TONE_PIN,988,100); //B5
  TimerFreeTone(TONE_PIN,523,100); //C5
  TimerFreeTone(TONE_PIN,587,100); //D5
  TimerFreeTone(TONE_PIN,523,100); //C5
  TimerFreeTone(TONE_PIN,587,100); //D5
  TimerFreeTone(TONE_PIN,659,100); //E5
  TimerFreeTone(TONE_PIN,587,100); //D5
  TimerFreeTone(TONE_PIN,659,100); //E5
  TimerFreeTone(TONE_PIN,659,100); //E5
  delay(250);
}

void playLoseSound()
{
  delay(400);
  for(double wah=0; wah<4; wah+=6.541)
  {
    TimerFreeTone(TONE_PIN, 440+wah, 50);
  }
  TimerFreeTone(TONE_PIN, 466.164, 100);
  delay(80);
  for(double wah=0; wah<5; wah+=4.939)
  {
    TimerFreeTone(TONE_PIN, 415.305+wah, 50);
  }
  TimerFreeTone(TONE_PIN, 440.000, 100);
  delay(80);
  for(double wah=0; wah<5; wah+=4.662)
  {
    TimerFreeTone(TONE_PIN, 391.995+wah, 50);
  }
  TimerFreeTone(TONE_PIN, 415.305, 100);
  delay(80);
  for(int j=0; j<7; j++)
  {
    TimerFreeTone(TONE_PIN, 391.995, 70);
    TimerFreeTone(TONE_PIN, 415.305, 70);
  }
  delay(400);
}
