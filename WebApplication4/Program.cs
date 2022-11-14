using Newtonsoft.Json;

string path_json_setting = "setting_config.txt";

string? apprun = new FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).DirectoryName;
string directory_data = Path.Combine(apprun ?? "", "Data");
string path_json_players = "";
Console.WriteLine(directory_data);




if (!Directory.Exists(directory_data))
{
    Directory.CreateDirectory("Data");
    File.Create(Path.Combine("Data", path_json_setting));
}


while (true)
{
    try
    {
        if (Directory.Exists("Data"))
        {
            string str_ = Path.Combine("Data", path_json_setting);
            if (File.Exists(str_))
            {
                string ddd = File.ReadAllText(str_).Trim().Replace("\"", "");
                if (ddd == string.Empty || !File.Exists(ddd))
                {
                    Console.WriteLine("А тут пусто!: " + ddd);

                    break;
                }
                   
                path_json_players = new FileInfo(ddd).FullName;
                Console.WriteLine("Файл " + path_json_players + " принят");
                break;
            }
            else
            {
                Console.WriteLine("Файла не существует! " + str_);
            }
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        
    }
    Thread.Sleep(1000);
}

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapGet("/", () => "Сервер включен");
app.MapGet("/users", () =>
{
    if (File.Exists(path_json_players))
    {
        Console.WriteLine("send->" + path_json_players);
        return File.ReadAllText(path_json_players);

    }
    else
    {
       
        Console.WriteLine("Я не могу читать!");
    }
    return "";

});
app.Run();







