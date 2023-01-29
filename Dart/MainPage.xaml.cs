using Dart.Model;

namespace Dart;

public partial class MainPage : ContentPage
{
	private readonly Thickness MarginPrimary = new Thickness(10, 10, 0, 0);
	private readonly Thickness MarginSecondary = new Thickness(20, 10, 0, 0);

	public MainPage()
	{
		InitializeComponent();
		List<int> countList = new List<int>() { 2, 3, 4 };
		List<string> rulesPicker = new List<string>() { "Włączona", "Wyłączona" };
		List<int> gameVariants = new List<int>() { 301, 501, 701 };
		playersCountPicker.ItemsSource = countList;
		doubleRulePicker.ItemsSource = rulesPicker;
		gameVariantPicker.ItemsSource = gameVariants;
	}

	private async void startGameBt_Clicked(object sender, EventArgs e)
	{
		var selectedGameVariant = (int)gameVariantPicker.SelectedItem;
		var selectedPlayersCount = (int)playersCountPicker.SelectedItem;
		var finishWithDouble = (string)doubleRulePicker.SelectedItem == "Włączona" ? true : false;
		List<Player> playersList = new List<Player>();
		var userInputs = grPlayers.Where(x => x.GetType() == typeof(Entry));
		foreach(var input in userInputs)
		{
			playersList.Add(new Player(((Entry)input).Text));
		}
		var gameModel = new GameModel(playersList, selectedGameVariant, finishWithDouble);
		await Navigation.PushAsync(new GamePage(gameModel));
		
	}

	private void playersCountPicker_SelectedIndexChanged(object sender, EventArgs e)
	{
		var playersCount = (int)playersCountPicker.SelectedItem;

		for (int i = 0; i < playersCount; i++)
		{
			GenerateAddUserControls(i);
		}
	}

	private void GenerateAddUserControls(int playerNumber)
	{
		grPlayers.RowDefinitions.Add(new RowDefinition());
		var userLabel = new Label();
		userLabel.Margin = MarginSecondary;
		userLabel.Text = string.Format("Imię gracza {0}", playerNumber + 1);
		grPlayers.Add(userLabel, 0, playerNumber);

		var userInput = new Entry();
		userInput.Margin = MarginSecondary;
		userInput.MinimumWidthRequest = 200;
		grPlayers.Add(userInput, 1, playerNumber);
	}
}

