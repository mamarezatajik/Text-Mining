using System;
using static System.Console;
using System.Collections.Generic;
using static System.Math;
using System.Text;
using System.Linq;
using System.IO;
using static System.IO.File;
using System.Timers;
using System.Diagnostics;
class DS_Project
{
    public static Dictionary<string, string[]> All_Genres = new Dictionary<string, string[]>();
    public static Dictionary<string, string> All_Words = new Dictionary<string, string>();
    public static string[] Helper_Repeated_Bad_Words = {"centralprocessing", "unit", "centralprocessing unit","von", "neumann", "machine", "fruits", "vegetables", "Rambutan", "popular", "von neumann machine", "fruits vegetables", "Rambutan ", "popular music", "art music", "double bass", "double", "bass", "", "Bone", "animal welfare", "Bone ", "animal", "welfare", "tame", "wild", "dark", "homeless", "meat", "plant", "Lime", "Honeydew", "Orange", "Plum", "phone", "story", "action", "corruption", "illegal", "game", "entertainment", "art", "performance", "work", "kill", "music", "social", "concert", "dance", "culture", "club", "career", "video"};
    public static Dictionary<string, string> Helper_Repeated_Bad_Words2 = new Dictionary<string, string>();






    public static string Helper_Genre_Of_Word(string word)
    {
        if (All_Words.ContainsKey(word))
            return All_Words[word];
        if (word.Contains(':')) return "time";
        if (word.Contains("__")) return "temperature";
        if (word.Split('_')[0] == "PrimeNumber") return "PrimeNumber";
        if (word.Split('_')[0] == "SquareNumber") return "SquareNumber";
        if (word.Split('_')[0] == "CubeNumber") return "CubeNumber";
        
        string genre = "";
        foreach (string key in All_Genres.Keys){
            if (All_Genres[key].Contains(word))
                if (genre.Length>1)
                    genre = genre + "_" + key;
                else
                    genre = key;
        }
        return genre;
    }
    public static Dictionary<string, int> Helper_Genres_of_Sheet(string[] sheet)
    {
        Dictionary<string, int> genres = new Dictionary<string, int>();
        for (int i = 0; i < sheet.Length; i+=11) {
            string[] tmp = sheet[i].Split();
            if (!Helper_Repeated_Bad_Words2.ContainsKey(tmp[0]))      {
                genres.Add(Helper_Genre_Of_Word(tmp[0]), i); continue;
            }
            else if (!Helper_Repeated_Bad_Words2.ContainsKey(tmp[1])) {
                genres.Add(Helper_Genre_Of_Word(tmp[1]), i); continue;
            }
            else if (!Helper_Repeated_Bad_Words2.ContainsKey(tmp[2])) {
                genres.Add(Helper_Genre_Of_Word(tmp[2]), i); continue;
            }
            else if (!Helper_Repeated_Bad_Words2.ContainsKey(tmp[3])) {
                genres.Add(Helper_Genre_Of_Word(tmp[3]), i); continue;
            }
            else if (!Helper_Repeated_Bad_Words2.ContainsKey(tmp[4])) {
                genres.Add(Helper_Genre_Of_Word(tmp[4]), i); continue;
            }
        }
        return genres;
    }
    public static int Helper_Equality_Counter(string[] sheet, string word) 
    {
        // 9 genres in one sheet;
        List<int> start_indices = new List<int>(); 
        string[] tmp;
        string genre_of_word = Helper_Genre_Of_Word(word);
        Dictionary<string, int> dict_of_genres = Helper_Genres_of_Sheet(sheet);

        List<string> genres_of_word = new List<string>(); 
        if (genre_of_word.Contains("_"))
            genres_of_word = genre_of_word.Split("_").ToList();
        else
            genres_of_word.Add(genre_of_word);

        foreach (string genre in genres_of_word)
            if (dict_of_genres.ContainsKey(genre))
                start_indices.Add(dict_of_genres[genre]);

        if (start_indices.Count==0)
            return -1;

        int count = 0;
        int i,j;
        if (word == "Bone ") word = "Bone";
        else if (word == "animal welfare") word = "welfare";
        else if (word == "centralprocessing unit") word = "unit";
        else if (word == "von neumann machine") word = "von";
        else if (word == "fruits vegetables") word = "fruits";
        else if (word == "Rambutan ") word = "Rambutan";
        else if (word == "popular music") word = "popular";
        else if (word == "double bass") word = "double";

        foreach (int index in start_indices)
        {
            i = index;
            while(i-index!=11)
            {
                j = 0;
                tmp = sheet[i].Split();
                while (j!=tmp.Length)
                {
                    if (word == "art music" && tmp[j] == "art" && j<tmp.Length-1 && tmp[j+1]=="music") {
                        count++;
                        j+=1;
                    }
                    else if (tmp[j]==word)
                        count++;
                    j++;
                }
                i++;
            }
        }
        return count;
    }
    public static Dictionary<string, int> Helper_Words_G_in_Sheet_3_8(string[] sheet, string G, int start_index)
    {
        int i,j,locker; string[] tmp;
        Dictionary<string, int> words_of_G_in_sheet = new Dictionary<string, int>();
        for (i = start_index; i < start_index+11; i++) {
            tmp = sheet[i].Split();
            j = 0;
            while (j < tmp.Length) {
                locker=0;
                if (tmp[j] == "animal" && j<tmp.Length-1 && tmp[j+1] == "welfare") {
                    tmp[j] = "animal welfare";
                    locker = 1;
                }
                else if (tmp[j] == "von") {
                    tmp[j] = "von neumann machine";
                    locker = 3;
                }
                else if (tmp[j] == "popular") {
                    tmp[j] = "popular music";
                    locker = 1;
                }
                else if (tmp[j] == "art" && j<tmp.Length-1 && tmp[j+1] == "music") {
                    tmp[j] = "art music";
                    locker = 1;
                }
                else if (tmp[j] == "double") {
                    tmp[j] = "double bass";
                    locker = 1;
                }
                else if (tmp[j] == "fruits") {
                    tmp[j] = "fruits vegetables";
                    locker = 1;
                }
                else if (tmp[j] == "centralprocessing") {
                    tmp[j] = "centralprocessing unit";
                    locker = 1;
                }
                if (tmp[j] != "")
                {
                    if (!words_of_G_in_sheet.ContainsKey(tmp[j]))
                        words_of_G_in_sheet.Add(tmp[j], 1);
                    else
                        words_of_G_in_sheet[tmp[j]]++;
                }
                if (locker==0)
                    j++;
                else if (locker==1)
                    j+=2;
                else
                    j+=3;
            }
        }
        return words_of_G_in_sheet;
    }
    public static Dictionary<string, int> Helper_In_Common_Words_3_8(string[] sheet, Dictionary<string, int> words_of_G_in_sheet, int start_index, string G)
    {
        int i,j,locker; string[] tmp;
        Dictionary<string, int> in_common_words = new Dictionary<string, int>();
        for (i = start_index; i < start_index+11; i++) {
            tmp = sheet[i].Split();
            j = 0;
            while (j<tmp.Length)
            {
                locker = 0; // aniaml, computer, food, Music, 
                if (G=="animal" || G=="computer" || G=="food" || G=="Music")
                {
                    if (tmp[j] == "centralprocessing"){
                        tmp[j] = "centralprocessing unit";
                        locker = 1;
                    }
                    else if (tmp[j] == "fruits") {
                        tmp[j] = "fruits vegetables";
                        locker = 1;
                    }
                    else if (tmp[j] == "double") {
                        tmp[j] = "double bass";
                        locker=1;
                    }
                    else if (tmp[j] == "popular") {
                        tmp[j] = "popular music";
                        locker=1;
                    }
                    else if (tmp[j] == "von") {
                        tmp[j] = "von neumann machine";
                        locker=3;
                    }
                    else if (tmp[j] == "animal" && j<tmp.Length-1 && tmp[j+1] == "welfare") {
                        tmp[j] = "animal welfare";
                        locker=1;
                    }
                    else if (tmp[j] == "art" && j<tmp.Length-1 && tmp[j+1] == "music") {
                        tmp[j] = "art music";
                        locker=1;
                    }
                }
                
                if (words_of_G_in_sheet.ContainsKey(tmp[j]) && tmp[j] != "")
                {
                    if (!in_common_words.ContainsKey(tmp[j]))
                        in_common_words.Add(tmp[j], 1);
                    else
                        in_common_words[tmp[j]]++;
                }
                if (locker==0)
                    j++;
                else if (locker==1)
                    j+=2;
                else
                    j+=3;
            }
        }
        return in_common_words;
    }
    public static Dictionary<Tuple<string, string>, int> Helper_All_Words_Sheet_4(string[] sheet, Dictionary<string, int> genres_of_sheet)
    {
        // all words of all genres with genre with count;
        Dictionary<Tuple<string, string>, int> words_of_sheet = new Dictionary<Tuple<string, string>, int>();
        List<string> keys = new List<string>(); // genres in list;
        foreach (var key in genres_of_sheet.Keys)
            keys.Add(key);
        int i,j,locker,k = -1; string[] tmp; Tuple<string, string> tuple;
        for (i = 0; i < sheet.Length; i++) {
            if (i%11==0)
                k++;
            tmp = sheet[i].Split(); j = 0;
            while (j < tmp.Length)
            {
                locker=0;
                if (tmp[j] == "animal" && j<tmp.Length-1 && tmp[j+1] == "welfare") {
                    tmp[j] = "animal welfare";
                    locker = 1;
                }
                else if (tmp[j] == "von") {
                    tmp[j] = "von neumann machine";
                    locker = 3;
                }
                else if (tmp[j] == "popular") {
                    tmp[j] = "popular music";
                    locker = 1;
                }
                else if (tmp[j] == "art" && j<tmp.Length-1 && tmp[j+1] == "music") {
                    tmp[j] = "art music";
                    locker = 1;
                }
                else if (tmp[j] == "double") {
                    tmp[j] = "double bass";
                    locker = 1;
                }
                else if (tmp[j] == "fruits") {
                    tmp[j] = "fruits vegetables";
                    locker = 1;
                }
                else if (tmp[j] == "centralprocessing") {
                    tmp[j] = "centralprocessing unit";
                    locker = 1;
                }
                tuple = Tuple.Create(tmp[j], keys[k]);
                if (tmp[j] != "")
                {
                    if (!words_of_sheet.ContainsKey(tuple))
                        words_of_sheet.Add(tuple, 1);
                    else
                        words_of_sheet[tuple]++;
                }
                
                if (locker==0)
                    j++;
                else if (locker==1)
                    j+=2;
                else
                    j+=3;
            }
        }
        return words_of_sheet;
    }
    public static Dictionary<string, int> Helper_In_Common_Words_4_5(string[] sheet, Dictionary<Tuple<string, string>, int> words_of_sheet, int start_index, string genre)
    {
        int i,j,locker; string[] tmp;
        Dictionary<string, int> in_common_words = new Dictionary<string, int>();
        for (i = start_index; i < start_index+11; i++)
        {
            tmp = sheet[i].Split(); j = 0; // check equality of words of sheet with another sheet;
            while (j<tmp.Length)
            {
                locker = 0;
                if (tmp[j] == "centralprocessing"){
                    tmp[j] = "centralprocessing unit";
                    locker = 1;
                }
                else if (tmp[j] == "fruits") {
                    tmp[j] = "fruits vegetables";
                    locker = 1;
                }
                else if (tmp[j] == "double") {
                    tmp[j] = "double bass";
                    locker=1;
                }
                else if (tmp[j] == "popular") {
                    tmp[j] = "popular music";
                    locker=1;
                }
                else if (tmp[j] == "von") {
                    tmp[j] = "von neumann machine";
                    locker=3;
                }
                else if (tmp[j] == "animal" && j<tmp.Length-1 && tmp[j+1] == "welfare") {
                    tmp[j] = "animal welfare";
                    locker=1;
                }
                else if (tmp[j] == "art" && j<tmp.Length-1 && tmp[j+1] == "music") {
                    tmp[j] = "art music";
                    locker=1;
                }
                if (words_of_sheet.ContainsKey(Tuple.Create(tmp[j], genre)) && tmp[j] != "")
                {
                    if (!in_common_words.ContainsKey(tmp[j]))
                        in_common_words.Add(tmp[j], 1);
                    else
                        in_common_words[tmp[j]]++;
                }
                if (locker==0)
                    j++;
                else if (locker==1)
                    j+=2;
                else
                    j+=3;
            }
        }
        return in_common_words;
    }
    public static Tuple<int, List<int>> Helper_Simple_Neighbor_6(string path, string G, int N, bool[] Nodes, int start = 0, int counter=500)
    {
        string[] sheet = ReadAllLines(@$"..\DataSet\Text_{path}.txt");
        Dictionary<string, int> dict_of_genres = Helper_Genres_of_Sheet(sheet);
        if (!dict_of_genres.ContainsKey(G))
            return Tuple.Create(0, new List<int>());
        List<int> indices = new List<int>();
        string[] tmp_sheet;
        int sum, i;

        Dictionary<string, int> in_common_words = new Dictionary<string, int>();
        Dictionary<string, int> words_of_G_in_sheet = Helper_Words_G_in_Sheet_3_8(sheet, G, dict_of_genres[G]);

        for (i = start; i < start+counter; i++)
        {
            if (path != $"{i}" && !Nodes[i-start])
            {
                tmp_sheet = ReadAllLines(@$"..\DataSet\Text_{i}.txt");
                dict_of_genres = Helper_Genres_of_Sheet(tmp_sheet);
                if (dict_of_genres.ContainsKey(G))
                {
                    in_common_words = Helper_In_Common_Words_3_8(tmp_sheet, words_of_G_in_sheet, dict_of_genres[G], G);
                    sum = 0;
                    foreach (string key in in_common_words.Keys)
                        sum += Min(words_of_G_in_sheet[key], in_common_words[key]);
                    if (sum==N)
                        indices.Add(i);
                }
            }
        }
        return Tuple.Create(indices.Count, indices);
    }
    private static bool Helper_Is_Neighbor_8(int path1, int path2, string genre, int N, int start_index_path1, int start_index_path2)
    {
        string[] sheet1 = ReadAllLines(@$"..\DataSet\Text_{path1}.txt");
        string[] sheet2 = ReadAllLines(@$"..\DataSet\Text_{path2}.txt");
        Dictionary<string, int> words1 = Helper_Words_G_in_Sheet_3_8(sheet1, genre, start_index_path1);
        Dictionary<string, int> in_common_words = Helper_In_Common_Words_3_8(sheet2, words1, start_index_path2, genre);
        return in_common_words.Values.Sum()==N;
    }







