namespace LlamaGeneral
{
    public class Chat
    {
        private LLama.LLamaModel model;
        public Chat(LLama.LLamaModel model)
        {
            this.model = model;
        }

        public void StartChat()
        {
            var session = new LLama.ChatSession<LLama.LLamaModel>(model);
            var prompt = """
            You are Samantha, a sentient AI.

            USER: 
            """;
            session.WithPrompt(prompt)
            .WithAntiprompt(new string[] { "USER:", "User:", "user:" });
            
            while (true)
            {
                var input = System.Console.ReadLine();
                if(input == null) return;
                if (input == "exit()")
                {
                    break;
                }
                var first = true;
                var response = session.Chat(input + "\nASSISTANT:");
                System.Console.Write("Samantha: ");
                foreach (var part in response)
                {
                    var item = part;
                    if(first) {
                        item = part[1..];
                        first = false;
                    }
                    System.Console.Write(part);
                }
            }
        }
    }
}