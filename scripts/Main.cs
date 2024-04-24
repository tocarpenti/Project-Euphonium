using Godot;
using System;
using System.Runtime.InteropServices;

public partial class Main : Node
{
	[Export]
	public PackedScene enemyScene;

	private int score;
	private Player player;
	private Marker2D startPosition;
    private Timer enemyTimer;
    private Timer scoreTimer;
    private Timer startTimer;
    private HUD hud;



    // Called when the node enters the scene tree for the first time.



    public override void _Ready()
	{
		player = GetNode<Player>("Player");
		startPosition = GetNode<Marker2D>("StartPosition");
		enemyTimer= GetNode<Timer>("EnemyTimer");
		scoreTimer= GetNode<Timer>("ScoreTimer");
		startTimer= GetNode<Timer>("StartTimer");
		hud= GetNode<HUD>("HUD");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void GameOver() {
		scoreTimer.Stop();
		enemyTimer.Stop();
		hud.ShowGameOver();
	}

	private void NewGame() {
		score = 0;
		player.Start(startPosition.Position);
		startTimer.Start();
		hud.UpdateScore(score);
		hud.ShowMessage("GET READY !");
	}

	private void OnStartTimerTimeout() {
		enemyTimer.Start();
		scoreTimer.Start();
	}

	private void OnScoreTimerTimeout() {
		score ++;
		hud.UpdateScore(score);

	}

	private void OnEnemyTimerTimeout() {
		Enemy mob = enemyScene.Instantiate<Enemy>();
		PathFollow2D mobSpawnLocation = GetNode<PathFollow2D>("EnemyPath/EnemySpawnLocation");

		mobSpawnLocation.ProgressRatio = GD.Randf();
		float direction = (float)(mobSpawnLocation.Rotation - Math.PI / 2);
		Vector2 velocity = new Vector2((float)GD.RandRange(150.0, 250.0), 0f);

		mob.Position = mobSpawnLocation.Position;
		mob.LinearVelocity = velocity.Rotated(direction);

		AddChild(mob);
	}
}
