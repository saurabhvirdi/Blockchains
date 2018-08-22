using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleBlockchain
{
    /// <summary>
    /// My Blockchain class, implementing one possible ways to create a blockchain from scratch
    /// </summary>
    public class Blockchain
    {
        /// <summary>
        /// The chain
        /// </summary>
        public List<Block> chain;

        /// <summary>
        /// Initializes a new instance of the <see cref="Blockchain"/> class.
        /// </summary>
        public Blockchain()
        {
            // allocate memory to the list
            chain = new List<Block>();

            // create a genesis block
            CreateBlock("1", "0");
        }

        /// <summary>
        /// Creates the block.
        /// </summary>
        /// <param name="newProof">The new Proof.</param>
        /// <param name="previousHash">The previous hash.</param>
        /// <returns></returns>
        public Block CreateBlock(string newProof, string previoushash)
        {
            // create a new block and add it to the blockchain
            var block = new Block();
            block.index = String.Format("{0}", chain.Count + 1);
            block.timestamp = GetTimestamp();
            block.proof = newProof;
            block.previousHash = previoushash;

            // add the new block to the blockchain
            chain.Add(block);

            // return the new block for display purpose
            return block;
        }

        /// <summary>
        /// Proofs the of work.
        /// </summary>
        /// <param name="previousProof">The previous Proof.</param>
        /// <returns></returns>
        public string ProofOfWork(string previousProof)
        {
            int newProof = 1;
            var prevProof = Convert.ToInt32(previousProof);
            var newProofFound = false;

            while (!newProofFound)
            {
                var proof = CalculateProof(prevProof, newProof);

                if (IsProofHashValid(proof))
                {
                    newProofFound = true;
                }
                else
                {
                    newProof++;
                }
            }

            return newProof.ToString();
        }

        /// <summary>
        /// Calculates the hash.
        /// </summary>
        /// <param name="block">The block.</param>
        /// <returns></returns>
        public string CalculateHash(Block block)
        {
            return BlockUtility.GetSha256Hash(block.ToString());
        }

        /// <summary>
        /// Determines whether [is chain valid].
        /// </summary>
        /// <returns></returns>
        public bool IsChainValid()
        {
            Block previousBlock = null;
            foreach (var block in this.chain)
            {
                if (previousBlock == null && block.previousHash.Equals("0"))
                {
                    // this block is the genesis block
                    previousBlock = block;

                    // continue with the loop
                    continue;
                }
                
                // check the current block's previous hash with the calculated hash of the previous block
                if (!block.previousHash.Equals(CalculateHash(previousBlock)))
                {
                    // return false since the previous hash of current block did not match wht calculated hash of previous block
                    return false;
                }

                // get the Proof of previous block
                var previousProof = Convert.ToInt32(previousBlock.proof);
                
                // get the Proof of the current block
                var currentProof = Convert.ToInt32(block.proof);

                // validate Proof of previous block with the Proof of the current block
                var calculatedProof = CalculateProof(previousProof, currentProof);
                var isProofValid = IsProofHashValid(calculatedProof);
                
                if (!isProofValid)
                {
                    return false;
                }

                // set previous block as current block
                previousBlock = block;
            }
            return true;
        }

        /// <summary>
        /// Mines the block.
        /// </summary>
        /// <returns></returns>
        public Block MineBlock()
        {
            // previous block is the last block in the blockchain
            var previousBlock = chain[chain.Count - 1];
            var previousProof = previousBlock.proof;
            
            var newProof = ProofOfWork(previousProof);
            
            var previousHash = CalculateHash(previousBlock);
            var newBlock = CreateBlock(newProof, previousHash);

            return newBlock;
        }

        /// <summary>
        /// Gets the timestamp.
        /// </summary>
        /// <returns></returns>
        private string GetTimestamp()
        {
            // get the unix time, number of seconds from Jan 1, 1970
            return string.Format("{0}", (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds);
        }

        /// <summary>
        /// Determines whether [is hash valid]
        /// </summary>
        /// <param name="hash">The hash.</param>
        /// <returns></returns>
        private bool IsProofHashValid(string hash)
        {
            // this is the difficulty level (in bitcoin this is determined by the algorithm.)
            // The more the number of 0s the higher the difficulty and the lower the value of the hash.

            return hash.StartsWith("0000");
        }

        /// <summary>
        /// Calculates the Proof.
        /// </summary>
        /// <param name="previousProof">The previous Proof.</param>
        /// <param name="currentProof">The current Proof.</param>
        /// <returns></returns>
        private string CalculateProof(int previousProof, int currentProof)
        {
            // this is a random calculation to find out the new Proof which would satisfy the validation criteria
            // in this example I am using (a^2 - b^2 - 2ab)

            return BlockUtility.GetSha256Hash(String.Format("{0}", (Math.Pow(currentProof, 2) - Math.Pow(previousProof, 2) - 2 * currentProof * previousProof)));
        }
    }
}
