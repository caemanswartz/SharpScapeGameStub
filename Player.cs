using Godot;
using System;

public class Player : Area2D
{
	public int Speed=100;
	public Vector2 ScreenSize;
	private AnimatedSprite _animatedSprite;
	public override void _Ready()
	{
		ScreenSize=GetViewportRect().Size;
		_animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
		_animatedSprite.Animation="idle";
		_animatedSprite.Play();
	}
	public override void _Process(float delta)
	{
		var velocity = Vector2.Zero; // The player's movement vector.
		if(Input.IsActionPressed("move_right"))
			velocity.x+=1;
		if(Input.IsActionPressed("move_left"))
			velocity.x-=1;
		if(Input.IsActionPressed("move_down"))
			velocity.y+=1;
		if(Input.IsActionPressed("move_up"))
			velocity.y-=1;
		if(velocity.Length()>0)
		{
			velocity=velocity.Normalized() * Speed;
		}
		if(velocity.x!=0||velocity.y!=0)
		{
			_animatedSprite.Animation = "walk";
			_animatedSprite.FlipV = false;
			_animatedSprite.FlipH = velocity.x < 0;
		}
		else _animatedSprite.Animation = "idle";
		Position+=velocity*delta;
		Position=new Vector2(
			x:Mathf.Clamp(Position.x,0,ScreenSize.x),
			y:Mathf.Clamp(Position.y,0,ScreenSize.y));
	}
}
