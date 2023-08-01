using Godot;
using System;

public partial class world : Node2D
{
	// Called when the node enters the scene tree for the first time.
	private CollisionPolygon2D _collisionPolygon2D;
	private Polygon2D _polygon2D;
	public override void _Ready()
	{
		RenderingServer.SetDefaultClearColor(Colors.Black);
		_collisionPolygon2D = GetNode<CollisionPolygon2D>("StaticBody2D/CollisionPolygon2D");
		_polygon2D = GetNode<Polygon2D>("StaticBody2D/CollisionPolygon2D/Polygon2D");
		_polygon2D.Polygon = _collisionPolygon2D.Polygon;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
