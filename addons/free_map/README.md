# Godot-FreeMap-Addon
The Addon in Godot Game Engine, Like GirdMap but you can drag mesh in the any position and degree.
Build Godot Version 4.6.2.stable.mono
It's not support the platform without .NET. If you don't like .NET, I will make the GDScript version in the future

## How To Install
Download the file and open your program.
If you not have any addons, create the `addons` folder under the `res://`.
If not, open the `addons`, copy the `free_map` in your addons folder.
At last, Clicked Project > Project Settings > Plugin, Clicked `Enabled`.

## How To Use
1. Get MeshLibrary. If you have a `.glb` or other file which have some many `Mesh` in the Scene, you must export it as `MeshLibrary`. Clicked Scene > Export.. > MeshLibrary.., Export As `.res` `.meshlib` or `.tres` file.
2. Look at the Dock on the bottom, Clicked `FreeMap`, and Clicked `MeshLibrary`.
3. Drag the `.res` `.meshlib` or `.tres` file in the box.
4. Clicked `Elements` and Clicked `Refresh`, you will see all mesh in there.
5. Open the other Scene and Create a new node `FreeMap`. Choose it.
6. Now you can clicked the element. It will be load under the node `FreeMap`

## F & Q
If you have some question, tell me from `Discussion`
