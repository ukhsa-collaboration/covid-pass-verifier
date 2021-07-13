using System;
using System.Threading.Tasks;
using NHSCovidPassVerifier.Services.ErrorHandlers;

namespace NHSCovidPassVerifier.Services.Utils
{
    public static class TaskUtils
    {
        public static async void FireAndForgetSafeAsync(this Task task, IAppErrorHandler handler)
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                handler?.HandleError(ex);
            }
        }
    }
}