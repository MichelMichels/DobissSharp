using MichelMichels.DobissSharp.Models;
using System.Diagnostics.CodeAnalysis;

namespace MichelMichels.DobissSharp.Comparers
{
    internal class SubjectComparer : IEqualityComparer<Subject>
    {
        public bool Equals(Subject x, Subject y)
        {
            return x.Channel == y.Channel && x.Address == y.Address;
        }

        public int GetHashCode([DisallowNull] Subject obj)
        {
            return obj.Address.GetHashCode() + obj.Channel.GetHashCode();
        }
    }
}
