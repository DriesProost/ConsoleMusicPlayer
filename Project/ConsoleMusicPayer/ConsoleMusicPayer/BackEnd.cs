using EnumOptions;
using WMPLib;

namespace ConsoleMusicPayer
{
    public class BackEnd
    {
        public WindowsMediaPlayer player = new WindowsMediaPlayer();
        private string musicFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
        private FrontEnd frontEnd = new FrontEnd();

        public void ShowSongInfo()
        {
            Console.WriteLine("\n|| Title: " + player.currentMedia.getItemInfo("Title"));
            Console.WriteLine("|| Artist: " + player.currentMedia.getItemInfo("Artist"));
            Console.WriteLine("|| Album: " + player.currentMedia.getItemInfo("Album"));
        }

        private void GiveVolume()
        {
            int volume = player.settings.volume;
            int volumeDelen = volume / 5;
            Console.ForegroundColor = ConsoleColor.Green;
            string volumeBars = new string('#', volumeDelen);
            Console.Write(volumeBars);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            string restVolume = new string('#', (20 - volumeDelen));
            Console.Write(restVolume);
            Console.ResetColor();
        }

        private void PausePlayer()
        {
            player.controls.pause();
            Console.ResetColor();
            Console.Clear();
            frontEnd.PrintTitle();
            PrintVolume();
            ShowSongInfo();
            Console.WriteLine("\n Song paused");
        }

        private void PlayPlayer()
        {
            player.controls.play();
            Console.ResetColor();
            Console.Clear();
            frontEnd.PrintTitle();
            PrintVolume();
            ShowSongInfo();
            Console.WriteLine("\n Resuming song");
        }

        private void ChangeVolume()
        {
            Console.Clear();
            frontEnd.PrintTitle();
            PrintVolume();
            ShowSongInfo();
            int volume = player.settings.volume;
            Console.ResetColor();
            Console.WriteLine("\n Enter a value between 0 - 100 to change the volume level ");
            while (!int.TryParse(Console.ReadLine(), out volume) || (volume > 100) || (volume < 0))
            {
                Console.Clear();
                frontEnd.PrintTitle();
                PrintVolume();
                ShowSongInfo();
                Console.WriteLine("/n Please Enter a valid numerical value between 0 - 100: ");
            }
            player.settings.volume = volume;
            Console.Clear();
            frontEnd.PrintTitle();
            PrintVolume();
            ShowSongInfo();
        }

        private void MutePlayer()
        {
            player.settings.mute = true;
            Console.ResetColor();
            Console.Clear();
            frontEnd.PrintTitle();
            PrintVolume();
            ShowSongInfo();
            Console.WriteLine("\n Muted");
        }

        private void UnmutePlayer()
        {
            player.settings.mute = false;
            Console.ResetColor();
            Console.Clear();
            frontEnd.PrintTitle();
            PrintVolume();
            ShowSongInfo();
            Console.WriteLine("\n unmuted");
        }

        private void StopSong()
        {
            player.controls.stop();
            Console.ResetColor();
            Console.Clear();
            frontEnd.PrintTitle();
            PrintVolume();
            ShowSongInfo();
            Console.WriteLine("\n Stopping song");
        }

        private void QuitProgram()
        {
            player.controls.stop();
            Console.ResetColor();
            Console.Clear();
            frontEnd.PrintTitle();
            PrintVolume();
            ShowSongInfo();
            Console.WriteLine("\n Quitting program");
        }

        private void PrintVolume()
        {
            int huidigVolume = player.settings.volume;
            Console.WriteLine($"Volume: {huidigVolume}");
            GiveVolume();
        }

        private void PlayNextSong()
        {
            Console.Clear();
            frontEnd.PrintTitle();
            PrintVolume();
            Console.ResetColor();
            player.URL = Path.Combine(musicFolder, GetUserChoice());
            Console.WriteLine("\n playing different song");
            Console.Clear();
            frontEnd.PrintTitle();
            PrintVolume();
            ShowSongInfo();
        }

        public string GetUserChoice()
        {
            Console.WriteLine("\n Enter the pathway of the desired song: ");
            string input = Console.ReadLine();
            string choice = TrimInput(input);
            return choice;
        }

        private string TrimInput(string input)
        {
            string trimmed = input.Trim('"');
            return trimmed;
        }

        public void MusicPlayerLoop(string trimmed)
        {
            player.URL = Path.Combine(musicFolder, trimmed);

            int menuChoice = 0;
            PrintVolume();
            ShowSongInfo();
            Console.WriteLine("\nIf no music is playing, you may have entered a wrong input, or the player does not support the file type. Try again with a different file.");

            while (menuChoice != 7)
            {
                Console.WriteLine(" \n 1. Pause \n 2. Play \n 3. Volume wijzigen \n 4. mute \n 5. unmute  \n 6. stop \n 7. quit \n 8. Different song");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.ResetColor();
                while (!int.TryParse(Console.ReadLine(), out menuChoice))
                {
                    Console.Clear();
                    frontEnd.PrintTitle();
                    Console.WriteLine("Please Enter a valid numerical value!");
                    Console.WriteLine(" 1. Pause \n 2. Play \n 3. Volume wijzigen \n 4. mute \n 5. unmute  \n 6. stop \n 7. quit \n 8. Different song");
                }
                switch (menuChoice)
                {
                    case (int)Options.Pause:
                        PausePlayer();
                        break;

                    case (int)Options.Play:
                        PlayPlayer();
                        break;

                    case (int)Options.ChangeVolume:
                        ChangeVolume();

                        break;

                    case (int)Options.Mute:
                        MutePlayer();

                        break;

                    case (int)Options.Unmute:
                        UnmutePlayer();

                        break;

                    case (int)Options.Stop:
                        StopSong();

                        break;

                    case (int)Options.Quit:
                        QuitProgram();

                        break;

                    case (int)Options.PlayNewSong:
                        PlayNextSong();

                        break;

                    default:
                        Console.Clear();
                        frontEnd.PrintTitle();
                        Console.ResetColor();
                        Console.WriteLine("This is not a valid option. Choose: ");
                        break;
                }
            }
        }
    }
}