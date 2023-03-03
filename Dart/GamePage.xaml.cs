using Dart.Model;
using Microsoft.Maui.Graphics;
using System.Collections.ObjectModel;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Dart;

public partial class GamePage : ContentPage
{
	public GameModel model { get; set; }
	ObservableCollection<Player> playersList;
    Player currentPlayer;
	int playerScore;
	int currentPlayerScore;
	int firstScore;
	int secondScore;
	int thirdScore;
	int loopCount = 0;
	string scoreText = "Do końca pozostało: {0}";

	public GamePage(GameModel gameModel)
	{
		InitializeComponent();
		model = gameModel;
        Title = string.Format("Gra dla {0} osób", model.PlayersList.Count);
		List<int> multi = new List<int>() { 1, 2, 3 };
		playersList = new ObservableCollection<Player>();
		foreach(var player in model.PlayersList)
		{
			playersList.Add(player);
		}
		SetPicker(multi);
		SetStartingValues();
	}

	private async Task SetInitial()
	{
        pick1Multi.SelectedIndex = 0;
        pick2Multi.SelectedIndex = 0;
        pick3Multi.SelectedIndex = 0;
        thr1Score.Text = String.Empty;
        thr2Score.Text = String.Empty;
        thr3Score.Text = String.Empty;
        firstScore = 0;
        secondScore = 0;
        thirdScore = 0;
        throw1Empty.IsChecked = false;
		throw2Empty.IsChecked = false;
		throw3Empty.IsChecked = false;
		SetStartingValues();
		await DisplayAlert(string.Format("Rzuca {0}", currentPlayer.Name), string.Format("Masz {0} do końca", playerScore), "OK");
		UpdateScore();
	}

    #region FirstScoreControl

