namespace Graph
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a Disjoint Set Union (DSU) data structure with path compression and union by rank.
    /// </summary>
    public class DSU
    {
        private readonly Dictionary<int, int> parent = [];
        private readonly Dictionary<int, int> rank = [];

        /// <summary>
        /// Finds the representative/root of the set containing the specified element.
        /// </summary>
        /// <param name="x">The element to find.</param>
        /// <returns>The root representative of the set.</returns>
        public int Find(int x)
        {
            if (!this.parent.ContainsKey(x))
            {
                this.parent[x] = x;
                this.rank[x] = 0;
            }

            if (this.parent[x] != x)
            {
                this.parent[x] = this.Find(this.parent[x]);
            }

            return this.parent[x];
        }

        /// <summary>
        /// Merges the sets containing two elements.
        /// </summary>
        /// <param name="x">First element.</param>
        /// <param name="y">Second element.</param>
        public void Union(int x, int y)
        {
            int rootX = this.Find(x);
            int rootY = this.Find(y);

            if (rootX == rootY)
            {
                return;
            }

            if (this.rank[rootX] > this.rank[rootY])
            {
                this.parent[rootY] = rootX;
            }
            else
            {
                this.parent[rootX] = rootY;
                if (this.rank[rootX] == this.rank[rootY])
                {
                    this.rank[rootY]++;
                }
            }
        }
    }
}