using Godot;
using System;

public partial class Player : Area2D
{
	[Export]
	public int speed = 400;

	private Vector2 screen_size;

	private AnimatedSprite2D animator;

	private CollisionShape2D collidor;

	override public void _Ready () {
		screen_size = GetViewportRect().Size;
		animator= GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		collidor= GetNode<CollisionShape2D>("CollisionShape2D");
		Hide();
	}

	override public void _Process (double delta) {
		Vector2 velocity = Vector2.Zero;
		if(Input.IsActionPressed("move_right")){
			velocity.X += 1;
		}
		if(Input.IsActionPressed("move_left")){
			velocity.X -= 1;
		}
		if(Input.IsActionPressed("move_up")){
			velocity.Y -= 1;
		}
		if(Input.IsActionPressed("move_down")){
			velocity.Y += 1;
		}

		if(velocity.Length() > 0){
			velocity = velocity.Normalized() * speed;
			animator.Play();			
		}
		else {
			animator.Stop();
		}

		Position += velocity * (float) delta;
		Position = Position.Clamp(Vector2.Zero, screen_size);

		if(velocity.X != 0) {
			animator.Animation = "walk";
			animator.FlipH = velocity.X < 0;
		}
		else if (velocity.Y != 0){
			animator.Animation = "up";
		}
	}

	[Signal]
	public delegate void HitEventHandler();

	private void OnBodyEntered(Node2D body){
		Hide();
		EmitSignal(SignalName.Hit);
		collidor.SetDeferred("disabled", true);
	}


	public void Start(Vector2 pos) {
		Position = pos;
		Show();
		collidor.Disabled = false;
	}
}
