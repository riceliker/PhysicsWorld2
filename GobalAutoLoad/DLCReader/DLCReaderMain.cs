using Godot;
using System.Collections.Generic; 
/*
	This class only two functions:
	1. Store all DLC Data as DLC Information Package
	2. Get DLC From GetDLCFromRes And GetDLCFromUser
*/

public partial class DLCReaderMain : Node
{
	private int the_number_of_res_DLC;
	public override void _Ready()
	{
		GD.Print("DLCReader: Start Loading.");
		var DLC_loader = new GetDLCByLoadPath();
		DLC_loader.StartLoad("res://DLCLocal");
		GD.Print("DLCReader: All DLC has been read.");
		var character_loader = new CharacterReaderMain();
		character_loader.loadCharacterByDLCInformationPackage();
		
	}
	
}
