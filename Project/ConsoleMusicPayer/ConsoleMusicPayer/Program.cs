using ConsoleMusicPayer;

FrontEnd frontEnd = new FrontEnd();
frontEnd.PrintTitle();

BackEnd backEnd = new BackEnd();

string input = backEnd.GetUserChoice();
backEnd.MusicPlayerLoop(input);