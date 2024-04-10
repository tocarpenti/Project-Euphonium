using Godot;
using System;

public partial class main : Node
{
	[Export]
	public PackedScene enemyScene;

	private int score;
	private Player player;
	private Marker2D startPosition;
    private Timer enemyTimer;
    private Timer scoreTimer;
    private Timer startimer;



    // Called when the node enters the scene tree for the first time.


    public override void _Ready()
	{
		player = GetNode<Player>("Player");
		startPosition = GetNode<Marker2D>("StartPosition");
		enemyTimer= GetNode<Timer>("EnemyTimer");
		scoreTimer= GetNode<Timer>("ScoreTimer");
		startimer= GetNode<Timer>("Startimer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void GameOver() {
		scoreTimer.Stop();
		enemyTimer.Stop();
	}

	private void NewGame() {
		score = 0;
		player.Start(startPosition.Position);
		startimer.Start();
	}

	private void OnStartTimerTimeout() {
		enemyTimer.Start();
		scoreTimer.Start();
	}

	private void OnScoreTimerTimeout() {
		score ++;
	}

	private void OnEnemyTimerTimeout() {
		
	}
}
