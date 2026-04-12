using Godot;
using System;

public partial class InformationReader : Node
{
    public override void _Ready()
    {
        new GetDLCByPath("res://DLCLocal");
        new GetDataByDLCList();
    }
}
