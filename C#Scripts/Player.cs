Player.cs
using Godot;

public partial class CharacterBody2D : Node2D
{
    // Member Variables here.
    private const int SNAP_DISTANCE = 8;  // Adjust this value based on your needs
    private const float SPEED = 60f;
    private string currentDirection = "Down";
    private bool isAttacking = false;
    private AnimationPlayer animationPlayer;

    // Change _PhysicsProcess to virtual or abstract
    public virtual void _PhysicsProcess(float delta)
    {
        PlayerMovement(delta);
    }

    private void PlayerMovement(float delta)
    {
        var inputVector = new Vector2();
        if (!isAttacking)
        {
            if (Input.IsActionPressed("ui_right") || Input.IsActionPressed("ui_left"))
            {
                if (currentDirection == "Up" || currentDirection == "Down")
                    Position = new Vector2(Position.x, Position.y + SnapTo((int)Position.y, SNAP_DISTANCE));
            
                inputVector.x += Input.IsActionPressed("ui_right") ? 1 : -1;
                currentDirection = Input.IsActionPressed("ui_right") ? "Right" : "Left";
            }
            else if (Input.IsActionPressed("ui_down") || Input.IsActionPressed("ui_up"))
            {
                if (currentDirection == "Right" || currentDirection == "Left")
                    Position = new Vector2(Position.x + SnapTo((int)Position.x, SNAP_DISTANCE), Position.y);
            
                inputVector.y += Input.IsActionPressed("ui_down") ? 1 : -1;
                currentDirection = Input.IsActionPressed("ui_down") ? "Down" : "Up";
            }
        }
    }

        // Check for attack input
        if (Input.IsActionJustPressed("ui_a_button"))
        {
            isAttacking = true;
            animationPlayer.Play("Attack" + currentDirection);
            // await animationPlayer.AnimationFinished; // C# does not support await in this context
            isAttacking = false;
        }

        PlayAnimation();
        var velocity = inputVector.Normalized() * SPEED;
        Position = new Vector2(Mathf.Round(Position.x), Mathf.Round(Position.y));
        MoveAndSlide(velocity);

        if (velocity == Vector2.Zero && !isAttacking)
        {
            animationPlayer.Stop();
        }
    }

    private void PlayAnimation()
    {
        if (!isAttacking)
        {
            animationPlayer.Play(currentDirection);
        }
    }

    private static int SnapTo(int value, int gridSize)
    {
        var remainder = value % gridSize;
        var halfway = gridSize / 2;

        if (remainder >= halfway)
        {
            return value + (gridSize - remainder);
        }
        else
        {
            return value - remainder;
        }
    }
}