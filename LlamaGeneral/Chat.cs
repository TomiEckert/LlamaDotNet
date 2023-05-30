using LLama;

namespace LlamaGeneral
{
    public class Chat
    {
        ChatSession<LLamaModel> session;

        public Chat(LLamaModel model, string prompt)
        {
            OnTokenReceived += (_) => { };
            session = new ChatSession<LLamaModel>(model);
            session.WithPrompt(prompt)
            .WithAntiprompt(new string[] { "USER:", "User:", "user:" });
        }

        public void SendMessage(string text)
        {
            var response = session.Chat(text + "\nASSISTANT:");
            foreach (var item in response)
            {
                OnTokenReceived(item);
            }
        }

        public event Action<string> OnTokenReceived;
    }
}