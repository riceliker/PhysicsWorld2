using Godot;
using System;

namespace PhysicsWorld.Src.DLCManager
{
    /// <summary>
    /// This interface will help you to get JSON key easily
    /// Use `(this as IGetIcon).setIcon();` to set Texture2D
    /// </summary>
    public interface IGetIcon
    {
        public Texture2D icon {get;set;}
        public string path {get;init;}
        public void setIcon()
        {
            string icon_path = path.PathJoin("icon.png");
            if (ResourceLoader.Exists(icon_path))
            {
                Texture2D loadedTex = ResourceLoader.Load<Texture2D>(icon_path);
                Image image_icon = loadedTex.GetImage();
                image_icon.Resize(160, 160, Image.Interpolation.Bilinear);
                icon = ImageTexture.CreateFromImage(image_icon);
            }
            else
            {
                GD.PushWarning($"DLCData: The icon image in DLC({path}) was lost!");
            }
        }
    }
}
