using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleBlockchain
{
    /// <summary>
    /// a block in a blockchain
    /// </summary>
    public class Block
    {
        /// <summary>
        /// The index
        /// </summary>
        public string index;
        /// <summary>
        /// The timestamp
        /// </summary>
        public string timestamp;
        /// <summary>
        /// The proof of work
        /// </summary>
        public string proof;
        /// <summary>
        /// The previous hash
        /// </summary>
        public string previousHash;

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public string ToString()
        {
            return String.Format("index:{0}, timestamp:{1}, proof:{2}, previousHash:{3}", index, timestamp, proof, previousHash);
        }
    }
}
