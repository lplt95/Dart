using Dart.Model;

namespace Dart;

public partial class MainPage : ContentPage
{
	private readonly Thickness MarginPrimary = new Thickness(10, 10, 0, 0);
	private readonly Thickness MarginSecondary = new Thickness(20, 10, 0, 0);
	private readonly bool isDebug = false;

	public MainPage()
	{
		InitializeComponent();
		Title = "Gra w Darta";
		List<int> countList = new List<int>() { 2, 3, 4 };
		List<string> rulesPicker = new List<string>() { "Włączona", "Wyłączona" };
		List<int> gameVariants = new List<int>() { 301, 501, 701 };
		playersCountPicker.ItemsSource = countList;
		playersCountPicker.SelectedItem = countList[0];
		doubleRulePicker.ItemsSource = rulesPicker;
		doubleRulePicker.SelectedItem = rulesPicker[0];
		gameVariantPicker.ItemsSource = gameVariants;
		gameVariantPicker.SelectedItem = gameVariants[1];
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
		grPlayers.Clear();
		var playersCount = (int)playersCountPicker.SelectedItem;
		List<string> names = new List<string>() { "AAAA", "BBBB", "CCCC", "DDDD" };

		for (int i = 0; i < playersCount; i++)
		{
			if (isDebug)
				GenerateAddUserControls(i, names[i]);
			else
				GenerateAddUserControls(i);
		}
	}

	private void GenerateAddUserControls(int playerNumber, string name = null)
	{
		grPlayers.RowDefinitions.Add(new RowDefinition());
		var userLabel = new Label();
		userLabel.Margin = MarginSecondary;
		userLabel.Text = string.Format("Imię gracza {0}", playerNumber + 1);
		userLabel.VerticalOptions = LayoutOptions.Center;
		grPlayers.Add(userLabel, 0, playerNumber);

		var entry = new Entry()
		{
			MinimumWidthRequest = 150,
			Margin = MarginSecondary,
			BackgroundColor = Colors.GhostWhite,
			Text = name,
			HorizontalTextAlignment = TextAlignment.Center,
			Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeSentence),
		};
		entry.Completed += EntryCompleted;
		grPlayers.Add(entry, 1, playerNumber);
	}

	private void EntryCompleted(object sender, EventArgs e)
	{
		((Entry)sender).IsEnabled = false;
		((Entry)sender).IsEnabled = true;
	}
}

