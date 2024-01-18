using Godot;
using System;
using static Godot.GD;

public partial class player : CharacterBody2D
{
	private float speed = 300f;
	private Vector2 viewportSize;
	public override void _Ready()
	{
		viewportSize = GetViewportRect().Size;
	}

	public override void _Process(double delta)
	{
	}

	public override void _PhysicsProcess(double delta)
	{
		float direction = Input.GetAxis("move_left", "move_right");
		var newVelocity = Velocity;
		var newGlobalPosition = GlobalPosition;
		if (direction != 0)
		{
			newVelocity.X = direction * speed;
		}
		else
		{
			newVelocity.X = Mathf.MoveToward(newVelocity.X, 0, speed);
		}

		var teleportMargin = 20;
		if (GlobalPosition.X > viewportSize.X + teleportMargin)
		{
			newGlobalPosition.X = -teleportMargin;
			GlobalPosition = newGlobalPosition;
		}
		else if (GlobalPosition.X < -teleportMargin)
		{
			newGlobalPosition.X = viewportSize.X + teleportMargin;
			GlobalPosition = newGlobalPosition;
		}
		Velocity = newVelocity;
		MoveAndSlide();

	}
}
