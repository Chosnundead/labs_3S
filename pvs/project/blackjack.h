#include "Hardware.h"
#include "Game.h"
#include "Symbols.h"

char strBlackjack[] = "Blackjack";
char strHitOrStand[] = "H or S?";
char strPlayer[] = "Player ";
char strDealer[] = "Dealer ";
char strWins[] = "Wins";
char strLoses[] = "Loses";
char strBusts[] = "Busts";
char strDraw[] = "Draw";
char strYouWin[] = "You Win ";
char strYouLose[] = "You Lose ";
char strTappedOut[] = "Tapped out!";
char strYourBet[] = "Your Bet? ";
char strShuffling[] = "Shuffling";

enum bjPlayModeEnum { BJ_NOINPUT, BJ_GETBET, BJ_HITORSTAND, BJ_STARTOVER };
enum bjDealModeEnum { DEALER_FIRST, PLAYER_FIRST, DEALER_SECOND, PLAYER_SECOND, PLAYER_NEXT, DEALER_NEXT };

#define TOTAL_DECK_CARDS 52
#define TOTAL_HAND_CARDS 7

byte deck[TOTAL_DECK_CARDS];    //Holds current pack
byte nextCardToDraw = 0;        //Next card to draw from pack
byte player[TOTAL_HAND_CARDS];  //Player cards
byte nextPlayerCard = 0;        //Next card player will be dealt
byte dealer[TOTAL_HAND_CARDS];  //Dealer cards
byte nextDealerCard = 0;        //Next card dealer will be dealt
bool hideDealerCard = true;     //Whether dealers first card is hidden
bjDealModeEnum bjDealMode;      //Current card being dealt
bjPlayModeEnum bjPlayMode;      //Current mode of play

void blackjackSetup();
bool blackjackLoop();
void startGame();
void processHitStand();
void processPlayerHit(byte card);
void dealerHit(); 
void processDealerHit(byte card);
void dealInitialCards(byte card);
void dealCard();
void displayHands();
void displayHand(byte* hand, byte nextCard, bool hideFirstCard);
byte countHand(byte* hand, byte nextCard);
void processTestCards();
void playerBlackjack();
void playerWin(); 
void playerBust(); 
void dealerBlackjack();
void dealerWin();
void dealerBust(); 
void playerDraw(); 
void shuffleDeck();
void showBlackjackSplashScreen();

void blackjackSetup()
{
  //Define all the new card characters
  lcd.createChar(HEART, &cards[HEART][0]);
  lcd.createChar(DIAMOND, &cards[DIAMOND][0]);
  lcd.createChar(SPADE, &cards[SPADE][0]);
  lcd.createChar(CLUB, &cards[CLUB][0]);
  lcd.createChar(BACK_1, &cards[BACK_1][0]);
  lcd.createChar(BACK_2, &cards[BACK_2][0]);
  lcd.createChar(TEN, &cards[TEN][0]);

  //Initialise deck of cards
  for (byte i = 0; i < TOTAL_DECK_CARDS; i++)
  {
    deck[i] = i;
  }

  showBlackjackSplashScreen();
  randomSeed(millis());
  shuffleDeck();
  startGame();
}

bool blackjackLoop()
{
  //Get button pressed
  lastButtonPressed = buttonPressed(NULL);
  while ((lastButtonPressed != BTN_HIT || bjPlayMode != BJ_GETBET) && lastButtonPressed != BTN_NONE)
  {
    Serial.println("bjPlayMode=" + String(bjPlayMode) + ", lastButtonPressed=" + String(lastButtonPressed));
    switch (bjPlayMode) 
    {
      case BJ_GETBET: 
        if (processGetBet() == BTN_STAND)
        {
          bjPlayMode == BJ_NOINPUT;
          // Deal initial hand.
          bjDealMode = DEALER_FIRST;
          dealCard();
        }
        break;
        
      case BJ_HITORSTAND: processHitStand(); break;
      case BJ_STARTOVER: startGame(); break;
    }
    lastButtonPressed = buttonPressed(NULL);
  }
  return (lastButtonPressed == BTN_HIT);
}
  
void startGame()
{
  nextDealerCard = 0;
  nextPlayerCard = 0;
  lastButtonPressed = BTN_NONE;
  lcd.clear();
  if (creditBalance < BET_MIN) 
  {
    // Yes, Hope you have more in the ATM.
    lcd.clear();
    lcd.print(strTappedOut);
    creditBalance = animateBank(creditBalance, STARTING_CREDIT_BALANCE, DIGIT_DELAY_TIME, 50);
  }
  creditBalance = min(creditBalance, 99999);
  lcd.clear();
  lcd.print(strYourBet);
  animateBank(creditBalance, creditBalance, DIGIT_DELAY_TIME, 50);
  displayBet(bet);
  bjPlayMode = BJ_GETBET;
}

