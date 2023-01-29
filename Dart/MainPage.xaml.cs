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

	private void startGameBt_Clicked(object sender, EventArgs e)
	{
		var selectedGameVariant = (int)gameVariantPicker.SelectedItem;
		var selectedPlayersCount = (int)playersCountPicker.SelectedItem;
		var 
	}
}

