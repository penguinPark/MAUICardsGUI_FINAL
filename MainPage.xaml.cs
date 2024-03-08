using Microsoft.UI.Xaml.Controls;
using RaceTo21;
using System.Reflection;
using static System.Formats.Asn1.AsnWriter;

namespace MAUICardsGUI;
public partial class MainPage : ContentPage
{
    // need to have draw and stay work
    // need to start new game if people want to
    // scoring and end of game should work
    int numOfPlayers; // number of players in current game
    List<Player> players = new List<Player>(); // list of objects containing player data
    Deck deck = new Deck(); // deck of cards
    int currentPlayer = 0; // current player on list
    public RaceTo21.Task nextTask = RaceTo21.Task.GetNumberOfPlayers; // keeps track of game state through enum Task
    Player previousWinner; // to keep track of the player who won
    public int winningScore; // variable to represent the winning total score
    public bool finishedTask = false;
    public string response;
    int count; // counter
    int fullNumber = 0; // full winning score number
    int numResponse; // response
    int intro = 0;
    List<Label> FinalScores;
    public MainPage()
    {
        InitializeComponent();
        FinalScores = new List<Label> { FinalTotalScore, FinalTotalScore2, FinalTotalScore3, FinalTotalScore4, FinalTotalScore5, FinalTotalScore6, FinalTotalScore7, FinalTotalScore8 };
    }

