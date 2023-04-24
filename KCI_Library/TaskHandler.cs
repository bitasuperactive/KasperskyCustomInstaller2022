using Google.Protobuf.WellKnownTypes;
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
    public class TaskHandler
    {
        public EventHandler<double> ProgressChanged;
        public EventHandler TaskStarted;
        public EventHandler TaskCancelled;
        public EventHandler TaskCompleted;
        public bool IsRunning { get; private set; }

        private readonly Action<IProgress<double>, CancellationToken> _taskAction;
        private readonly Func<IProgress<double>, CancellationToken, Task<object>> _taskFunction;
        private CancellationTokenSource _cancellationTokenSource;

        // Constructor que permite especificar el método que se ejecutará en la tarea.
        public TaskHandler(Action<IProgress<double>, CancellationToken> taskAction)
        {
            _taskAction = taskAction;
        }

        public TaskHandler(Func<IProgress<double>, CancellationToken, Task<object>> taskFunction)
        {
            _taskFunction = taskFunction;
        }

        // Función para iniciar la tarea.
        public async Task RunAsync()
        {
            // Indicar que la tarea se ha iniciado.
            IsRunning = true;
            TaskStarted?.Invoke(this, EventArgs.Empty);

            // Crear el token de cancelación.
            _cancellationTokenSource = new();

            // Crear el objeto que informará sobre el progreso de la tarea.
            Progress<double> progress = new(progressValue => ProgressChanged?.Invoke(this, progressValue));

            object tResult = null;

            // Ejecutar la tarea.
            try
            {
                if (_taskAction != null)
                    await Task.Run(() => _taskAction.Invoke(progress, _cancellationTokenSource.Token));
                else
                    tResult = await _taskFunction.Invoke(progress, _cancellationTokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                IsRunning = false;
                TaskCancelled?.Invoke(this, EventArgs.Empty);
                return;
            }
            finally
            {
                IsRunning = false;
                _cancellationTokenSource.Dispose();
            }

            // Indicar que la tarea se ha completado con éxito.
            TaskCompleted?.Invoke(this, EventArgs.Empty);
            Debug.WriteLine("COMPLETED");

            await Task.FromResult(tResult);
        }

        // Función para cancelar la tarea.
        public void Cancel()
        {
            if (this.IsRunning && !this._cancellationTokenSource.Token.IsCancellationRequested)
                _cancellationTokenSource.Cancel();
        }
    }
}
