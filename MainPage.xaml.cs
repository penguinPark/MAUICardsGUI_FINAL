using Microsoft.UI.Xaml.Controls;
using RaceTo21;
using System.Reflection;
using static System.Formats.Asn1.AsnWriter;

namespace MAUICardsGUI;
public partial class MainPage : ContentPage
{
    int numOfPlayers; // number of players in current game
    List<Player> players = new List<Player>(); // list of objects containing player data
    Deck deck = new Deck(); // deck of cards
    int currentPlayer = 0; // current player on list
    public RaceTo21.Task nextTask = RaceTo21.Task.GetNumberOfPlayers; // keeps track of game state through enum Task
    Player previousWinner; // to keep track of the player who won
    public int winningScore; // variable to represent the winning total score
    public bool finishedTask = false; // to see if the task is finished
    public string response; // player's responses
    int count; // counter for button
    int fullNumber = 0; // full winning score number
    int numResponse; // player's number response
    int intro = 0; // for the player introduction
    List<Label> FinalScores; // list of the labels for the final scores (was testing to see if this can shorten the amount of code)...

    public MainPage()
    {
        InitializeComponent();
        FinalScores = new List<Label> { FinalTotalScore, FinalTotalScore2, FinalTotalScore3, FinalTotalScore4, FinalTotalScore5, FinalTotalScore6, FinalTotalScore7, FinalTotalScore8 }; // list of all the final score labels for every player
    }

    public void DoNextTask() // this method goes through every single task throughout this game. It starts from the first button click that starts the game
    {
        if (nextTask == RaceTo21.Task.GetNumberOfPlayers) // this is to get the number of players in the game
        {
            StartButton.IsVisible = false;
            gameLabel.Text = "How many players?";
            NumberOfPlayers.IsVisible = true;
            NumberPlayersButton.IsVisible = true;
        }
        else if (nextTask == RaceTo21.Task.GetNames) // this is to get the names of the players in the game
        {
            InvalidNumbersText.IsVisible = false;
            NumberOfPlayers.IsVisible = false;
            NumberPlayersButton.IsVisible = false;
            gameLabel.Text = "What is your name?";
            NamesOfPlayers.IsVisible = true;
            NameButton.IsVisible = true;
        }
        else if (nextTask == RaceTo21.Task.AgreedScore) // this is to get everyone's preferred winning score
        {
            InvalidName.IsVisible = false;
            NamesOfPlayers.IsVisible = false;
            NameButton.IsVisible = false;
            TotalWinningScore.IsVisible = true;
            ScoreButton.IsVisible = true;
            gameLabel.Text = "What does " + players[intro].Name + " want the winning score to be? (The winning score determines who wins the full game when a player reaches " +
                "this score after multiple rounds. The score is the average of every player's preferred winning score)";

        }
        else if (nextTask == RaceTo21.Task.Intro) // introduces all the players and their initial scores (which should be 0)
        {
            deck.Shuffle(); // shuffles deck here
            InvalidScore.IsVisible = false;
            TotalWinningScore.IsVisible = false;
            ScoreButton.IsVisible = false;
            gameLabel.IsVisible = false;
            TotalScore.IsVisible = true;
            NonScoreButton.IsVisible = true;
            TotalScore.Text = "Total Winning Score: " + winningScore;
            while (currentPlayer < players.Count) // this sets up the labels for up to 8 players
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
        else if (nextTask == RaceTo21.Task.FirstTurn) //This is to give every player their first card after they start the game. This is to ensure everyone gets their first card without clicking "stay" when they have no cards
        {
          
            NonScoreButton.IsVisible = false;
            DrawScoreButton.IsVisible = true;
            DrawCard.IsVisible = true;
            StayScoreButton.IsVisible = true;
            DrawCard.Text = "Do you want to draw a card " + players[intro].Name;
            currentPlayer = 0;
            if (players[currentPlayer].cards.Count == 0) // if their card count is 0... (for up to 8 players)
            {
                while (currentPlayer < players.Count)
                {
                    Card card = deck.DealTopCard();
                    players[currentPlayer].cards.Add(card); // they'll get their first card
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
            } FirstCard(); // this method was created to allow the card image of everyone's first card to pop out in the screen :).
        }
        else if (nextTask == RaceTo21.Task.PlayerTurn) // this is when players can choose to draw or stay
        {
            DrawScoreButton.IsVisible = true;
            StayScoreButton.IsVisible = true;
            while (players[count].status == PlayerStatus.stay || players[count].status == PlayerStatus.bust) // while the player's status is bust or stay....
            {
                count = (count + 1) % players.Count; // this was made to skip the player if their status is bust or stay
                if (!CheckActivePlayers()) //if everyone busted, stayed, or someone won
                {
                    Player winner = StayScore(); // stayed winner with highest score wins
                    AnnounceWinner(winner);
                    DrawScoreButton.IsVisible = false;
                    StayScoreButton.IsVisible = false;
                    NextButton.IsVisible = true;
                    break; // if not, breaks out of loop
                }
            }
            DrawCard.Text = "Do you want to draw a card " + players[count].Name + "?"; // asks current player if they want to draw a card
            currentPlayer = 0; 
            while (currentPlayer < players.Count) // goes through all the players with their name and updated scores
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
            int winningIndex = Reached21(); // if someone readches 21 we get their index in the list
            if (winningIndex != -1)
            {
                AnnounceWinner(players[winningIndex]); // announce that player as the winner
                DrawScoreButton.IsVisible = false;
                StayScoreButton.IsVisible = false;
                NextButton.IsVisible = true;
            }
            else // if no one reached 21, it will update to the next players
            {
                currentPlayer++;
                if (currentPlayer > players.Count - 1)
                {
                    currentPlayer = 0; // back to the first player...
                }
                nextTask = RaceTo21.Task.PlayerTurn;
            }
        }
        else if (nextTask == RaceTo21.Task.GameOver) // when the game ends...
        {
            DoFinalScoring(); // does the final scoring for everyone and announces winner
        }
        else if (nextTask == RaceTo21.Task.Final) // the final task of the game when a player reaches the total winning score
        {
            NewRoundButton.IsVisible = true;
            FinalTask(); // calculates if anyone is the true winner!

        }
    }

    public int Reached21() // if the player reaches the score of 21, their index will be returned
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

    public void AnnounceWinner(Player player) // announces the winner
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

    public int ScoreHand(Player player) // scores tha hand of the player
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

    private void StartButton_Clicked(object sender, EventArgs e) // this button starts the game
    {
        DoNextTask();
    }

    private void NumberPlayersButton_Clicked(object sender, EventArgs e) // button to get number of players
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
            DoNextTask(); // goes to next task when done
        }
    }

