using Discord.Commands;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace TrtlBotSharp
{
    public partial class Commands : ModuleBase<SocketCommandContext>
    {
        [Command("hashrate")]
        public async Task HashrateAsync([Remainder]string Remainder = "")
        {
            // Get last block header from daemon and calculate hashrate
            decimal Hashrate = 0;
            JObject Result = Request.RPC(TrtlBotSharp.daemonHost, TrtlBotSharp.daemonPort, "getlastblockheader");
            if (Result.Count > 0 && !Result.ContainsKey("error"))
                Hashrate = (decimal)Result["block_header"]["difficulty"] / 120;

            // Send reply
            await ReplyAsync("The current global hashrate is **" + TrtlBotSharp.FormatHashrate(Hashrate) + "**");
        }

        [Command("difficulty")]
        public async Task DifficultyAsync([Remainder]string Remainder = "")
        {
            // Get last block header from daemon and calculate hashrate
            decimal Difficulty = 0;
            JObject Result = Request.RPC(TrtlBotSharp.daemonHost, TrtlBotSharp.daemonPort, "getlastblockheader");
            if (Result.Count > 0 && !Result.ContainsKey("error"))
                Difficulty = (decimal)Result["block_header"]["difficulty"];

            // Send reply
            await ReplyAsync(string.Format("The current difficulty is **{0:N0}**", Difficulty));
        }

        [Command("height")]
        public async Task HeightAsync([Remainder]string Remainder = "")
        {
            // Get height
            decimal Height = 0;
            JObject Result = Request.GET("http://" + TrtlBotSharp.daemonHost + ":" + TrtlBotSharp.daemonPort + "/getinfo");
            if (Result.Count > 0 && !Result.ContainsKey("error"))
                Height = (decimal)Result["height"];


            // Send reply
            await ReplyAsync(string.Format("The current block height is **{0:N0}**", Height));
        }

        [Command("supply")]
        public async Task SupplyAsync([Remainder]string Remainder = "")
        {
            // Get supply
            decimal Supply = TrtlBotSharp.GetSupply();

            // Send reply
            await ReplyAsync(string.Format("The current circulating supply is **{0:N4}** {1}", Supply, TrtlBotSharp.coinSymbol));
        }

	[Command("dynamit")]
        public async Task DynamitAsync([Remainder]string Remainder = "")
        {
            // Get supply
            decimal Supply = TrtlBotSharp.GetSupply();
	    decimal Height = TrtlBotSharp.GetHeight();
	    decimal Hashrate = TrtlBotSharp.GetHashrate();
            decimal Difficulty = TrtlBotSharp.GetDifficulty(); 
	   
	    string Message =string.Format(  "The current block height is **{0:N0}**" + 
		   	     "\nThe current global hashrate is **" + TrtlBotSharp.FormatHashrate(Hashrate) + "**" + 
			     "\nThe current difficulty is **{1:N0}**" + 
			     "\nThe current circulating supply is **{2:N4}** {3}", Height, Difficulty, Supply, TrtlBotSharp.coinSymbol ); 

	    // Send reply
	    //await ReplyAsync(string.Format("The current block height is **{0:N0}**", Height));
            //await ReplyAsync("The current global hashrate is **" + TrtlBotSharp.FormatHashrate(Hashrate) + "**"); 
	    //await ReplyAsync(string.Format("The current difficulty is **{0:N0}**", Difficulty)); 
	    //await ReplyAsync(string.Format("The current circulating supply is **{0:N4}** {1}", Supply, TrtlBotSharp.coinSymbol));
	    
	    await ReplyAsync(string.Format(Message));

	}
    

    }

}
