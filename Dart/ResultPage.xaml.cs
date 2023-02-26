using Dart.Model;

namespace Dart;

public partial class ResultPage : ContentPage
{
    private readonly Thickness margin = new Thickness(0, 10, 0, 10);
    private readonly Player player;
    private readonly IEnumerable<Round> Rounds;

    public ResultPage(Player currentPlayer)
    {
        player = currentPlayer;
        Rounds = player.ResultList;
        InitializeComponent();
        plLabel.Text = player.Name;
        SetBindings();
        lvResults.ItemsSource = Rounds;
    }

    private void SetBindings()
    {
        lvResults.ItemTemplate = new DataTemplate(() =>
        {
            // roundLabel and bindings
            Label roundNumber = new Label();
            roundNumber.SetBinding(Label.TextProperty, "RoundNumber");
            roundNumber.HorizontalOptions = LayoutOptions.Center;
            roundNumber.Margin = margin;

            // firstScoreLabel and bindings
            Label firstScore = new Label();
            firstScore.SetBinding(Label.TextProperty, "FirstShot");
            firstScore.HorizontalOptions = LayoutOptions.Center;
            firstScore.Margin = margin;

            // secondScoreLabel and bindings
            Label secondScore = new Label();
            secondScore.SetBinding(Label.TextProperty, "SecondShot");
            secondScore.HorizontalOptions = LayoutOptions.Center;
            secondScore.Margin = margin;

            // thirdScoreLabel and bindings
            Label thirdScore = new Label();
            thirdScore.SetBinding(Label.TextProperty, "ThirdShot");
            thirdScore.HorizontalOptions = LayoutOptions.Center;
            thirdScore.Margin = margin;

            // resultLabel and bindings
            Label result = new Label();
            result.SetBinding(Label.TextProperty, "Result");
            result.HorizontalOptions = LayoutOptions.Center;
            result.Margin = margin;

            var grid = new Grid()
            {
                ColumnDefinitions = new ColumnDefinitionCollection()
                {
                    new ColumnDefinition(GridLength.Star),
                    new ColumnDefinition(GridLength.Star),
                    new ColumnDefinition(GridLength.Star),
                    new ColumnDefinition(GridLength.Star),
                    new ColumnDefinition(GridLength.Star),
                }
            };
            grid.Add(roundNumber, 0);
            grid.Add(firstScore, 1);
            grid.Add(secondScore, 2);
            grid.Add(thirdScore, 3);
            grid.Add(result, 4);

            return new ViewCell
            {
                View = grid,
            };
        });
    }
}