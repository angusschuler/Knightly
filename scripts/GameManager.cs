using Godot;
using System;

public partial class GameManager : Node
{

	int score = 0;
	Label label;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		label = GetNode<Label>("ScoreLabel");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void AddScore(int points)
	{
		score += points;
		label.Text = "You collected " + score + " coins!";
	}
}
