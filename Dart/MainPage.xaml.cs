using Dart.Model;

namespace Dart;

public partial class MainPage : ContentPage
{ 

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
		for (int i = 0; i > selectedPlayersCount; i++)
		{
			playersList.Add(new Player(string.Format("Player {0} name", i)));
		}

		var gameModel = new GameModel(playersList, selectedGameVariant, finishWithDouble);
		await Navigation.PushAsync(new GamePage());
		
	}
}

