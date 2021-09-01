using Godot;
using System;


public class NewGameButton : CollisionShape2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	[Export]
	public string name = "";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
	public override void _Input(InputEvent @event)
	{
		// Mouse in viewport coordinates.
		if (@event is InputEventMouseButton eventMouseButton)
		{
			InputEventMouseButton ev = (InputEventMouseButton)@event;
			
			if (is_point_inside_collision_shape(ev.Position,this) && ev.IsPressed())
			{
				GD.Print("hello");
				
				GD.Print(ev);
				switch(name){
					case "NewGame":
						GD.Print(name);
						return;
					case "LoadGame":
						GD.Print(name);
						return;
					case "Settings":
						GD.Print(name);
						return;
					case "Quit":
						GD.Print(name);
						return;
				}
			}
		}
			//GD.Print("Mouse Click/Unclick at: ", eventMouseButton.Position);
		//else if (@event is InputEventMouseMotion eventMouseMotion)
			//GD.Print("Mouse Motion at: ", eventMouseMotion.Position);

		// Print the size of the viewport.
		//GD.Print("Viewport Resolution is: ", GetViewportRect().Size);
	}
	
	public bool is_point_inside_collision_shape(Vector2 point, CollisionShape2D cs)
	{
		RectangleShape2D r;
		r = new RectangleShape2D();
		Vector2 v;
		v = new Vector2(1,1);
		r.Extents = v;
		
		//var cam = Globals.get("currentCamera");
		
		//Camera2D cam = GetNode<Camera2D>("Camera2D");
		
		//Transform2D t = new Transform2D(0,point + cam.GlobalTransform.origin);
		Transform2D t = new Transform2D(0,point );
		
		bool res = false;
		res = cs.Shape.Collide(cs.GlobalTransform, r, t);
		return res;
	}
}

