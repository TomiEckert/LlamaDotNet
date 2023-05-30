using LlamaGeneral;

var model = Models.GetModel("TheBloke/Samantha-7B-GGML", "Samantha-7B.ggmlv3.q5_1.bin", "D:\\AI\\Models");
var chat = new Chat(model, Prompts.Samantha);


while (true)
{
    var input = Console.ReadLine();
    chat.SendMessage(input + "\nASSISTANT:");
}

Console.ReadKey();
void Chat_OnMessageReceived(IEnumerable<string> obj)
{
    foreach (var item in obj)
    {
        Console.Write(item);
    }
}