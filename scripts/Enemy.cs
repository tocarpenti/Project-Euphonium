using Godot;
using System;

public partial class Enemy : RigidBody2D
{
	private AnimatedSprite2D animator;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		animator= GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		Random random = new Random();
		string[] enemy_types = animator.SpriteFrames.GetAnimationNames();
		animator.Play(enemy_types[random.Next(0, enemy_types.Length)]);
	}

	private void OnVisibleOnScreenNotifier2dScreenExited() {
		QueueFree();
	}
}