    public static int Find_Word_part_1(string word, int repeat)
    {
        int sheet_count = 0;
        for (int i = 0; i < 100_000; i++)  
            if (Helper_Equality_Counter(ReadAllLines(@$"..\DataSet\Text_{i}.txt"), word)>=repeat)
                sheet_count++;
        return sheet_count;
    }
    public static int Main_Genre_part_2(string genre)
    {
        int count = 0;
        for (int i = 0; i < 100_000; i++)
        {
            var all_lines = ReadAllLines(@$"..\DataSet\Text_{i}.txt");
            string[] tmp = all_lines[50].Split();
            if      (!Helper_Repeated_Bad_Words2.ContainsKey(tmp[0])) {
                if (Helper_Genre_Of_Word(tmp[0]) == genre)
                    count++; 
                continue;
            }
            else if (!Helper_Repeated_Bad_Words2.ContainsKey(tmp[1])) {
                if (Helper_Genre_Of_Word(tmp[1]) == genre)
                    count++;
                continue;
            }
            else if (!Helper_Repeated_Bad_Words2.ContainsKey(tmp[2])) {
                if (Helper_Genre_Of_Word(tmp[2]) == genre)
                    count++;
                continue;
            }
            else if (!Helper_Repeated_Bad_Words2.ContainsKey(tmp[3])) {
                if (Helper_Genre_Of_Word(tmp[3]) == genre)
                    count++;
                continue;
            }
            else if (!Helper_Repeated_Bad_Words2.ContainsKey(tmp[4])) {
                if (Helper_Genre_Of_Word(tmp[4]) == genre)
                    count++;
                continue;
            }
        }
        return count;
    }
    public static Tuple<int, List<int>> Simple_Neighbor_part_3(string path, string G, int N, int start = 0, int counter = 10_000)
    {
        string[] sheet = ReadAllLines(@$"..\DataSet\Text_{path}.txt");
        Dictionary<string, int> dict_of_genres = Helper_Genres_of_Sheet(sheet);
        if (!dict_of_genres.ContainsKey(G))
            return Tuple.Create(0, new List<int>());
        List<int> indices = new List<int>();
        string[] tmp_sheet;
        int sum,i;

        Dictionary<string, int> in_common_words = new Dictionary<string, int>();
        Dictionary<string, int> words_of_G_in_sheet = Helper_Words_G_in_Sheet_3_8(sheet, G, dict_of_genres[G]);

        for (i = start; i < start+counter; i++)
        {
            if (path != $"{i}")
            {
                tmp_sheet = ReadAllLines(@$"..\DataSet\Text_{i}.txt");
                dict_of_genres = Helper_Genres_of_Sheet(tmp_sheet);
                if (dict_of_genres.ContainsKey(G))
                {
                    in_common_words = Helper_In_Common_Words_3_8(tmp_sheet, words_of_G_in_sheet, dict_of_genres[G], G);
                    sum = 0;
                    foreach (string key in in_common_words.Keys)
                        sum += Min(words_of_G_in_sheet[key], in_common_words[key]);
                    if (sum==N)
                        indices.Add(i);
                }
            }
        }
        return Tuple.Create(indices.Count, indices);
    }
    public static Tuple<int, List<int>> Neighbor_part_4(string path, int N)
    {
        string[] sheet = ReadAllLines(@$"..\DataSet\Text_{path}.txt");
        int i,sum=0; string[] tmp_sheet; // variables; // dicts;
        Dictionary<string, int> genres_of_tmp_sheet, in_common_words, genres_of_sheet = Helper_Genres_of_Sheet(sheet);
        Dictionary<Tuple<string, string>, int> words_of_sheet = Helper_All_Words_Sheet_4(sheet, genres_of_sheet);
        List<int> Neighbors = new List<int>();
        for (i = 0; i < 5_000; i++)
        {
            if (path != $"{i}")
            {
                tmp_sheet = ReadAllLines(@$"..\DataSet\Text_{i}.txt");
                genres_of_tmp_sheet = Helper_Genres_of_Sheet(tmp_sheet);
                foreach (string genre in genres_of_tmp_sheet.Keys)
                {
                    if (genres_of_sheet.ContainsKey(genre))
                    {
                        in_common_words = Helper_In_Common_Words_4_5(tmp_sheet, words_of_sheet, genres_of_tmp_sheet[genre], genre);
                        sum = 0;
                        foreach (string word in in_common_words.Keys)
                            sum += Min(words_of_sheet[Tuple.Create(word, genre)], in_common_words[word]);
                        if (sum==N)
                        {   
                            Neighbors.Add(i);
                            break;
                        }
                    }
                }
            }
        }
        return Tuple.Create(Neighbors.Count, Neighbors);
    }
    public static Tuple<int, List<int>> Full_Neighbor_part_5(string path, int N, int M)
    {
        string[] sheet = ReadAllLines(@$"..\DataSet\Text_{path}.txt");
        int i,M_counter,sum=0; string[] tmp_sheet; // variables; // dicts;
        Dictionary<string, int> genres_of_tmp_sheet, in_common_words, genres_of_sheet = Helper_Genres_of_Sheet(sheet);
        Dictionary<Tuple<string, string>, int> words_of_sheet = Helper_All_Words_Sheet_4(sheet, genres_of_sheet);
        List<int> Neighbors = new List<int>();
        for (i = 0; i < 1000; i++)
        {
            if (path != $"{i}")
            {
                tmp_sheet = ReadAllLines(@$"..\DataSet\Text_{i}.txt");
                genres_of_tmp_sheet = Helper_Genres_of_Sheet(tmp_sheet);
                M_counter = 0;
                foreach (string genre in genres_of_tmp_sheet.Keys)
                {
                    if (genres_of_sheet.ContainsKey(genre))
                    {
                        in_common_words = Helper_In_Common_Words_4_5(tmp_sheet, words_of_sheet, genres_of_tmp_sheet[genre], genre);
                        sum = 0;
                        foreach (string word in in_common_words.Keys)
                            sum += Min(words_of_sheet[Tuple.Create(word, genre)], in_common_words[word]);
                        if (sum==N)
                            M_counter++;
                    }
                }
                if (M_counter == M)
                    Neighbors.Add(i);
            }
        }
        return Tuple.Create(Neighbors.Count, Neighbors);
    }
    public static Tuple<int, Dictionary<int, List<int>>> Book_part_6(string Genre, int N, int start, int counter = 500)
    {
        Dictionary<int, List<int>> number_of_pages = new Dictionary<int, List<int>>();
        int i, j, k, book_counter = 0; 
        List<int> sheets_of_book, queue;
        bool[] Nodes = new bool[counter];
        for (i = start; i < start+counter; i++)
        {
            if (!Nodes[i-start]) {
                queue = new List<int>(); queue.Add(i);
                for (j = 0; j < queue.Count; j++) {    // exploring queue[j];
                    sheets_of_book = Helper_Simple_Neighbor_6($"{queue[j]}", Genre, N, Nodes, start, counter).Item2;
                    Nodes[queue[j] - start] = true;
                    for (k = 0; k < sheets_of_book.Count; k++)
                        if (!Nodes[sheets_of_book[k]- start])
                            queue.Add(sheets_of_book[k]);
                }
                if (queue.Count!=1) {
                    book_counter++;
                    number_of_pages.Add(book_counter, queue);
                }
            }
        }
        return Tuple.Create(number_of_pages.Count, number_of_pages);
    }
    public static Tuple<int, Dictionary<int, List<int>>> Book_With_Word_part_7(string Genre, int N_, int N, string w, int start)
    {
        Dictionary<int, List<int>> number_of_pages = Book_part_6(Genre, N_, start).Item2;
        int sum;
        foreach (var key in number_of_pages.Keys)
        {
            sum = 0;
            foreach (var path in number_of_pages[key])
                sum += Helper_Equality_Counter(ReadAllLines(@$"..\DataSet\Text_{path}.txt"), w);
            if (sum < N)
                number_of_pages.Remove(key);
        }
        return Tuple.Create(number_of_pages.Count, number_of_pages);
    }
    public static Tuple<int, List<int>> Complete_Book_part_8(string genre, int N, int start)
    {
        int i,j,k; bool[] Nodes;
        List<int> queue; List<List<int>> queues = new List<List<int>>();
        Dictionary<int, List<int>> Books = Book_part_6(genre, N, 4500, 100).Item2;
        List<Dictionary<string, int>> genres;
        foreach (int key in Books.Keys)
        {
            genres = new List<Dictionary<string, int>>();
            for (i = 0; i < Books[key].Count; i++)
                genres.Add(Helper_Genres_of_Sheet(ReadAllLines(@$"..\DataSet\Text_{Books[key][i]}.txt")));
            Nodes = new bool[100];
            for (i = 0; i < Books[key].Count; i++) {
                if (!Nodes[Books[key][i]-start])
                    {
                    queue = new List<int>();
                    queue.Add(Books[key][i]);
                    Nodes[Books[key][i]-start] = true;
                    for (j = 0; j < queue.Count; j++)
                    {
                        if (queue[j] != Books[key][i])
                        for (k = 0; k < Books[key].Count; k++)
                        {
                            if (!Nodes[Books[key][k]-start] && Books[key][k] != queue[j])    
                                if (Helper_Is_Neighbor_8(queue[j], Books[key][k], genre, N, genres[j][genre], genres[k][genre]))
                                {
                                    queue.Add(Books[key][j]);
                                    Nodes[Books[key][j]-start] = true;
                                }   
                        }
                    }
                    if (queue.Count>1)
                    queues.Add(queue);
                }
            }
        }
        int max = 0;
        int index = -1;
        for (i = 0; i < queues.Count; i++){
            if (queues[i].Count > max)
            {
                max = queues[i].Count;
                index = i;
            }
        }
        return queues.Count > 0 
        ? Tuple.Create(max, queues[index]): Tuple.Create(0, new List<int>());
    }
    public static void Main()
    {
        string[] tmp_file; 
        string path, genre, G, input_word, tmp; 
        int N_, N, M, start;
        foreach (var item in Helper_Repeated_Bad_Words)
            Helper_Repeated_Bad_Words2.Add(item, item);
        foreach (string file in Directory.GetFiles(@$"..\Genres")) {
            tmp = file.Remove(0, 10);
            tmp = tmp.Remove(tmp.Length-4, 4);
            if (tmp != "BinaryNumber" && tmp != "PrimeNumber" && tmp != "CubeNumber" &&
                tmp != "SquareNumber" && tmp != "time" && tmp != "temperature")
            {
                tmp_file = ReadAllLines(file);
                All_Genres.Add(tmp, tmp_file);
                foreach (string word in tmp_file) {
                    if (All_Words.ContainsKey(word))
                        All_Words[word] += $"_{tmp}";
                    else
                        All_Words.Add(word, tmp);
                }
            }
        }

        Stopwatch time;
        var memory = 0.0;
        Process proc;
        while (true)
        {
            Write("choose a number between 1 and 8: ");
            string starter = ReadLine();
            if (starter == "0")
                break;
            switch (starter)
            {
                case "9":
                WriteLine(Helper_Genre_Of_Word(ReadLine()));
                break;

                case "1":
                Write("word: ");
                input_word = ReadLine();
                Write("repeat: ");
                int repeat = int.Parse(ReadLine());
                time = Stopwatch.StartNew(); proc = Process.GetCurrentProcess();
                WriteLine(Find_Word_part_1(input_word, repeat));
                time.Stop(); WriteLine(time.ElapsedMilliseconds*0.001 + " S");
                memory = proc.PrivateMemorySize64/(1024*1024);
                proc.Dispose();
                WriteLine(memory + " MB");
                break;

                case "2":
                Write("genre: ");
                genre = ReadLine();
                time = Stopwatch.StartNew(); proc = Process.GetCurrentProcess();
                WriteLine(Main_Genre_part_2(genre));
                time.Stop(); WriteLine(time.ElapsedMilliseconds*0.001 + " S");
                memory = proc.PrivateMemorySize64/(1024*1024);
                proc.Dispose();
                WriteLine(memory + " MB");
                break;

                case "3":
                Write("path: ");
                path = ReadLine();
                Write("genre: ");
                G = ReadLine();
                Write("N: ");
                N = int.Parse(ReadLine());
                time = Stopwatch.StartNew(); proc = Process.GetCurrentProcess();
                var num = Simple_Neighbor_part_3(path, G, N);
                time.Stop(); WriteLine(time.ElapsedMilliseconds*0.001 + " S");
                memory = proc.PrivateMemorySize64/(1024*1024);
                proc.Dispose();
                WriteLine(memory + " MB");
                WriteLine("number of simple neighbors: " + num.Item1);
                WriteLine("sheet numbers: ");
                foreach (var item in num.Item2)
                    Write(item + "  ");
                WriteLine();
                break;

                case "4":
                Write("path: ");
                path = ReadLine();          // 6
                Write("N: ");
                N = int.Parse(ReadLine());  // 10    ---->   85
                time = Stopwatch.StartNew(); proc = Process.GetCurrentProcess();
                num = Neighbor_part_4(path, N);
                time.Stop(); WriteLine(time.ElapsedMilliseconds*0.001 + " S");
                memory = proc.PrivateMemorySize64/(1024*1024);
                proc.Dispose();
                WriteLine(memory + " MB");
                WriteLine("number of neighbors: " + num.Item1);
                WriteLine("sheet numbers: ");
                foreach (var item in num.Item2)
                    Write(item + "  ");
                WriteLine();
                break;

                case "5":
                Write("path: ");
                path = ReadLine();          // 6
                Write("N: ");
                N = int.Parse(ReadLine());  // 10
                Write("M: ");
                M = int.Parse(ReadLine());  // 1
                time = Stopwatch.StartNew(); proc = Process.GetCurrentProcess();
                num = Full_Neighbor_part_5(path, N, M);
                time.Stop(); WriteLine(time.ElapsedMilliseconds*0.001 + " S");
                memory = proc.PrivateMemorySize64/(1024*1024);
                proc.Dispose();
                WriteLine(memory + " MB");
                WriteLine("number of full neighbors: " + num.Item1);
                WriteLine("sheet numbers: ");
                foreach (var item in num.Item2)
                    Write(item + "  ");
                WriteLine();
                break;

                case "6":
                Write("genre: ");
                genre = ReadLine();
                Write("N: ");
                N = int.Parse(ReadLine());
                Write("start page? ");
                start = int.Parse(ReadLine());
                time = Stopwatch.StartNew(); proc = Process.GetCurrentProcess();
                var mine = Book_part_6(genre, N, start);
                time.Stop(); WriteLine(time.ElapsedMilliseconds*0.001 + " S");
                memory = proc.PrivateMemorySize64/(1024*1024);
                proc.Dispose();
                WriteLine(memory + " MB");
                WriteLine("number of books : " + mine.Item1);
                foreach (var key in mine.Item2.Keys)
                {
                    Write($"sheets of book {key}:  ");
                    foreach (var item in mine.Item2[key])
                    {
                        Write(item+ "  ");
                    }
                    WriteLine();
                }
                break;

                case "7":
                Write("genre: ");
                genre = ReadLine();
                Write("N joint: ");
                N_ = int.Parse(ReadLine());
                Write("N repeated word: ");
                N = int.Parse(ReadLine());
                Write("word: ");
                input_word = ReadLine();
                Write("start page? ");
                start = int.Parse(ReadLine());
                time = Stopwatch.StartNew(); proc = Process.GetCurrentProcess();
                mine = Book_With_Word_part_7(genre, N_, N, input_word, start);
                time.Stop(); WriteLine(time.ElapsedMilliseconds*0.001 + " S");
                memory = proc.PrivateMemorySize64/(1024*1024);
                proc.Dispose();
                WriteLine(memory + " MB");
                WriteLine("number of books : " + mine.Item1);
                foreach (var key in mine.Item2.Keys)
                {
                    Write($"sheets of book {key}:  ");
                    foreach (var item in mine.Item2[key])
                    {
                        Write(item+ "  ");
                    }
                    WriteLine();
                }
                break;

                case "8":
                Write("genre: ");
                genre = ReadLine();
                Write("N: ");
                N = int.Parse(ReadLine());
                Write("start page? ");
                start = int.Parse(ReadLine());
                time = Stopwatch.StartNew(); proc = Process.GetCurrentProcess();
                num = Complete_Book_part_8(genre, N, start);
                time.Stop(); WriteLine(time.ElapsedMilliseconds*0.001 + " S");
                memory = proc.PrivateMemorySize64/(1024*1024);
                proc.Dispose();
                WriteLine(memory + " MB");
                WriteLine("number of books : " + num.Item1);
                foreach (var item in num.Item2)
                {
                    Write(item+"  ");
                }
                break;
            }
        }
    }
}