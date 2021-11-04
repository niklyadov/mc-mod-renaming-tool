using System.IO;
using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json;

namespace MinecraftModRenamingTool.Extractor
{
    public class ForgeModInfoExtractor : ModInfoExtractor
    {
        public ForgeModInfoExtractor(ResourceWorker resourceWorker) : base(resourceWorker)
        {
        }

        public override ModInfo Retrieve()
        {
            using (var stream = ResourceWorker.GetFile("mcmod.info"))
            {
                if (stream is null || !stream.CanRead)
                    return null;
                
                using (var reader = new StreamReader(stream))
                {
                    dynamic modInfo = JsonConvert.DeserializeObject(reader.ReadToEnd());

                    if (modInfo is null)
                        return null;

                    
                    try
                    {
                        modInfo = modInfo.modList;
                    }
                    catch (RuntimeBinderException e)
                    {
                        
                    } 
                    
                    
                    return new ModInfo
                    {
                        MinecraftVersion = modInfo[0].mcversion,
                        ModVersion = modInfo[0].version,
                        ModName = modInfo[0].name
                    };
                }
            }
        }
    }
}