    public void DoNextTask()
    {
        if (nextTask == RaceTo21.Task.GetNumberOfPlayers) // this is to get the number of players in the game
        {
            StartButton.IsVisible = false;
            gameLabel.Text = "How many players?";
            NumberOfPlayers.IsVisible = true;
            NumberPlayersButton.IsVisible = true;
        }
        else if (nextTask == RaceTo21.Task.GetNames)
        {
            InvalidNumbersText.IsVisible = false;
            NumberOfPlayers.IsVisible = false;
            NumberPlayersButton.IsVisible = false;
            gameLabel.Text = "What is your name?";
            NamesOfPlayers.IsVisible = true;
            NameButton.IsVisible = true;
        }
        else if (nextTask == RaceTo21.Task.AgreedScore)
        {
            InvalidName.IsVisible = false;
            NamesOfPlayers.IsVisible = false;
            NameButton.IsVisible = false;
            TotalWinningScore.IsVisible = true;
            ScoreButton.IsVisible = true;
            gameLabel.Text = "What does " + players[intro].Name + " want the winning score to be?";

        }
        else if (nextTask == RaceTo21.Task.Intro)
        {
            deck.Shuffle();
            InvalidScore.IsVisible = false;
            TotalWinningScore.IsVisible = false;
            ScoreButton.IsVisible = false;
            gameLabel.IsVisible = false;
            TotalScore.IsVisible = true;
            NonScoreButton.IsVisible = true;
            TotalScore.Text = "Total Winning Score: " + winningScore;
            while (currentPlayer < players.Count)
            {
                PlayersNames1.IsVisible = true;
                PlayersNames1.Text = "Player #" + (currentPlayer + 1) + " Name: " + players[currentPlayer].Name + "      Game Score: " + players[currentPlayer].score;
                currentPlayer++;
                while (currentPlayer < players.Count)
                {
                    PlayersNames2.IsVisible = true;
                    PlayersNames2.Text = "Player #" + (currentPlayer + 1) + " Name: " + players[currentPlayer].Name + "      Game Score: " + players[currentPlayer].score;
                    currentPlayer++;
                    while (currentPlayer < players.Count)
                    {
                        PlayersNames3.IsVisible = true;
                        PlayersNames3.Text = "Player #" + (currentPlayer + 1) + " Name: " + players[currentPlayer].Name + "      Game Score: " + players[currentPlayer].score;
                        currentPlayer++;
                        while (currentPlayer < players.Count)
                        {
                            PlayersNames4.IsVisible = true;
                            PlayersNames4.Text = "Player #" + (currentPlayer + 1) + " Name: " + players[currentPlayer].Name + "      Game Score: " + players[currentPlayer].score;
                            currentPlayer++;
                            while (currentPlayer < players.Count)
                            {
                                PlayersNames5.IsVisible = true;
                                PlayersNames5.Text = "Player #" + (currentPlayer + 1) + " Name: " + players[currentPlayer].Name + "      Game Score: " + players[currentPlayer].score;
                                currentPlayer++;
                                while (currentPlayer < players.Count)
                                {
                                    PlayersNames6.IsVisible = true;
                                    PlayersNames6.Text = "Player #" + (currentPlayer + 1) + " Name: " + players[currentPlayer].Name + "      Game Score: " + players[currentPlayer].score;
                                    currentPlayer++;
                                    while (currentPlayer < players.Count)
                                    {
                                        PlayersNames7.IsVisible = true;
                                        PlayersNames7.Text = "Player #" + (currentPlayer + 1) + " Name: " + players[currentPlayer].Name + "       Game Score: " + players[currentPlayer].score;
                                        currentPlayer++;
                                        while (currentPlayer < players.Count)
                                        {
                                            PlayersNames8.IsVisible = true;
                                            PlayersNames8.Text = "Player #" + (currentPlayer + 1) + " Name: " + players[currentPlayer].Name + "       Game Score: " + players[currentPlayer].score;
                                            currentPlayer++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        else if (nextTask == RaceTo21.Task.FirstTurn)
        {
          
            NonScoreButton.IsVisible = false;
            DrawScoreButton.IsVisible = true;
            DrawCard.IsVisible = true;
            StayScoreButton.IsVisible = true;
            DrawCard.Text = "Do you want to draw a card " + players[intro].Name;
            currentPlayer = 0;
            if (players[currentPlayer].cards.Count == 0)
            {
                while (currentPlayer < players.Count)
                {
                    Card card = deck.DealTopCard();
                    players[currentPlayer].cards.Add(card);
                    players[currentPlayer].score = ScoreHand(players[currentPlayer]);
                    PlayersNames1.IsVisible = true;
                    PlayersNames1.Text = "Player #" + (currentPlayer + 1) + " Name: " + players[currentPlayer].Name + "      Game Score: " + players[currentPlayer].score;
                    currentPlayer++;
                    while (currentPlayer < players.Count)
                    {
                        card = deck.DealTopCard();
                        players[currentPlayer].cards.Add(card);
                        players[currentPlayer].score = ScoreHand(players[currentPlayer]);
                        PlayersNames2.IsVisible = true;
                        PlayersNames2.Text = "Player #" + (currentPlayer + 1) + " Name: " + players[currentPlayer].Name + "      Game Score: " + players[currentPlayer].score;
                        currentPlayer++;
                        while (currentPlayer < players.Count)
                        {
                            card = deck.DealTopCard();
                            players[currentPlayer].cards.Add(card);
                            players[currentPlayer].score = ScoreHand(players[currentPlayer]);
                            PlayersNames3.IsVisible = true;
                            PlayersNames3.Text = "Player #" + (currentPlayer + 1) + " Name: " + players[currentPlayer].Name + "      Game Score: " + players[currentPlayer].score;
                            currentPlayer++;
                            while (currentPlayer < players.Count)
                            {
                                card = deck.DealTopCard();
                                players[currentPlayer].cards.Add(card);
                                players[currentPlayer].score = ScoreHand(players[currentPlayer]);
                                PlayersNames4.IsVisible = true;
                                PlayersNames4.Text = "Player #" + (currentPlayer + 1) + " Name: " + players[currentPlayer].Name + "      Game Score: " + players[currentPlayer].score;
                                currentPlayer++;
                                while (currentPlayer < players.Count)
                                {
                                    card = deck.DealTopCard();
                                    players[currentPlayer].cards.Add(card);
                                    players[currentPlayer].score = ScoreHand(players[currentPlayer]);
                                    PlayersNames5.IsVisible = true;
                                    PlayersNames5.Text = "Player #" + (currentPlayer + 1) + " Name: " + players[currentPlayer].Name + "      Game Score: " + players[currentPlayer].score;
                                    currentPlayer++;
                                    while (currentPlayer < players.Count)
                                    {
                                        card = deck.DealTopCard();
                                        players[currentPlayer].cards.Add(card);
                                        players[currentPlayer].score = ScoreHand(players[currentPlayer]);
                                        PlayersNames6.IsVisible = true;
                                        PlayersNames6.Text = "Player #" + (currentPlayer + 1) + " Name: " + players[currentPlayer].Name + "      Game Score: " + players[currentPlayer].score;
                                        currentPlayer++;
                                        while (currentPlayer < players.Count)
                                        {
                                            card = deck.DealTopCard();
                                            players[currentPlayer].cards.Add(card);
                                            players[currentPlayer].score = ScoreHand(players[currentPlayer]);
                                            PlayersNames7.IsVisible = true;
                                            PlayersNames7.Text = "Player #" + (currentPlayer + 1) + " Name: " + players[currentPlayer].Name + "      Game Score: " + players[currentPlayer].score;
                                            currentPlayer++;
                                            while (currentPlayer < players.Count)
                                            {
                                                card = deck.DealTopCard();
                                                players[currentPlayer].cards.Add(card);
                                                players[currentPlayer].score = ScoreHand(players[currentPlayer]);
                                                PlayersNames8.IsVisible = true;
                                                PlayersNames8.Text = "Player #" + (currentPlayer + 1) + " Name: " + players[currentPlayer].Name + "      Game Score: " + players[currentPlayer].score;
                                                currentPlayer++;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            } FirstCard();
        }
        else if (nextTask == RaceTo21.Task.PlayerTurn)
        {
            DrawScoreButton.IsVisible = true;
            StayScoreButton.IsVisible = true;
            while (players[count].status == PlayerStatus.stay || players[count].status == PlayerStatus.bust)
            {
                count = (count + 1) % players.Count;
                if (!CheckActivePlayers())
                {
                    Player winner = StayScore();
                    AnnounceWinner(winner);
                    DrawScoreButton.IsVisible = false;
                    StayScoreButton.IsVisible = false;
                    NextButton.IsVisible = true;
                    break;
                }
            }
            DrawCard.Text = "Do you want to draw a card " + players[count].Name + "?";
            currentPlayer = 0;
            while (currentPlayer < players.Count)
            {

                PlayersNames1.Text = "Player 1 Name: " + players[currentPlayer].Name + "      Game Score: " + players[currentPlayer].score;
                currentPlayer++;
                while (currentPlayer < players.Count)
                {
                    PlayersNames2.Text = "Player 2 Name: " + players[currentPlayer].Name + "      Game Score: " + players[currentPlayer].score;
                    currentPlayer++;
                    while (currentPlayer < players.Count)
                    {
                        PlayersNames3.Text = "Player 3 Name: " + players[currentPlayer].Name + "      Game Score: " + players[currentPlayer].score;
                        currentPlayer++;
                        while (currentPlayer < players.Count)
                        {
                            PlayersNames4.Text = "Player 4 Name: " + players[currentPlayer].Name + "      Game Score: " + players[currentPlayer].score;
                            currentPlayer++;
                            while (currentPlayer < players.Count)
                            {
                                PlayersNames5.Text = "Player 5 Name: " + players[currentPlayer].Name + "      Game Score: " + players[currentPlayer].score;
                                currentPlayer++;
                                while (currentPlayer < players.Count)
                                {
                                    PlayersNames6.Text = "Player 6 Name: " + players[currentPlayer].Name + "      Game Score: " + players[currentPlayer].score;
                                    currentPlayer++;
                                    while (currentPlayer < players.Count)
                                    {
                                        PlayersNames7.Text = "Player 7 Name: " + players[currentPlayer].Name + "           Game Score: " + players[currentPlayer].score;
                                        currentPlayer++;
                                        while (currentPlayer < players.Count)
                                        {
                                            PlayersNames8.Text = "Player 8 Name: " + players[currentPlayer].Name + "           Game Score: " + players[currentPlayer].score;
                                            currentPlayer++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            int winningIndex = Reached21();
            if (winningIndex != -1)
            {
                AnnounceWinner(players[winningIndex]);
                DrawScoreButton.IsVisible = false;
                StayScoreButton.IsVisible = false;
                NextButton.IsVisible = true;
            }
            else
            {
                currentPlayer++;
                if (currentPlayer > players.Count - 1)
                {
                    currentPlayer = 0; // back to the first player...
                }
                nextTask = RaceTo21.Task.PlayerTurn;
            }
        }
        else if (nextTask == RaceTo21.Task.GameOver)
        {
            DoFinalScoring();
        }
        else if (nextTask == RaceTo21.Task.Final)
        {
            NewRoundButton.IsVisible = true;
            FinalTask();

        }
    }

    public int Reached21()
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].score == 21)
            {
                return i;
            }
        }
        return -1;
    }

    public void AnnounceWinner(Player player)
    {
        if (player != null)
        {
            player.status = PlayerStatus.win;
            DrawScoreButton.IsVisible = false;
            StayScoreButton.IsVisible = !false;
            Status.IsVisible = true;
            Status.Text = player.Name + " wins this round!";
        }
    }

    public int ScoreHand(Player player)
    {
        int score = 0;
        foreach (Card card in player.cards)
        {
            string faceValue = card.ID[0].ToString(); // made string faceValue to show the cardName string that the player has
            switch (faceValue)
            {
                case "K":
                case "Q":
                case "J":
                case "1": // to make sure that the 10 card will score +10
                    score = score + 10;
                    break;
                case "A":
                    score = score + 1;
                    break;
                default:
                    score = score + int.Parse(faceValue);
                    break;
            }
        }
        return score;
    }

    private void StartButton_Clicked(object sender, EventArgs e)
    {
        DoNextTask();
    }

    private void NumberPlayersButton_Clicked(object sender, EventArgs e)
    {
        response = NumberOfPlayers.Text;
        if (int.TryParse(response, out numOfPlayers) == false || numOfPlayers < 2 || numOfPlayers > 8) // if they type less than 2 or greater than 8, it will not run 
        {
            InvalidNumbersText.IsVisible = true; // it will say that it is invalid if what the user inputs is NOT the numbers 2-8
        }
        else
        {
            finishedTask = true;
            nextTask = RaceTo21.Task.GetNames;
            DoNextTask();
        }
    }

    private void NameButton_Clicked(object sender, EventArgs e)
    {
        string PlayerName = NamesOfPlayers.Text;
        if (PlayerName == null)
        {
            InvalidName.IsVisible = true;
        }
        else if (PlayerName.Length < 1)
        {
            InvalidName.IsVisible = true;
        }
        else
        {
            players.Add(new Player(PlayerName));
            InvalidName.IsVisible = false;
            NamesOfPlayers.Text = "";
            count++;
            if (numOfPlayers == count)
            {
                finishedTask = true;
                count = 0;
                nextTask = RaceTo21.Task.AgreedScore;
                DoNextTask();
            }
        }
    }
    private void ScoreButton_Clicked(object sender, EventArgs e)
    {
        string response = TotalWinningScore.Text; // their input
        if (int.TryParse(response, out numResponse) == false || numResponse < 50 || numResponse > 500) // if they type less than 2 or greater than 8, it will not run 
        {
            InvalidScore.IsVisible = true; // it will say that it is invalid if what the user inputs is NOT the numbers 2-8
        }
        else
        {
            InvalidScore.IsVisible = false;
            intro++;
            fullNumber += numResponse;
            TotalWinningScore.Text = "";
            count++;
            if (players.Count == count)
            {
                count = 0;
                intro = 0;
                winningScore = fullNumber / players.Count;
                finishedTask = true;
                nextTask = RaceTo21.Task.Intro;
                DoNextTask();
            }
            DoNextTask();
        }
    }

    private void NonScoreButton_Clicked(object sender, EventArgs e)
    {
        nextTask = RaceTo21.Task.FirstTurn;
        DoNextTask();
        //FirstCard();
    }

    private void DrawScoreButton_Clicked(object sender, EventArgs e)
    {

        Card card = deck.DealTopCard();
        players[count].cards.Add(card);
        players[count].score = ScoreHand(players[count]);
        CorrectCards();
        if (players[count].score > 21)
        {
            players[count].status = PlayerStatus.bust;
        }
        int counter = 0; // counting how many players bust
        Player notBusted = null; // assigned to null just in case this situation doesn't run and causes an error
        for (int i = 0; i < numOfPlayers; i++) // created a loop to check how many players bust
        {
            if (players[i].status == PlayerStatus.bust) // if the player busts...
            {
                counter++; // ...counter will go up
            }
            else
            {
                notBusted = players[i]; // whoever doesn't bust will be saved
            }
        }
        if (numOfPlayers - 1 == counter) // if the numberOfPlayers-1 busted (meaning 1 did not bust)
        {
            DrawScoreButton.IsVisible = false;
            StayScoreButton.IsVisible = false;
            Player winner = notBusted; // whoever didn't bust will win
            AnnounceWinner(winner); // announce winner
            winner.status = PlayerStatus.win; // wins the game                        
            previousWinner = winner; // keeps the winner in this variable
            NextButton.IsVisible = true;
            nextTask = RaceTo21.Task.GameOver;
        }
        nextTask = RaceTo21.Task.PlayerTurn;
        count = (count + 1) % players.Count;
        DoNextTask();
    }

    private void StayScoreButton_Clicked(object sender, EventArgs e)
    {
        players[count].status = PlayerStatus.stay;
        nextTask = RaceTo21.Task.PlayerTurn;
        count = (count + 1) % players.Count;
        DoNextTask();
    }
    private void NextButton_Clicked(object sender, EventArgs e)
    {
        DrawScoreButton.IsVisible = false;
        StayScoreButton.IsVisible = false;
        NextButton.IsVisible = false;
        nextTask = RaceTo21.Task.GameOver;
        DoNextTask();
    }

    public bool CheckActivePlayers()
    {
        foreach (var player in players)
        {
            if (player.status == PlayerStatus.active)
            {
                return true; // at least one player is still going!
            }
        }
        return false; // everyone has stayed or busted, or someone won!
    }


    public void showFinalTotalScores(List<Player> players) // I made this method to show the total final scores of every player after each round
    {
        for (int i = 0; i < players.Count; i++)
        {
            FinalScores[i].IsVisible = true;
            FinalScores[i].Text = players[i].Name + "'s total score is " + players[i].TotalScore;
        }
        FinalButton.IsVisible = true;
    }

    public Player StayScore()
    {
        FinalTotalScore.IsVisible = true;
        int highScore = 0;
        foreach (var player in players)
        {
            if (player.status == PlayerStatus.stay) // still could win...
            {
                if (player.Score > highScore)
                {
                    highScore = player.Score;
                }
            }
        }
        Player winner = players.Find(player => player.Score == highScore);
        return winner;
    }
    public Player DoFinalScoring()
    {
        FinalTotalScore.IsVisible = true;
        int highScore = 0;
        foreach (var player in players)
        {
            if (player.status == PlayerStatus.bust)
            {
                player.TotalScore -= (player.Score - 21);
            }
            if (player.status == PlayerStatus.win)
            {
                player.TotalScore += player.Score;
            }
        }
        if (highScore > 0) // someone scored, anyway!
        {
            // find the FIRST player in list who meets win condition
            Player winner = players.Find(player => player.Score == highScore);
            winner.TotalScore += winner.Score; // the winner's + winner's with the highest score when they stay total score will be updated with their score
            showFinalTotalScores(players); // shows all the player's final scores
            return winner; // returns the winner
        }
        showFinalTotalScores(players);
        return null; // everyone must have busted because nobody won!
    }

    public void FinalTask() // created FinalTask() LEVEL2 on the homework pdf where: At end of round, each player is asked if they want to keep playing. If a player says no, they are removed from the player list. If only 1 player remains, that player is the winner(equivalent to everyone else “folding” in a card game)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].TotalScore >= winningScore) // if a player has a total score that is greater than or equal to the winning score, they win the whole game
            {
                NewRoundButton.IsVisible = false;
                FullWinningLabel.IsVisible = true;
                FullWinningLabel.Text = players[i].Name + " won the whole game! They are the TRUEEEEE WINNERRRR!!! YIPPEEEEEEE!!!!!! :DDDD";
                winTheGame(); // goes to the method winTheGame
                return; // gets out of this method
            }

        }

    }
    public void winTheGame() // restarts the whole game after there is a true winner
    {
        currentPlayer = 0;
        winningScore = 0; // resets the winningScore
        players = new List<Player>(); // resets the list of players
        deck = new Deck(); // new deck
        deck.Shuffle(); // shuffles deck
        NewgameButton.IsVisible = true;
    }

    public void Restart() // made method Restart to refresh the player and deck when a new game starts
    {
        foreach (Player player in players)
        {
            player.Restart(); // calls the Restart method made in the Player class to reset the player score, status, and cards
        }
        deck = new Deck(); // new deck
        deck.Shuffle(); // shuffles deck
        Status.Text = "";
        count = 0;
        for(int i = 1; i <= players.Count; i++)
        {
            var delete = this.FindByName($"Player{i}") as HorizontalStackLayout;
            Microsoft.Maui.Controls.Label label = (Label)delete[0];
            delete.Clear();
            delete.Add(label);
        }
    }

    private void FinalButton_Clicked(object sender, EventArgs e)
    {
        DrawCard.IsVisible = false;
        Status.IsVisible = false;
        NextButton.IsVisible = false;
        FinalTotalScore.IsVisible = false;
        FinalTotalScore2.IsVisible = false;
        FinalTotalScore3.IsVisible = false;
        FinalTotalScore4.IsVisible = false;
        FinalTotalScore5.IsVisible = false;
        FinalTotalScore6.IsVisible = false;
        FinalTotalScore6.IsVisible = false;
        FinalTotalScore7.IsVisible = false;
        FinalTotalScore8.IsVisible = false;
        FinalButton.IsVisible = false;
        nextTask = RaceTo21.Task.Final;
        DoNextTask();
    }

    private void NewRoundButton_Clicked(object sender, EventArgs e)
    {
        Restart();
        NewRoundButton.IsVisible = false;
        nextTask = RaceTo21.Task.FirstTurn;
        DoNextTask();
    }

    private void NewgameButton_Clicked(object sender, EventArgs e)
    {
        nextTask = RaceTo21.Task.GetNumberOfPlayers;
        DoNextTask();
    }

    public void CorrectCards()
    {
        var addToMe = this.FindByName($"Player{count + 1}") as HorizontalStackLayout;
        Card card = players[count].cards[players[count].cards.Count - 1];
        switch (card.ID)
        {
            case "AC":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_clubs_A.png" });
                break;
            case "2C":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_clubs_02.png" });
                break;
            case "3C":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_clubs_03.png" });
                break;
            case "4C":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_clubs_04.png" });
                break;
            case "5C":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_clubs_05.png" });
                break;
            case "6C":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_clubs_06.png" });
                break;
            case "7C":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_clubs_07.png" });
                break;
            case "8C":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_clubs_08.png" });
                break;
            case "9C":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_clubs_09.png" });
                break;
            case "10C":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_clubs_10.png" });
                break;
            case "KC":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_clubs_K.png" });
                break;
            case "QC":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_clubs_Q.png" });
                break;
            case "JC":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_clubs_J.png" });
                break;
            case "AD":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_diamonds_A.png" });
                break;
            case "2D":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_diamonds_02.png" });
                break;
            case "3D":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_diamonds_03.png" });
                break;
            case "4D":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_diamonds_04.png" });
                break;
            case "5D":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_diamonds_05.png" });
                break;
            case "6D":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_diamonds_06.png" });
                break;
            case "7D":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_diamonds_07.png" });
                break;
            case "8D":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_diamonds_08.png" });
                break;
            case "9D":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_diamonds_09.png" });
                break;
            case "10D":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_diamonds_10.png" });
                break;
            case "KD":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_diamonds_K.png" });
                break;
            case "QD":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_diamonds_Q.png" });
                break;
            case "JD":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_diamonds_J.png" });
                break;
            case "AS":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_spades_A.png" });
                break;
            case "2S":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_spades_02.png" });
                break;
            case "3S":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_spades_03.png" });
                break;
            case "4S":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_spades_04.png" });
                break;
            case "5S":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_spades_05.png" });
                break;
            case "6S":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_spades_06.png" });
                break;
            case "7S":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_spades_07.png" });
                break;
            case "8S":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_spades_08.png" });
                break;
            case "9S":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_spades_09.png" });
                break;
            case "10S":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_spades_10.png" });
                break;
            case "KS":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_spades_K.png" });
                break;
            case "QS":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_spades_Q.png" });
                break;
            case "JS":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_spades_J.png" });
                break;
            case "AH":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_hearts_A.png" });
                break;
            case "2H":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_hearts_02.png" });
                break;
            case "3H":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_hearts_03.png" });
                break;
            case "4H":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_hearts_04.png" });
                break;
            case "5H":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_hearts_05.png" });
                break;
            case "6H":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_hearts_06.png" });
                break;
            case "7H":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_hearts_07.png" });
                break;
            case "8H":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_hearts_08.png" });
                break;
            case "9H":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_hearts_09.png" });
                break;
            case "10H":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_hearts_10.png" });
                break;
            case "KH":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_hearts_K.png" });
                break;
            case "QH":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_hearts_Q.png" });
                break;
            case "JH":
                addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_hearts_J.png" });
                break;
        }
    }
    public void FirstCard()
    {
        for (int i = 1; i <= players.Count; i++)
        {
            var addToMe = this.FindByName($"Player{i}") as HorizontalStackLayout;
            Card card = players[i-1].cards[0];
            switch (card.ID)
            {
                case "AC":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_clubs_A.png" });
                    break;
                case "2C":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_clubs_02.png" });
                    break;
                case "3C":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_clubs_03.png" });
                    break;
                case "4C":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_clubs_04.png" });
                    break;
                case "5C":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_clubs_05.png" });
                    break;
                case "6C":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_clubs_06.png" });
                    break;
                case "7C":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_clubs_07.png" });
                    break;
                case "8C":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_clubs_08.png" });
                    break;
                case "9C":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_clubs_09.png" });
                    break;
                case "10C":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_clubs_10.png" });
                    break;
                case "KC":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_clubs_K.png" });
                    break;
                case "QC":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_clubs_Q.png" });
                    break;
                case "JC":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_clubs_J.png" });
                    break;
                case "AD":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_diamonds_A.png" });
                    break;
                case "2D":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_diamonds_02.png" });
                    break;
                case "3D":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_diamonds_03.png" });
                    break;
                case "4D":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_diamonds_04.png" });
                    break;
                case "5D":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_diamonds_05.png" });
                    break;
                case "6D":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_diamonds_06.png" });
                    break;
                case "7D":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_diamonds_07.png" });
                    break;
                case "8D":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_diamonds_08.png" });
                    break;
                case "9D":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_diamonds_09.png" });
                    break;
                case "10D":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_diamonds_10.png" });
                    break;
                case "KD":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_diamonds_K.png" });
                    break;
                case "QD":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_diamonds_Q.png" });
                    break;
                case "JD":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_diamonds_J.png" });
                    break;
                case "AS":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_spades_A.png" });
                    break;
                case "2S":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_spades_02.png" });
                    break;
                case "3S":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_spades_03.png" });
                    break;
                case "4S":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_spades_04.png" });
                    break;
                case "5S":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_spades_05.png" });
                    break;
                case "6S":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_spades_06.png" });
                    break;
                case "7S":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_spades_07.png" });
                    break;
                case "8S":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_spades_08.png" });
                    break;
                case "9S":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_spades_09.png" });
                    break;
                case "10S":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_spades_10.png" });
                    break;
                case "KS":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_spades_K.png" });
                    break;
                case "QS":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_spades_Q.png" });
                    break;
                case "JS":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_spades_J.png" });
                    break;
                case "AH":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_hearts_A.png" });
                    break;
                case "2H":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_hearts_02.png" });
                    break;
                case "3H":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_hearts_03.png" });
                    break;
                case "4H":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_hearts_04.png" });
                    break;
                case "5H":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_hearts_05.png" });
                    break;
                case "6H":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_hearts_06.png" });
                    break;
                case "7H":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_hearts_07.png" });
                    break;
                case "8H":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_hearts_08.png" });
                    break;
                case "9H":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_hearts_09.png" });
                    break;
                case "10H":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_hearts_10.png" });
                    break;
                case "KH":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_hearts_K.png" });
                    break;
                case "QH":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_hearts_Q.png" });
                    break;
                case "JH":
                    addToMe.Add(new Microsoft.Maui.Controls.Image { Source = "card_hearts_J.png" });
                    break;
            }
        }
    }
}


