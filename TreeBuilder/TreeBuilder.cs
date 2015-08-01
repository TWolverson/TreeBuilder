using System;
using System.Collections.Generic;
using Xunit;

namespace TreeBuilder
{
    public interface ITreeBuilder
    {
        ITreeBuilder Branch(Func<ITreeBuilder, ITreeBuilder> tree);

        ITreeBuilder Leaf();
    }

    public class TreeBuilder : ITreeBuilder
    {
        public TreeBuilder()
        {
            _parentStack = new Stack<Branch>();
        }

        private readonly Stack<Branch> _parentStack;

        public ITreeBuilder Branch(Func<ITreeBuilder, ITreeBuilder> tree)
        {

            var node = new Branch();
            _parentStack.Peek().AddChild(node);
            _parentStack.Push(node);
            tree(this);
            _parentStack.Pop();
            return this;
        }

        public ITreeBuilder Leaf()
        {
            _parentStack.Peek().AddChild(new Leaf());
            return this;
        }

        [Fact]
        public void AcceptsLeaf()
        {

        }
    }

    public class Node
    {

    }

    public class Branch : Node
    {
        public Branch()
        {
            _childNodes = new List<Node>();
        }

        public void AddChild(Node child)
        {
            _childNodes.Add(child);
        }

        private readonly ICollection<Node> _childNodes;
    }

    public class Leaf : Node
    {

    }
}