using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ASNC.Services
{
    internal class ImageQueueService
    {
        private readonly ServiceProvider serviceProvider;
        private readonly List<string> workingList = new List<string>();
        private object listLock = new object();

        public ImageQueueService(ServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        internal async Task TryGetImage(string statueId, bool downloadIfMissing, Action<string?> onComplete)
        {
            var path = this.serviceProvider.LibAmiibo.GetApiService?.GetAmiiboImagePath(statueId);
            if (File.Exists(path))
            {
                onComplete(path);
            }
            else
            {
                if (downloadIfMissing)
                {
                    var retry = true;
                    lock (this.listLock)
                    {
                        if (!this.workingList.Contains(statueId))
                        {
                            this.workingList.Add(statueId);
                            retry = false;
                        }
                    }

                    if (!retry)
                    {
                        path = await this.serviceProvider.LibAmiibo.GetLocalAmiiboImage(statueId, true);
                        onComplete(path);
                        return;
                    }
                    else
                    {
                        const int maxTries = 10;
                        const int sleepTimeInMS = 1500; // 1,5 sec || try * sleepTime (4 * 1500 = 6000)
                        int tryCount = 0;
                        while (tryCount < maxTries)
                        {
                            await Task.Delay(tryCount * sleepTimeInMS);
                            tryCount++;

                            try
                            {
                                path = this.serviceProvider.LibAmiibo.GetApiService?.GetAmiiboImagePath(statueId);
                                if (File.Exists(path))
                                {
                                    onComplete(path);
                                    return;
                                }
                            }
                            catch (Exception ex)
                            {
                                await Console.Out.WriteLineAsync(ex.Message);
                            }
                        }

                        onComplete(null);
                        return;
                    }
                }
            }
        }
    }
}
