using Godot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FileSystem
{
    public partial class DirectoryTreeFolderNode : DirectoryTreeNode
    {
        private List<DirectoryTreeFolderNode> subfolders;

        public ReadOnlyCollection<DirectoryTreeFolderNode> Subfolders
        {
            get => subfolders.AsReadOnly();
        }

        private List<DirectoryTreeFileNode> files;

        public ReadOnlyCollection<DirectoryTreeFileNode> ContainedFiles
        {
            get => files.AsReadOnly();
        }

        public DirectoryTreeFolderNode(DirectoryTreeNode parent = null, String name = "File") : base(parent, name)
        {
            subfolders = new List<DirectoryTreeFolderNode>();
            files = new List<DirectoryTreeFileNode>();
        }
    
        public override DirectoryTreeFolderNode GetCopy()
        {
            DirectoryTreeFolderNode copy = new DirectoryTreeFolderNode(parent, name);

            List<DirectoryTreeFolderNode> subfoldersCopy = new List<DirectoryTreeFolderNode>();

            foreach(DirectoryTreeFolderNode folder in subfolders)
            {
                subfoldersCopy.Add(folder.GetCopy());
            }
                
            List<DirectoryTreeFileNode> filesCopy = new List<DirectoryTreeFileNode>();

            foreach(DirectoryTreeFileNode file in files)
            {
                filesCopy.Add(file.GetCopy());
            }

            copy.subfolders = subfoldersCopy;
            copy.files = filesCopy;

            return copy;
        }

        public void addFolder(DirectoryTreeFolderNode folder)
        {
            subfolders.Add(folder);
        }

        public void addFile(DirectoryTreeFileNode file)
        {
            files.Add(file);
        }
    }
}