    private void NameButton_Clicked(object sender, EventArgs e) // button to get names of players
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
            players.Add(new Player(PlayerName)); // adds the players names into the player list
            InvalidName.IsVisible = false;
            NamesOfPlayers.Text = "";
            count++; // counts amount of times the button is pressed
            if (numOfPlayers == count) // when the number of players and number of times the button was clicked is the same...
            {
                finishedTask = true;
                count = 0; // count is back to 0
                nextTask = RaceTo21.Task.AgreedScore;
                DoNextTask(); // goes to next task
            }
        }
    }
    private void ScoreButton_Clicked(object sender, EventArgs e) // button for the total winning score
    {
        string response = TotalWinningScore.Text; // their input
        if (int.TryParse(response, out numResponse) == false || numResponse < 50 || numResponse > 500) // if they type less than 2 or greater than 8, it will not run 
        {
            InvalidScore.IsVisible = true; // it will say that it is invalid if what the user inputs is NOT the numbers 2-8
        }
        else
        {
            InvalidScore.IsVisible = false;
            intro++; // when clicking the intro counter goes up 
            fullNumber += numResponse; // everyone's number response gets added to a full number
            TotalWinningScore.Text = "";
            count++; // counts amount of times the button is pressed
            if (players.Count == count) 
            {
                count = 0;
                intro = 0;
                winningScore = fullNumber / players.Count; // gets the average of the total winning score
                finishedTask = true;
                nextTask = RaceTo21.Task.Intro;
                DoNextTask(); // goes to next task
            }
            DoNextTask(); // goes back to do next task
        }
    }

    private void NonScoreButton_Clicked(object sender, EventArgs e) // button for getting first cards
    {
        nextTask = RaceTo21.Task.FirstTurn;
        DoNextTask();
    }

    private void DrawScoreButton_Clicked(object sender, EventArgs e) // button for drawing
    {

        Card card = deck.DealTopCard(); // deals top card
        players[count].cards.Add(card); // players get a card
        players[count].score = ScoreHand(players[count]); // updates score
        CorrectCards(); // shows the correct images of the cards that the players get
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
            nextTask = RaceTo21.Task.GameOver; //if there is a winner, goes to game over...
        }
        nextTask = RaceTo21.Task.PlayerTurn; // ... and goes back to player turn if not
        count = (count + 1) % players.Count; // to make sure that players are skipped correctly 
        DoNextTask();
    }

    private void StayScoreButton_Clicked(object sender, EventArgs e) // changes players status to stay
    {
        players[count].status = PlayerStatus.stay;
        nextTask = RaceTo21.Task.PlayerTurn;
        count = (count + 1) % players.Count; // makes sure that players are skipped correctly
        DoNextTask();
    }
    private void NextButton_Clicked(object sender, EventArgs e) // goes to the game over task
    {
        DrawScoreButton.IsVisible = false;
        StayScoreButton.IsVisible = false;
        NextButton.IsVisible = false;
        nextTask = RaceTo21.Task.GameOver;
        DoNextTask();
    }

    public bool CheckActivePlayers() // checks for active players
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
    public Player DoFinalScoring() // does the final scoring for every player
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

    public void FinalTask() // created FinalTask to announce who the true winner of the whole game is after multiple rounds
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
        for (int i = 1; i <= players.Count; i++) // this was made to delete all of the cards images for new card images
        {
            var delete = this.FindByName($"Player{i}") as HorizontalStackLayout; // got inspired by Jay's MAUI no data binding example!
            Microsoft.Maui.Controls.Label label = (Label)delete[0]; // stores the label in here
            delete.Clear(); // clears everything
            delete.Add(label); // adds the labels back
        }
        currentPlayer = 0;
        winningScore = 0; // resets the winningScore
        players = new List<Player>(); // resets the list of players
        deck = new Deck(); // new deck
        deck.Shuffle(); // shuffles deck
        NewgameButton.IsVisible = true;
    }

    public void Restart() // made method Restart to refresh the player and deck when a new round starts
    {
        foreach (Player player in players)
        {
            player.Restart(); // calls the Restart method made in the Player class to reset the player score, status, and cards
        }
        deck = new Deck(); // new deck
        deck.Shuffle(); // shuffles deck
        Status.Text = "";
        count = 0;
        for(int i = 1; i <= players.Count; i++) // this was made to delete all of the cards images for new card images
        {
            var delete = this.FindByName($"Player{i}") as HorizontalStackLayout; // got inspired by Jay's MAUI no data binding example!
            Microsoft.Maui.Controls.Label label = (Label)delete[0]; // stores the label in here
            delete.Clear(); // clears everything
            delete.Add(label); // adds the labels back
        }
    }

    private void FinalButton_Clicked(object sender, EventArgs e) // when this button is clicked, a lot of labels and buttons are removed
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
        nextTask = RaceTo21.Task.Final; // goes to final
        DoNextTask();
    }

    private void NewRoundButton_Clicked(object sender, EventArgs e) // this button is clicked to start a new round
    {
        Restart(); // refreshes the new round
        NewRoundButton.IsVisible = false;
        nextTask = RaceTo21.Task.FirstTurn; // goes back to everyone getting their first cards
        DoNextTask();
    }

    private void NewgameButton_Clicked(object sender, EventArgs e) // starts a new game
    {
        Restart(); // restarts everything
        TotalScore.IsVisible = false;
        PlayersNames1.IsVisible = false;
        PlayersNames2.IsVisible = false;    
        PlayersNames3.IsVisible = false;
        PlayersNames4.IsVisible = false;
        PlayersNames5.IsVisible = false;
        PlayersNames6.IsVisible = false;
        PlayersNames7.IsVisible = false;
        PlayersNames8.IsVisible = false;
        FullWinningLabel.IsVisible = false;
        NewgameButton.IsVisible = false;
        gameLabel.IsVisible = true;
        nextTask = RaceTo21.Task.GetNumberOfPlayers; // goes to number of players
        DoNextTask();
    }

    public void CorrectCards() // this method was made to show the correct card images for every card drawn
    {
        var addToMe = this.FindByName($"Player{count + 1}") as HorizontalStackLayout; // got inspired by Jay's MAUI no data binding example
        Card card = players[count].cards[players[count].cards.Count - 1]; // card object that represents the players card that they got when drawing
        switch (card.ID) // used switch to connect an image source to a card ID
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

    public void FirstCard() // this is to get the card image for everyone's first card
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


