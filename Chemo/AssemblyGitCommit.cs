using System;

namespace Chemo
{
    class AssemblyGitCommit : Attribute
    {
        public AssemblyGitCommit(string commitHash)
        {
            CommitHash = commitHash;
        }

        public string CommitHash { get; }
    }
}
