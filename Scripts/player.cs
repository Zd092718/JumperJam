using Godot;
using System;
using static Godot.GD;

public partial class player : CharacterBody2D
{
	private float speed = 300f;
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}

	public override void _PhysicsProcess(double delta)
	{
		float direction = Input.GetAxis("move_left", "move_right");
		var newVelocity = Velocity;
		if (direction != 0)
		{
			newVelocity.X = direction * speed;
		}
		else
		{
			newVelocity.X = Mathf.MoveToward(newVelocity.X, 0, speed);
		}

		Velocity = newVelocity;
		MoveAndSlide();
	}
}
