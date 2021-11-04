namespace MinecraftModRenamingTool
{
    public class ModInfo
    {
        public string ModName { get; set; }
        public string ModVersion { get; set; }
        public string MinecraftVersion { get; set; }

        public override string ToString()
        {
            return $"{ModName.Replace(" ", "_")}_{ModVersion}";
        }
    }
}