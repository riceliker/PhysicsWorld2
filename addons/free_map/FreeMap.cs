#if TOOLS
using Godot;
using System;
using System.ComponentModel;

/// <summary>
/// The basic node in FreeMap Plugin. 
/// If you want to click the element button of FreeMap and add mesh, you should add it first."
/// </summary>

[Tool]
[GlobalClass]
[Description("The basic node in FreeMap Plugin. If you want to click the element button of FreeMap and add mesh, you should add it first.")]
public partial class FreeMap : EditorPlugin
{
	private FreeMapBottomDock free_map_button_dock;
    [Obsolete]
    public override void _EnterTree()
    {
        free_map_button_dock = new FreeMapBottomDock();
		// It will be removed, But I don't know the new method.
        AddControlToDock(DockSlot.Bottom, free_map_button_dock, null);
		

    }
	[Obsolete]
	public override void _ExitTree()
    {
        if (free_map_button_dock != null)
        {
            // It will be removed, But I don't know the new method.
            RemoveControlFromDocks(free_map_button_dock);
            free_map_button_dock.QueueFree();
            free_map_button_dock = null;
        }
    }
	
}
#endif
