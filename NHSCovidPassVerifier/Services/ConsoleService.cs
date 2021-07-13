using NHSCovidPassVerifier.Services.Interfaces;

namespace NHSCovidPassVerifier.Services
{
    public class ConsoleService : IConsoleService
    {
        public void PrintToDebugConsole(string s)
        {
            System.Diagnostics.Debug.Print(s);
        }
    }
}
