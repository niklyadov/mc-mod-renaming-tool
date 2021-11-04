using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MinecraftModRenamingTool.Extractor;

namespace MinecraftModRenamingTool
{
    class RenamingTool
    {
        private readonly string _directory;
        private readonly Dictionary<string, string> _renamedFiles = new ();
        public RenamingTool(string directory)
        {
            _directory = directory;
        }

        public RenamingTool DoRenaming()
        {
            var files = Directory.GetFiles(_directory, "*.jar")
                .Select(x => new FileInfo(x));
            
            Parallel.ForEach(files, file =>
            {
                var resource = new ResourceWorker(file.FullName);
                var modInfo = new ForgeModInfoExtractor(resource).Retrieve() ?? 
                              new FabricModInfoExtractor(resource).Retrieve();

                if (modInfo is null)
                {
                    return;
                }

                var newModName = Path.Combine(file.DirectoryName ?? "", modInfo.ToString());
                var newFileName = newModName + ".jar";
                
                if (file.FullName.Equals(newFileName)) return;

                file.MoveTo(newFileName);
                _renamedFiles.Add(file.FullName, newFileName);
            });
            
            return this;
        }

        public RenamingTool UndoRenaming()
        {
            Parallel.ForEach(_renamedFiles, file =>
            {
                new FileInfo(file.Value).MoveTo(file.Key);
            });

            return this;
        }

        public string GetResult()
            => string.Join('\n', _renamedFiles.Select(x => $"{x.Key} ==> {x.Value}"));
    }
}