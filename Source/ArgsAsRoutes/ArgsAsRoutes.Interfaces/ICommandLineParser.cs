namespace ArgsAsRoutes.Interfaces;

public delegate void ExecuteOnDelegate(Dictionary<string, string> parsedArgs);

public interface ICommandLineParser
{
    void On(string code, ExecuteOnDelegate executeOn);
    void Run();
}