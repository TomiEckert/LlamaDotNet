namespace LlamaGeneral;

public class Models
{
    public static LLama.LLamaModel Samantha7B =>
        new LLama.LLamaModel(
            new LLama.LLamaParams(
                model: "D:\\AI\\Models\\Samantha-7B.ggmlv3.q5_1.bin",
                n_gpu_layers: 50,
                interactive: true,
                n_ctx: 2048,
                n_batch: 2048
            )
        );
    public static LLama.LLamaModel Samantha13B =>
        new LLama.LLamaModel(
            new LLama.LLamaParams(
                model: "D:\\AI\\Models\\samantha-13b.ggmlv3.q5_1.bin",
                n_gpu_layers: 50,
                interactive: true,
                n_ctx: 2048,
                n_batch: 2048
            )
        );
}
