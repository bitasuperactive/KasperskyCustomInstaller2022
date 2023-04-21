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
        private readonly Func<IProgress<double>, CancellationToken, object> _taskFunction;
        private CancellationTokenSource _cancellationTokenSource;

        // Constructor que permite especificar el método que se ejecutará en la tarea.
        public TaskHandler(Action<IProgress<double>, CancellationToken> taskAction)
        {
            _taskAction = taskAction;
        }

        public TaskHandler(Func<IProgress<double>, CancellationToken, object> taskFunction)
        {
            _taskFunction = taskFunction;
        }

        // Función para iniciar la tarea.
        public Task<object> Run()
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
                    _taskAction.Invoke(progress, _cancellationTokenSource.Token);
                else
                    tResult = _taskFunction.Invoke(progress, _cancellationTokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                IsRunning = false;
                TaskCancelled?.Invoke(this, EventArgs.Empty);
                return Task.FromResult<object>(null);
            }
            finally
            {
                _cancellationTokenSource.Dispose();
            }

            // Indicar que la tarea se ha completado con éxito.
            Thread.Sleep(100); // Permite que el evento progress_changed reporte el resultado final.
            IsRunning = false;
            TaskCompleted?.Invoke(this, EventArgs.Empty);
            return Task.FromResult(tResult);
        }

        // Función para cancelar la tarea.
        public void Cancel()
        {
            if (this.IsRunning)
                _cancellationTokenSource.Cancel();
        }
    }
}
