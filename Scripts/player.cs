using Godot;
using System;
using static Godot.GD;

public partial class player : CharacterBody2D
{
	[Export]
	private float speed = 300f;
	[Export]
	private float gravity = 15f;
	private float maxFallVelocity = 1000f;
	private Vector2 viewportSize;
	private float jumpVelocity = -800;
	private Vector2 newVelocity;
	private AnimationPlayer anim;

	public override void _Ready()
	{
		anim = GetNode<AnimationPlayer>("AnimationPlayer");
		viewportSize = GetViewportRect().Size;
		newVelocity = Velocity;
	}

	public override void _Process(double delta)
	{
		if (Velocity.Y > 0)
		{
			if (anim.CurrentAnimation != "fall")
			{
				anim.Play("fall");
			}
		}
		else if (Velocity.Y < 0)
		{
			if (anim.CurrentAnimation != "jump")
			{
				anim.Play("jump");
			}
		}
	}

	public void Jump()
	{
		newVelocity.Y = jumpVelocity;
	}

	public override void _PhysicsProcess(double delta)
	{
		var newGlobalPosition = GlobalPosition;
		// Applying gravity to player
		newVelocity.Y += gravity;
		if (newVelocity.Y > maxFallVelocity)
		{
			newVelocity.Y = maxFallVelocity;
		}
		// Move based off of input directions
		float direction = Input.GetAxis("move_left", "move_right");
		if (direction != 0)
		{
			newVelocity.X = direction * speed;
		}
		else
		{
			newVelocity.X = Mathf.MoveToward(newVelocity.X, 0, speed);
		}
		// Teleports player to other side if outside viewport boundaries
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
