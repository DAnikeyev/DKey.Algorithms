namespace DKey.ContestProcedure;

public static class Program
{
    public static async Task Main()
    {
        var submissionBuilderTask = Task.Run(CodeForcesSubmissionBuilder.Program.Main);
        CodeForces.Program.Main();
        await submissionBuilderTask;
    }
}