void processHitStand() 
{
  switch (lastButtonPressed) 
  {
    case BTN_HIT:
      bjDealMode = PLAYER_NEXT;
      dealCard();
      break;

    case BTN_STAND:
      bjPlayMode = BJ_NOINPUT;
      hideDealerCard = false;
      dealerHit();
      break;

    case BTN_LESS:
    case BTN_MORE:
      playBadKeySound();
      break;
  }
}

void processPlayerHit(byte card) 
{
  player[nextPlayerCard] = card;
  nextPlayerCard++;
  displayHands();
  lcd.setCursor(9, 0);
  lcd.print(strHitOrStand);
  int playerCount = countHand(player, nextPlayerCard);
  if (playerCount > 21) 
  {
    hideDealerCard = false;
    displayHands();
    int dealerCount = countHand(dealer, nextDealerCard);
    displayCount(dealerCount, 0);
    displayCount(playerCount, 1);
    bjPlayMode = BJ_NOINPUT;
    delay(2000);
    playerBust();
  }
}

void dealerHit() 
{
  //Play out dealers hand - dealer must draw on 16 or less
  int dealerCount = countHand(dealer, nextDealerCard);
  int playerCount = countHand(player, nextPlayerCard);
  if (dealerCount < 17) 
  {
    bjDealMode = DEALER_NEXT;
    dealCard();
  }
  else 
  {
    displayHands();
    displayCount(dealerCount, 0);
    displayCount(playerCount, 1);
    delay(2000);
    if (dealerCount > 21)
      dealerBust();
    else if (dealerCount > playerCount)
      dealerWin();
    else if (dealerCount < playerCount)
      playerWin();
    else
      playerDraw();
  }
}

void processDealerHit(byte card) 
{
  dealer[nextDealerCard] = card;
  nextDealerCard++;
  displayHands();
  delay(1000);
  dealerHit();
}

void dealInitialCards(byte card) 
{
  // Deal card.
  if (bjDealMode == DEALER_FIRST)
  {
    hideDealerCard = true;
  }
  if (((int)bjDealMode & 1) == 0) 
  {
    dealer[nextDealerCard] = card;
    nextDealerCard++;
  }
  else {
    player[nextPlayerCard] = card;
    nextPlayerCard++;
  }
  displayHands();
  bjDealMode = (bjDealModeEnum)((int)bjDealMode + 1);
  
  if (bjDealMode == PLAYER_NEXT)
    processTestCards();
  else
    dealCard();
};

void dealCard() 
{
  byte card = deck[nextCardToDraw];
  nextCardToDraw++;
  if (nextCardToDraw >= TOTAL_DECK_CARDS) 
    shuffleDeck();
  if (bjDealMode == PLAYER_NEXT)
    processPlayerHit(card);
  else if (bjDealMode == DEALER_NEXT)
    processDealerHit(card);
  else
    dealInitialCards(card);
};

void displayHands() 
{
  // Display dealer's hand.
  lcd.clear();
  lcd.print("D:");
  displayHand(dealer, nextDealerCard, hideDealerCard);

  // Display player's hand.
  lcd.setCursor(0, 1);
  lcd.print("P:");
  displayHand(player, nextPlayerCard, false);
}

void displayHand(byte* hand, byte nextCard, bool hideFirstCard) 
{
  // Display cards.
  for (byte card = 0; card < nextCard; card++) 
  {
    if (hideFirstCard && card == 0) 
    {
      //Show back of first card.
      lcd.write(BACK_1);
      lcd.write(BACK_2);
    }
    else 
    {
      byte suit = hand[card] / 13;
      byte face = hand[card] % 13;
      // No, display card rank.
      switch (face)
      {
        case 0: lcd.print("A"); break;
        case 9: lcd.write(TEN); break;
        case 10: lcd.print("J"); break;
        case 11: lcd.print("Q"); break;
        case 12: lcd.print("K"); break;
        default: lcd.print(face + 1);
      }
      // Display card suit.  
      lcd.write(suit);
    }
  }
}

byte countHand(byte* hand, byte nextCard) 
{
  byte score = 0;
  byte aces = 0;
  
  //Sum all the cards counting aces as 11 and keeping track of how many are present
  for (byte card = 0; card < nextCard; card++) 
  {
    byte face = hand[card] % 13;
    if (face == 0)
    {
      aces++;
    }
    score += (face == 0) ? 11 : (face > 9) ? 10 : (face + 1);
  }

  //If over 21 and we have aces, start treating the ace value as 1 and see if we can
  //get under 21;
  while (score > 21 && aces > 0)
  {
    score = score - 10;
    aces--;
  }
  return score;
}

void processTestCards () 
{
  if (nextDealerCard == 2 && countHand(dealer, nextDealerCard) == 21) 
  {
    //Dealer has black jack
    hideDealerCard = false;
    displayHands();
    lcd.setCursor(7, 0);
    lcd.print(strBlackjack);
    // Give player the bad news.
    delay(2000);
    dealerBlackjack();
  }
  else if (nextPlayerCard == 2 && countHand(player, nextPlayerCard) == 21) 
  {
    hideDealerCard = false;
    displayHands();
    lcd.setCursor(7, 1);
    lcd.print(strBlackjack);
    delay(2000);
    playerBlackjack();
  }
  else {
    //Accept hits until player stands or busts
    lcd.setCursor(9, 0);
    lcd.print(strHitOrStand);
    bjPlayMode = BJ_HITORSTAND;
  }
}

