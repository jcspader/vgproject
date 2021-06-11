namespace VG.Domain.Shared
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class ProcessResult
    {
        public ProcessResult(string message)
        {
            this.message = message;
        }

        public string message { get; private set; }
    }
}
