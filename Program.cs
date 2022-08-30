using System.Diagnostics;
using System.Text.Json;

namespace PostgresEF
{
    internal class Program
    {
        static string GetReadableTimeByMs(long ms)
        {
            // Based on answers https://stackoverflow.com/questions/9993883/convert-milliseconds-to-human-readable-time-lapse
            TimeSpan t = TimeSpan.FromMilliseconds(ms);
            if (t.Hours > 0) return $"{t.Hours} hours {t.Minutes} minutes {t.Seconds} seconds";
            else if (t.Minutes > 0) return $"{t.Minutes}minutes {t.Seconds} seconds";
            else if (t.Seconds > 0) return $"{t.Seconds} seconds";
            else return $"{t.Milliseconds} milliseconds";
        }

        static async Task Main(string[] args)
        {
            LogMaker.Log("Hello, Postgres!");
            Configurations.Init();
            LocalContext localContext = new LocalContext();
            PostgresContext remoteContext = new PostgresContext();
            Console.WriteLine(Configurations.PostgresConnectionString);

            //// for testing purposes
            //azureContext.Database.EnsureDeleted();
            //azureContext.Database.EnsureCreated();
            //SeedData();

            await Task.Delay(1);  // stop compiler warnings on Linux


            if (File.Exists("log.html"))
                File.Delete("log.html");

            Stopwatch sw = Stopwatch.StartNew();

            List<WowAuction> localAuctions = localContext.WowAuctions.ToList();
            LogMaker.Log($"Finding that {localAuctions.Count} stored locally took {GetReadableTimeByMs(sw.ElapsedMilliseconds)}.");
            sw.Restart();

            List<WowAuction> remoteAuctions = remoteContext.WowAuctions.ToList();

            LogMaker.Log($"Finding that {remoteContext.WowAuctions.Count()} stored remotely took {GetReadableTimeByMs(sw.ElapsedMilliseconds)}.");
            sw.Restart();

            List<WowAuction> addadbleAuctions = new();
            List<WowAuction> updatableAuctions = new();

            foreach (WowAuction auction in localAuctions)
            {
                auction.FirstSeenTime = DateTime.SpecifyKind(auction.FirstSeenTime, DateTimeKind.Utc);
                auction.LastSeenTime = DateTime.SpecifyKind(auction.LastSeenTime, DateTimeKind.Utc);

                WowAuction trial = remoteAuctions.FirstOrDefault(l => l.Id == auction.Id);
                if (trial == null)
                {
                    addadbleAuctions.Add(auction);
                }
                else
                {
                    updatableAuctions.Add(auction);
                }
            }

            LogMaker.Log($"Finding {addadbleAuctions.Count} auctions are to be added and {updatableAuctions.Count} auctions are to be updated took {GetReadableTimeByMs(sw.ElapsedMilliseconds)}.");
            sw.Restart();


            remoteContext.WowAuctions.AddRange(addadbleAuctions);
            remoteContext.SaveChanges();
            remoteContext.WowAuctions.UpdateRange(updatableAuctions);
            remoteContext.SaveChanges();

            LogMaker.Log($"{localAuctions.Count} processed in {GetReadableTimeByMs(sw.ElapsedMilliseconds)}.");
        }
    }
}