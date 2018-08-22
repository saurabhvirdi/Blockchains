using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy;

namespace SampleBlockchain
{
    /// <summary>
    /// Main module for nancy
    /// </summary>
    public class MainModule : NancyModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainModule"/> class.
        /// </summary>
        public MainModule()
        {
            // get the blockchain instance
            var blockchain = Program.blockchain;

            // request handler for the root request
            Get["/"] = x => { return "Hello friends!!"; };

            // request handler for mine block in the blockchain
            Get["/mineblock"] = _ =>
                {
                    // mine a new block for this blockchain
                    var block = blockchain.MineBlock();

                    // create a new dictionary for response in JSON format
                    var dict = new Dictionary<string, object>() 
                    {
                        {"message", "Congratulations, you have successfully mined a block!"},
                        {"block", block}
                    };

                    return Response.AsJson<Dictionary<String, object>>(dict);
                };

            // request handler to get the entire chain
            Get["/getchain"] = x =>
            {
                // create a new dictionary for response in JSON format
                var dict = new Dictionary<string, object>() 
                    {
                        {"chain", blockchain.chain},
                        {"length", blockchain.chain.Count}
                    };

                return Response.AsJson<Dictionary<String, object>>(dict);
            };

            // request handler for validating if the blockchain is valid
            Get["/ischainvalid"] = x =>
            {
                // check if the blockchain is valid
                var isValid = blockchain.IsChainValid();

                // create a new dictionary for response in JSON format
                var dict = new Dictionary<string, object>() 
                    {
                        {"is_blockchain_valid", isValid},
                        {"length", blockchain.chain.Count}
                    };

                return Response.AsJson<Dictionary<String, object>>(dict);
            };
        }
    }
}