void playerBlackjack() 
{
  lcd.clear();
  lcd.print(strPlayer);
  lcd.print(strBlackjack);
  displayBankAndBet(creditBalance, bet);
  creditBalance = animateBank(creditBalance, creditBalance - bet * 1.5, DIGIT_DELAY_TIME, 1);
  playWinSoundShort();
  lastButtonPressed = BTN_NONE;
  bjPlayMode = BJ_STARTOVER;
};

void playerWin() 
{
  lcd.clear();
  lcd.print(strPlayer);
  lcd.print(strWins);
  displayBankAndBet(creditBalance, bet);
  creditBalance = animateBank(creditBalance, creditBalance + bet, DIGIT_DELAY_TIME, 1);
  playWinSoundShort();
  lastButtonPressed = BTN_NONE;
  bjPlayMode = BJ_STARTOVER;
};

void playerBust() 
{
  lcd.clear();
  lcd.print(strPlayer);
  lcd.print(strBusts);
  displayBankAndBet(creditBalance, bet);
  creditBalance = animateBank(creditBalance, creditBalance - bet, DIGIT_DELAY_TIME, 1);
  playLoseSound();
  lastButtonPressed = BTN_NONE;
  bjPlayMode = BJ_STARTOVER;
};

void dealerBlackjack() 
{
  // Player Blackjack?
  lcd.setCursor(7, 1);
  if ((nextPlayerCard == 2) && (countHand(player, nextPlayerCard) == 21)) {
    // Yes, Push.
    lcd.print(strDraw);
  }
  else {
    // No, Loser.
    lcd.print(strYouLose);
    creditBalance -= bet;
    playLoseSound();
  }
  lastButtonPressed = BTN_NONE;
  bjPlayMode = BJ_STARTOVER;
};

void dealerWin() 
{
  // Dealer wins.
  lcd.clear();
  lcd.print(strDealer);
  lcd.print(strWins);
  displayBankAndBet(creditBalance, bet);
  creditBalance = animateBank(creditBalance, creditBalance - bet, DIGIT_DELAY_TIME, 1);
  playLoseSound();
  lastButtonPressed = BTN_NONE;
  bjPlayMode = BJ_STARTOVER;
};

void dealerBust() 
{
  lcd.clear();
  lcd.print(strDealer);
  lcd.print(strBusts);
  displayBankAndBet(creditBalance, bet);
  creditBalance = animateBank(creditBalance, creditBalance + bet, DIGIT_DELAY_TIME, 1);
  playWinSoundShort();
  lastButtonPressed = BTN_NONE;
  bjPlayMode = BJ_STARTOVER;
};

void playerDraw() 
{
  lcd.clear();
  lcd.print(strDraw);
  displayBankAndBet(creditBalance, bet);
  lastButtonPressed = BTN_NONE;
  bjPlayMode = BJ_STARTOVER;
};

void shuffleDeck()
{
  lcd.clear();
  lcd.print(strShuffling);
  // Swap each card in the deck with a random card.
  for (byte cardShuffle = 0; cardShuffle < TOTAL_DECK_CARDS ; cardShuffle++) 
  {
    // Init random card to swap (could be the same card).
    byte cardRandom = random(0, TOTAL_DECK_CARDS);
    byte t =  deck[cardShuffle];
    deck[cardShuffle] = deck[cardRandom];
    deck[cardRandom] = t;
  }
  nextCardToDraw = 0;
  // Slow humans.
  delay(2000);
  // Display hands (to get rid of "Shuffling" message).
  displayHands();
}

void showBlackjackSplashScreen()
{
  lcd.clear();
  lcd.setCursor(0, 0);
  lcd.print("** ");
  lcd.print(strBlackjack);
  lcd.print(" **");
  lcd.setCursor(5, 1);
  lcd.write(BACK_1);
  lcd.write(BACK_2);
  delay(1000);
  
  lcd.setCursor(9, 1);
  lcd.write(BACK_1);
  lcd.write(BACK_2);
  delay(1000);
  
  lcd.setCursor(5, 1);
  lcd.print("J");
  lcd.write(DIAMOND);
  delay(1000);
  
  lcd.setCursor(9, 1);
  lcd.print("A");
  lcd.write(SPADE);
  delay(1000);
  
  lcd.setCursor(0, 0);
  lcd.print("   ");
  lcd.print(strBlackjack);
  lcd.print("!   ");
  
  int animDelay = 10;
  while (animDelay < 31)
  {
    animDelay = animDelay + 1;
    if ((animDelay % 2) > 0)
      lcd.display();
    else
      lcd.noDisplay();
    delay((31 - animDelay) * (31 - animDelay));
  };
  delay(1000);
};
