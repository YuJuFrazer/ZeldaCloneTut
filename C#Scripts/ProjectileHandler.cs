using Godot;
using System;

public class ProjectileHandler : Node
{
    // Function to spawn projectiles
    public void SpawnProjectile(Node parentNode, Vector2 position, Vector2 direction, string projectileType)
    {
        var projectileScene = (PackedScene)GD.Load("res://Scenes/" + projectileType + ".tscn");

        if (projectileScene != null)
        {
            var newProjectile = (Node2D)projectileScene.Instance();
            newProjectile.Position = position;
            newProjectile.Call("set_direction", direction);
            parentNode.AddChild(newProjectile);  // Add the new projectile under the specified parent node
            return;
        }
        else
        {
            GD.Print("Projectile type not found: ", projectileType);
            return;
        }
    }
}