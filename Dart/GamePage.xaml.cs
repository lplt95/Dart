using Dart.Model;

namespace Dart;

public partial class GamePage : ContentPage
{
	public GameModel model { get; set; }

	public GamePage(GameModel gameModel)
	{
		InitializeComponent();
		model = gameModel;
		Title = string.Format("Gra dla {0} osób", model.PlayersList.Count);
		SetInitial();
	}

	private void SetInitial()
	{
		var player = model.PlayersList[0];
		lbPlayer1.Text = player.Name;
		int playerScore = 0;
		foreach(var result in player.ResultList)
		{
			playerScore = +result.GetResult();
		}
		lbPlayer1_Score.Text = (model.GameVariant - playerScore).ToString();
	}
}