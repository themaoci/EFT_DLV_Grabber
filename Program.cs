using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFT_DLV_Grabber
{
    class Program
    {
        static void Main(string[] args)
        {
            string BSGLogsLocation = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Battlestate Games\BsgLauncher\Logs";
            Console.WriteLine("");
            Console.WriteLine(">> Automatic DownloadLink Creator from game launcher logs <<");
            Console.WriteLine("! Created by TheMaoci !");
            if (Directory.Exists(BSGLogsLocation))
            {
                Console.WriteLine("Logs Location Detected at: " + BSGLogsLocation);
            }
            else 
            {
                Console.WriteLine("Battlestate Launcher logs not found make sure you have game logs from their launcher.\nGameLogs should be located at: " + BSGLogsLocation);
            }

            List<string> fileList = Directory.GetFiles(BSGLogsLocation).ToList();
            List<string> current = new List<string>();
            for(int i = 0; i < fileList.Count; i++)
            {
                if (fileList[i].Contains("BSG_Launcher_"))
                {
                    //File.ReadLines("").Any(line => line.Contains("eft-store.com/client/live/updates/") && line.Contains(".update"));
                    foreach (var line in File.ReadAllLines(fileList[i]))
                    {
                        // older then 12.11 logs finder
                        if (line.Contains("Start downloading file") && line.Contains("eft-store.com/client/live/updates/") && line.Contains(".update"))
                        {
                            int start = line.IndexOf("http://");
                            int end = line.IndexOf(".update to");
                            if (start != -1 && end != -1)
                            {
                                string replacedLine = line.Substring(start, end - start);
                                string[] splitted = replacedLine.Split('/');
                                string endstring = splitted[splitted.Length - 2];
                                current.Add(endstring);
                            }
                        }
                        if (line.Contains("Start downloading file") && line.Contains("client/live/distribs/") && line.Contains("/Unpacked/ConsistencyInfo"))
                        {
                            int start = line.IndexOf("http://");
                            int end = line.IndexOf("/Unpacked/ConsistencyInfo");
                            if (start != -1 && end != -1)
                            {
                                string replacedLine = line.Substring(start, end - start);
                                string[] splitted = replacedLine.Split('/');
                                string endstring = splitted[splitted.Length - 1];
                                current.Add(endstring);
                            }
                        }
                        // 12.11+ logs finder
                        if (text2.Contains("(DWN") && text2.Contains("/client/live/updates/") && text2.Contains(".update "))
                        {
                            int num = text2.IndexOf("/client/live/updates/");
                            int num2 = text2.IndexOf(".update ");
                            if (num != -1 && num2 != -1)
                            {
                                string[] array2 = text2.Substring(num, num2 - num).Split(new char[]
                                {
                                    '/'
                                });
                                string item = array2[array2.Length - 2];
                                list2.Add(item);
                            }
                        }
                    }
                }
                   
            }
            Console.WriteLine("--------------------");
            Console.WriteLine($"Detected 'Game Versions' saved in logs on your machine:");
            Console.WriteLine($"");
            List<string> gameVersionsDisplayed = new List<string>();
            for (int i = 0; i < current.Count; i++) {
                string[] splittedString = current[i].Split('_');
                if (splittedString[0].Contains("-"))
                    splittedString[0] = splittedString[0].Split('-')[1];

                if (gameVersionsDisplayed.IndexOf(splittedString[0]) == -1) {
                    gameVersionsDisplayed.Add(splittedString[0]);

                    Console.WriteLine($"GameVersion: {splittedString[0]}, GUID: {splittedString[1]}");
                    Console.WriteLine($"DownloadLink: http://cdn-11.eft-store.com/client/live/distribs/{splittedString[0]}_{splittedString[1]}/Client.{splittedString[0]}.zip");
                    Console.WriteLine("");
                }
            }
            
                
            Console.ReadKey();
        }
    }
}
