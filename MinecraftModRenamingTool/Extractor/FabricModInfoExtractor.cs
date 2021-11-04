using System.IO;
using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json;

namespace MinecraftModRenamingTool.Extractor
{
    public class FabricModInfoExtractor : ModInfoExtractor
    {
        public FabricModInfoExtractor(ResourceWorker resourceWorker) : base(resourceWorker)
        {
        }

        public override ModInfo Retrieve()
        {
            using (var stream = ResourceWorker.GetFile("fabric.mod.json"))
            {
                if (stream is null || !stream.CanRead)
                    return null;
                
                using (var reader = new StreamReader(stream))
                {
                    dynamic modInfo = JsonConvert.DeserializeObject(reader.ReadToEnd());
                    
                    if (modInfo is null)
                        return null;
                    
                    var info = new ModInfo
                    {
                        ModVersion = modInfo.version,
                        ModName = modInfo.name
                    };

                    
                    try
                    {
                        info.MinecraftVersion = modInfo.depends.minecraft;
                    }
                    catch (RuntimeBinderException e)
                    {
                        
                    }
                    
                    return info;
                }
            }
        }
    }
}