namespace MinecraftModRenamingTool.Extractor
{
    public abstract class ModInfoExtractor
    {
        protected readonly ResourceWorker ResourceWorker;
        protected ModInfoExtractor(ResourceWorker resourceWorker)
        {
            ResourceWorker = resourceWorker;
        }
        public abstract ModInfo Retrieve();
    }
}