using System;
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;
using AntiStuck.WinApi;
using Flurl.Http;
using Flurl.Http.Configuration;

namespace AntiStuck
{
    internal static class Program
    {
        private static bool IsGameOnDisplay()
        {
            return NativeImport.GetActiveWindowTitle() == "League of Legends (TM) Client";
        }
        
        private static async Task Main(string[] args)
        {
            NativeImport.AllocConsole();
            FlurlHttp.ConfigureClient(ApiService.BaseUrl, cli =>
                cli.Settings.HttpClientFactory = new UntrustedCertClientFactory());

            var prevGold = 0.0f;
            var count = 0;
            while (true)
            {
                try
                {
                    if (count >= 60)
                    {
                        await QuitGame();
                        return;
                    }
                    
                    if (!(await ApiService.IsLiveGameRunning()) || !IsGameOnDisplay())
                    {
                        count = 0;
                        await Task.Delay(2000);
                        continue;
                    }

                    if ((await ApiService.IsLiveGameRunning()) && !IsGameOnDisplay())
                    {
                        count++;
                        await Task.Delay(1000);
                        continue;
                    }

                    await QuitGame();

                    var gameStatus = await ApiService.GetGameStats();
                    if (gameStatus.GameTime < 91)
                    {
                        await Task.Delay(2000);
                        continue;
                    }

                    var gold = (await ApiService.GetActivePlayerData()).CurrentGold;
                    if (Math.Abs(prevGold - gold) < float.Epsilon)
                    {
                        count++;
                    }
                    else
                    {
                        count = 0;
                    }
                    prevGold = gold;
                }
                catch (Exception ex)
                {
                    await Task.Delay(5000);
                }

                await Task.Delay(1000);
            }
        }

        private static async Task QuitGame()
        {
            Console.WriteLine("Exit game");
            var exitGameBtn = new Point(144, 197);
            var leaveBtn = new Point(251, 113);
            
            await Keyboard.SendKey((ushort)Keyboard.KeyBoardScanCodes.ESC);
            await Task.Delay(2000);
            Mouse.MouseMove(exitGameBtn.X, exitGameBtn.Y);
            await Task.Delay(100);
            await Mouse.MouseClickLeft();
            await Task.Delay(9000);
            Mouse.MouseMove(leaveBtn.X, leaveBtn.Y);
            await Task.Delay(100);
            await Mouse.MouseClickLeft();
            await Task.Delay(100);
            await Task.Delay(10000);
        }
    }
    
    public class UntrustedCertClientFactory : DefaultHttpClientFactory
    {
        public override HttpMessageHandler CreateMessageHandler() {
            return new HttpClientHandler {
                ServerCertificateCustomValidationCallback = (a, b, c, d) => true
            };
        }
    }
}