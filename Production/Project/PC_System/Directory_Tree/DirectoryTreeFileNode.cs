using Godot;
using System;

namespace FileSystem
{
    public partial class DirectoryTreeFileNode : DirectoryTreeNode
    {
        protected String type;

        public String Type
        {
            get => type;
        }

        protected String fileFormattedContent; // Content that the reader program will decode to execute

        public String FileFormattedContent
        {
            get => fileFormattedContent;
        }

        protected String fileASCIIContent; // Content visible when opening the file in notepad

        public String FileASCIIContent
        {
            get => fileASCIIContent;
        }

        public DirectoryTreeFileNode(DirectoryTreeNode parent = null, String name = "File", String type = "#") : base(parent, name)
        {
            this.type = type;
        }

        public override DirectoryTreeFileNode GetCopy()
        {
            DirectoryTreeFileNode copy = new DirectoryTreeFileNode(parent, "cpy-"+name, type);
            copy.fileFormattedContent = fileFormattedContent;
            copy.fileASCIIContent = fileASCIIContent;

            return copy;
        }
    }
}
