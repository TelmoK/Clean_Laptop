#include <iostream>
#include <vector>
#include <list>

class DirectoryTreeNode
{
	DirectoryTreeNode* parent;
	std::string properties;

public:

	DirectoryTreeNode(DirectoryTreeNode* _parent = nullptr) : parent(_parent) {}
	DirectoryTreeNode* get_parenth() { return parent; }
	virtual DirectoryTreeNode* get_copy() = 0;
	void update_root_path();
};

class DirectoryTreeFolderNode : public DirectoryTreeNode
{
	std::list<DirectoryTreeNode*> folders;
	std::list<DirectoryTreeNode*> files;

public:

	DirectoryTreeFolderNode(DirectoryTreeNode* _parent = nullptr) : DirectoryTreeNode(_parent) {}

	DirectoryTreeNode* get_copy() override {}
};

class DirectoryTreeFileNode : public DirectoryTreeNode
{
	std::string file_content;
	std::string file_type;

public:

	DirectoryTreeFileNode(DirectoryTreeNode* _parent = nullptr, std::string _file_type) 
		: DirectoryTreeNode(_parent), file_type(_file_type) {}

	DirectoryTreeFileNode* get_copy() override
	{
		DirectoryTreeFileNode* copy = new DirectoryTreeFileNode(nullptr, file_type);
		copy.file_content = file_content;

		return copy;
	}

	std::string get_file_type() { return file_type; }
	
};

class DirectoryTree
{
	DirectoryTreeFolderNode* root;

	DirectoryTreeNode* clipboard_dir_node;

public:

	DirectoryTree(){}
}

int main()
{

}