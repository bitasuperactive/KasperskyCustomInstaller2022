using Google.Protobuf.WellKnownTypes;
using KCI_Library.Models;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KCI_Library
{
    public class TaskHandler<TProgress>
    {
        public event EventHandler<TProgress> ProgressChanged;
        public event EventHandler TaskStarted;
        public event EventHandler TaskCancelled;
        public event EventHandler TaskCompleted;
        public bool IsRunning { get; private set; }
        private CancellationTokenSource _cancellationTokenSource;

        public TaskHandler()
        {

        }

        // Action.
        public void Run(Action<IProgress<TProgress>, CancellationToken> method)
        {
            _cancellationTokenSource = new();
            Progress<TProgress> progress = new(progressValue => ProgressChanged?.Invoke(this, progressValue));

            // Tarea iniciada.
            IsRunning = true;
            TaskStarted?.Invoke(this, EventArgs.Empty);

            try
            {
                method.Invoke(progress, _cancellationTokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                // Tarea cancelada.
                TaskCancelled?.Invoke(this, EventArgs.Empty);
                return;
            }
            finally
            {
                IsRunning = false;
                _cancellationTokenSource.Dispose();
            }

            // Tarea completada con éxito.
            TaskCompleted?.Invoke(this, EventArgs.Empty);
        }

        // Function.
        public TResult? Run<T1, TResult>(Func<IProgress<TProgress>, CancellationToken, T1, TResult> method, T1 property)
        {
            _cancellationTokenSource = new();
            Progress<TProgress> progress = new(progressValue => ProgressChanged?.Invoke(this, progressValue));
            TResult? result = default;

            // Tarea iniciada.
            IsRunning = true;
            TaskStarted?.Invoke(this, EventArgs.Empty);

            try
            {
                result = method.Invoke(progress, _cancellationTokenSource.Token, property);
            }
            catch (OperationCanceledException)
            {
                // Tarea cancelada.
                TaskCancelled?.Invoke(this, EventArgs.Empty);
                return default;
            }
            finally
            {
                IsRunning = false;
                _cancellationTokenSource.Dispose();
            }

            // Tarea completada con éxito.
            TaskCompleted?.Invoke(this, EventArgs.Empty);

            return result;
        }

        // Cancelar la tarea.
        public void Cancel()
        {
            if (this.IsRunning && !this._cancellationTokenSource.Token.IsCancellationRequested)
                _cancellationTokenSource.Cancel();
        }
    }
}
