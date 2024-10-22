partial class Program
{
    private static void Main()
    {
        string sheet = "1,c|1,c|1,g|1,g|1,a|1,a|2,g|1,f|1,f|1,e|1,e|1,d|1,d|2,c|2,n|1,f#|1,f#|1,g|1,a|1,a|1,g|1,f#|1,e|1,d|1,d|1,e|1,f#|1.5,f#|0.5,e|2,e|1,f#|1,f#|1,g|1,a|1,a|1,g|1,f#|1,e|1,d|1,d|1,e|1,f#|1.5,e|0.5,d|2,d|" +
            "1,e|1,e|1,f#|1,d|1,e|0.5,f#|0.5,g|1,f#|1,d|1,e|0.5,f#|0.5,g|1,f#|1,e";

        SheetSplitter(sheet, BPM(172));

    }

    private static decimal BPM(decimal bpm)
    {
        decimal tempo = 60 / bpm;
        return tempo;
    }

    private static decimal Note(decimal rate)
    {
        decimal note = rate / 1;
        return note;
    }

    private static decimal ConvertToMiliSeconds(decimal bpm, decimal note)
    {
        decimal milisec = (note * 1000) * bpm;
        
        return milisec;
    }
    private static void Player(string note, decimal bpm)
    {
        string[] parts = note.Split(',');

        decimal milisecs = ConvertToMiliSeconds(bpm, Note(Convert.ToDecimal(parts[0])));
        int frequency;

        if (parts[1] == "n")
        {
            Thread.Sleep(Convert.ToInt32( milisecs));
        }
        else
        {
            if (parts.Length < 3)
            {
                frequency = Frequency(parts[1], "null");
            }
            else
            {
                frequency = Frequency(parts[1], parts[2]);
            }

            Console.Beep(frequency, Convert.ToInt32(milisecs));
        }

        
    }

    private static void SheetSplitter(string sheet, decimal bpm)
    {
        string[] notes = sheet.Split("|");
        foreach (string note in notes)
        {
            Player(note, bpm);
        }
    }

    private static int Frequency(string note, string key)
    {
        int frequency = 440;
        Dictionary<string, int> notes = [];
        notes.Add("a", 440);
        notes.Add("b", 494);
        notes.Add("c", 262);
        notes.Add("d", 294);
        notes.Add("e", 330);
        notes.Add("f", 350);
        notes.Add("g", 392);
        notes.Add("a#", 466);
        notes.Add("c#", 277);
        notes.Add("d#", 311);
        notes.Add("f#", 370);
        
        if (key == "null")
        {
            frequency = notes[note.ToLower()];
        }
        else if (key == "-")
        {
            frequency = notes[note.ToLower()] / 2;
        }
        else { frequency = notes[note.ToLower()] * 2; }

        return frequency;
    }

}