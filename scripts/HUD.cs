using Godot;
using System;
using System.Threading.Tasks;

public partial class HUD : CanvasLayer
{
    private Label scoreLabel;
    private Label messageLabel;
    private Button startButton;
    private Timer messageTimer;

    // Called when the node enters the scene tree for the first time.

    public override void _Ready()
	{
		scoreLabel= GetNode<Label>("ScoreLabel");
		messageLabel= GetNode<Label>("MessageLabel");
		startButton= GetNode<Button>("StartButton");
		messageTimer= GetNode<Timer>("MessageTimer");
	}

	public void ShowMessage(String text) {
		messageLabel.Text = text;
		messageLabel.Show();
		messageTimer.Start();
	}

	public async void ShowGameOver() {
		ShowMessage("GAME OVER");
		await ToSignal(messageTimer, Timer.SignalName.Timeout);
		
		messageLabel.Text = "DODGE THE ENEMIES !";
		messageLabel.Show();

		await ToSignal(GetTree().CreateTimer(1.0f), SceneTreeTimer.SignalName.Timeout);
		startButton.Show();
	}

	public void UpdateScore (int score){
		scoreLabel.Text = score.ToString();
	}

	[Signal]
	public delegate void StartGameEventHandler();

	
	private void OnStartButtonPressed() {
		startButton.Hide();
		EmitSignal(SignalName.StartGame);
	}

	private void OnMessageTimerTimeout() {
		messageLabel.Hide();
	}
}
