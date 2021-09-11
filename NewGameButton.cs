using Godot;
using System;


public class NewGameButton : CollisionShape2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	[Export]
	public string name = "";
	[Export]
	public string description = "";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//var dynamic_font = new DynamicFontData();
		//dynamic_font.Load
		var font = ResourceLoader.Load("res://Handlee-Regular.ttf");
		font = (DynamicFontData)font;
		
		var label = new RichTextLabel();
		//label.bbcode_enabled = true
		label.BbcodeEnabled = true;
		label.PushColor(new Color(1,0,1,1));
		label.Set("Handlee-Regular",font);
		label.Text = description;
		label.Visible = true;
		
		AddChild(label);
		//GD.Print(label.GetPos());
		
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
				
				switch(this.name){
					case "NewGame":
						GD.Print(this.name);
						return;
					case "LoadGame":
						GD.Print(this.name);
						return;
					case "Settings":
						GD.Print(name);
						//GetTree().ChangeScene("res://Scenes/Settings.tscn");
						
						//PackedScene SettingsResource = (PackedScene)GD.Load("res://Settings.tscn");
						GetTree().ChangeScene("res://Settings.tscn");
						
						
						//GetTree().Root.CallDeffered("add_child", Settings);
						QueueFree();
						return;
					case "Quit":
						GetTree().Quit();
						return;
					case "Settings-Back":
						// Settings Menu Back Button
						GD.Print(name);
						GetTree().ChangeScene("res://MainMenu.tscn");
						return;
					
				}
				if (this.name == "")
				{
					GD.Print("Missing name");
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

