﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Lykke.Service.OperationsRepository.Client.CashOperations;

namespace Lykke.Service.OperationsRepository.Client
{
    public class ActionMeasureResult<T>
    {
        public long ElapsedTime { get; set; }
        public T ActionResult { get; set; }
    }

    public static class RepositoryClientExtensions
    {
        public static async Task<ActionMeasureResult<T>> MeasureTime<T>(this BaseRepositoryClient client, Func<Task<T>> action)
        {
            T actionResult;

            var watch = Stopwatch.StartNew();
            try
            {
                actionResult = await action();
            }
            finally
            {
                watch.Stop();
            }

            return new ActionMeasureResult<T>
            {
                ElapsedTime = watch.ElapsedMilliseconds,
                ActionResult = actionResult
            };
        }
    }
}