    private void HandleEmptyForFirst(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
			pick1Multi.IsEnabled = false;
			thr1Score.IsEnabled = false;
			thr1Score.BackgroundColor = Colors.White;
            firstScore = 0;
            UpdateScore();
        }
        else
        {
			pick1Multi.IsEnabled = true;
			thr1Score.IsEnabled = true;
			thr1Score.BackgroundColor = Colors.GhostWhite;
            CalculateScoreForFirst(sender, new FocusEventArgs(thr1Score, false));
        }
    }

    private async void CalculateScoreForFirst(object sender, EventArgs e)
    {
        var entry = thr1Score;
        if (string.IsNullOrEmpty(entry.Text))
        {
            return;
        }
        await CalculateScore(entry.Text, 1);
		thr1Score.IsEnabled = false;
		thr1Score.IsEnabled = true;
    }

    #endregion

    #region SecondScoreControl

    private void HandleEmptyForSecond(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
			pick2Multi.IsEnabled = false;
			thr2Score.IsEnabled = false;
			thr2Score.BackgroundColor = Colors.White;
            secondScore = 0;
            UpdateScore();
        }
        else
        {
			pick2Multi.IsEnabled = true;
			thr2Score.IsEnabled = true;
            thr2Score.BackgroundColor = Colors.GhostWhite;
            CalculateScoreForSecond(sender, new FocusEventArgs(thr2Score, false));
        }
    }

    private async void CalculateScoreForSecond(object sender, EventArgs e)
    {
        var entry = thr2Score;
        if (string.IsNullOrEmpty(entry.Text))
		{
			return;
		}
		await CalculateScore(entry.Text, 2);
		thr2Score.IsEnabled = false;
		thr2Score.IsEnabled = true;
    }

    #endregion

    #region ThirdScoreControl

    private void HandleEmptyForThird(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
			pick3Multi.IsEnabled = false;
			thr3Score.IsEnabled = false;
			thr3Score.BackgroundColor = Colors.White;
            thirdScore = 0;
            UpdateScore();
        }
        else
        {
			pick3Multi.IsEnabled = true;
			thr3Score.IsEnabled = true;
            thr3Score.BackgroundColor = Colors.GhostWhite;
            CalculateScoreForThird(sender, new FocusEventArgs(thr3Score, false));
        }
    }

    private async void CalculateScoreForThird(object sender, EventArgs e)
    {
        var entry = thr3Score;
        if (string.IsNullOrEmpty(entry.Text))
		{
			return;
		}
		await CalculateScore(entry.Text, 3);
		thr3Score.IsEnabled = false;
		thr3Score.IsEnabled = true;
    }

	#endregion

	private async Task CalculateScore(string entryText, int scoreToCalc)
	{
		int value, multiplier;
        if (!Int32.TryParse(entryText, out value))
        {
            await DisplayAlert("Dev coś zepsuł", "Developer jest kretynem i coś zepsuł. Zgłoś mu to.", "Eh, ok...");
			ClearField(scoreToCalc);
			return;
        }
		if (value < 0)
		{
			await DisplayAlert("Mniej niż zero?", "Serio? Ujemne punkty? Tak się nie gra.", "Ok");
            ClearField(scoreToCalc);
            return;
		}
		if (value > 20)
		{
			if(value != 25 || value != 50)
			{
				await DisplayAlert("Ej, tak nie gramy!", "Wprowadziłeś wartość większą niż 20, ale nie trafiłeś bulla. Chyba coś Ci się pomyliło.", "Aha.");
                ClearField(scoreToCalc);
                return;
			}
		}
		switch(scoreToCalc)
		{
			case 1:
				{
					multiplier = (int)pick1Multi.SelectedItem;
					break;
				}
			case 2:
				{
					multiplier = (int)pick2Multi.SelectedItem;
					break;
				}
			case 3:
				{
					multiplier = (int)pick3Multi.SelectedItem;
					break;
				}
			default:
				{
					await DisplayDevMessage();
					return;
				}
		}
		int score = multiplier * value;
		if (currentPlayerScore - score == 0)
		{
			if (model.FinalWithDouble && multiplier != 2)
			{
                await DisplayAlert("Błędny rzut!", "Ostatni rzut musi trafić w podwójne pole!", "Ok");
                EndPlayerRound(false);
            }
			else
			{
				EndGame();
			}
		}
		else if (currentPlayerScore - score < 0)
		{
            await DisplayAlert("Błędny rzut!", "Nie możesz zejść poniżej zera...", "No ok...");
            EndPlayerRound(false);
        }
		else
		{
			switch(scoreToCalc)
			{
				case 1:
					{
						firstScore = score;
						UpdateScore();
						break;
					}
				case 2:
					{
						secondScore = score;
						UpdateScore();
						break;
					}
				case 3:
					{
						thirdScore = score;
						UpdateScore();
						break;
					}
				default:
					{
						await DisplayDevMessage();
						break;
					}
			}
		}
    }

	private void FilterNonnumericValues(object sender, TextChangedEventArgs e)
	{
		if (e.NewTextValue.Any(x => !char.IsDigit(x)))
		{
			((Entry)sender).Text = e.OldTextValue;
		}
	}

	private async void SaveResultAndFinishRound(object sender, EventArgs e)
	{
		thr1Score.Unfocus();
		thr2Score.Unfocus();
		thr3Score.Unfocus();
		await EndPlayerRound(true);
	}

	private void ShowPlayerResults(object sender, EventArgs e)
	{
		Navigation.PushModalAsync(new ResultPage(currentPlayer));
	}

	private async Task EndPlayerRound(bool saveScore)
	{
        loopCount++;
        if (loopCount == model.PlayersList.Count)
            loopCount = 0;
		int roundNumber = currentPlayer.ResultList.Count > 0 ? currentPlayer.ResultList.Max(x => x.RoundNumber) : 0;
        if (saveScore)
		{
			var round = new Round(++roundNumber, firstScore, secondScore, thirdScore);
			currentPlayer.ResultList.Add(round);
			playersList.First(x => x.Name == currentPlayer.Name).GetCurrentScore();
            await SetInitial();
		}
		else
		{
			currentPlayer.ResultList.Add(new Round(++roundNumber, 0, 0, 0));
			await SetInitial();
		}
	}

	private async void EndGame()
	{
		string winner = string.Format("Zwycięzca: {0}", currentPlayer.Name);
		var result = await DisplayAlert("Jedziemy od nowa?", winner + "\nChcesz zacząć grę od początku?", "Tak", "Nie");
		if (result)
		{
			var sameRules = await DisplayAlert("Zasady", "Te same zasady i gracze?", "Tak", "Nie");
			if (sameRules)
			{
				foreach(var player in model.PlayersList)
				{
					player.ResultList.Clear();
				}
				loopCount = 0;
				await SetInitial();
			}
			else
			{
				await Navigation.PushAsync(new MainPage());
			}
		}
		else
		{
			//TODO: nie wiem? xD
		}
	}

	private void SetPicker(List<int> items)
	{
		pick1Multi.ItemsSource = items;
		pick1Multi.SelectedItem = items[0];
		pick2Multi.ItemsSource = items;
		pick2Multi.SelectedItem = items[0];
        pick3Multi.ItemsSource = items;
		pick3Multi.SelectedItem = items[0];
    }

	private void UpdateScore()
	{
		currentPlayerScore = playerScore - firstScore - secondScore - thirdScore;
		curScoreLab.Text = string.Format(scoreText, currentPlayerScore);
    }

	private void ClearField(int fieldToClear)
	{
		switch(fieldToClear)
		{
			case 1:
				{
					thr1Score.Text = string.Empty;
					break;
				}
			case 2:
				{
					thr2Score.Text = string.Empty;
					break;
				}
			case 3:
				{
					thr3Score.Text = string.Empty;
					break;
				}
		}
	}

	private void SetStartingValues()
	{
        currentPlayer = model.PlayersList[loopCount];
        currPlLabel.Text = string.Format("Aktualnie rzuca: {0}", currentPlayer.Name);
        playerScore = model.GameVariant - currentPlayer.GetCurrentScore();
        currentPlayerScore = playerScore;
		SetResultsList();
        UpdateScore();
    }

	private async Task DisplayDevMessage(string alternativeText = null)
	{
		string messageText = string.IsNullOrWhiteSpace(alternativeText) ? "Developer jest kretynem i coś zepsuł. Zgłoś mu to." : alternativeText;
        await DisplayAlert("Dev coś zepsuł", messageText, "Eh, ok...");
    }

	private void SetResultsList()
	{
		grResultsList.Clear();
		var margin = new Thickness(0, 0, 0, 10);
		int rowCount = 0;
		foreach(var player in model.PlayersList)
		{
			grResultsList.AddRowDefinition(new RowDefinition(30));
			var playerLabel = new Label()
			{
				Margin = margin,
				Text = player.Name,
			};
			grResultsList.Add(playerLabel, 0, rowCount);
			var scoreLabel = new Label()
			{
				Margin = margin,
				Text = player.CurrentScore.ToString()
			};
			grResultsList.Add(scoreLabel, 1, rowCount);
			rowCount++;
		}
	}
}