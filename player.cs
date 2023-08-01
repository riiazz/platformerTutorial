using Godot;
using System;

public partial class player : CharacterBody2D
{
	public const float Speed = 100.0f;
	public const float JumpVelocity = -400.0f;
	public const float ACCELERATION = 600f;
	public const float FRICTION = 1000f;
    private AnimatedSprite2D animatedSprite2D;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    public override void _Ready()
    {
        animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }
    
	public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;

        ApplyGravity(delta, ref velocity);
        HandleJump(ref velocity);
        Vector2 inputAxis = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        HandleAcceleration(delta, ref velocity, inputAxis);
        ApplyFriction(delta, ref velocity, inputAxis);
        UpdateAnimation(inputAxis);
        Velocity = velocity;
        MoveAndSlide();
    }

    private void HandleAcceleration(double delta, ref Vector2 velocity, Vector2 inputAxis)
    {
        if (inputAxis != Vector2.Zero)
        {
            velocity.X = Mathf.MoveToward(velocity.X, inputAxis.X * Speed, ACCELERATION * (float)delta);
        }
    }

    private void ApplyFriction(double delta, ref Vector2 velocity, Vector2 inputAxis)
    {
		if(inputAxis == Vector2.Zero){
			velocity.X = Mathf.MoveToward(Velocity.X, 0, FRICTION * (float)delta);
		}
    }

    private void HandleJump(ref Vector2 velocity)
    {
        // Handle Jump.
        if (IsOnFloor())
        {
            if (Input.IsActionJustPressed("ui_accept"))
            {
                velocity.Y = JumpVelocity;
            }
        }
        else
        {
            if (Input.IsActionJustReleased("ui_accept") && velocity.Y < JumpVelocity / 2)
            {
                velocity.Y = JumpVelocity / 2;
            }
        }
    }

    private void ApplyGravity(double delta, ref Vector2 velocity)
    {
        // Add the gravity.
        if (!IsOnFloor())
            velocity.Y += gravity * (float)delta;
    }

    private void UpdateAnimation(Vector2 inputAxis){
        if (inputAxis != Vector2.Zero){
            animatedSprite2D.FlipH = (inputAxis < Vector2.Zero);
            animatedSprite2D.Play("run");
        } else{
            animatedSprite2D.Play("idle");
        }

        if (!IsOnFloor()){
            animatedSprite2D.Play("jump");
        }
    }
}
