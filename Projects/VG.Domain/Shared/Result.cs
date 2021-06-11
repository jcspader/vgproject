using System;

namespace VG.Domain.Shared
{
    public struct Result<TFailure, TSuccess> : IDisposable
    {
        public TFailure Failure { get; internal set; }
        public TSuccess Success { get; internal set; }

        public bool IsFailure { get; }
        public bool IsSuccess => !IsFailure;

        internal Result(TFailure failure)
        {
            IsFailure = true;
            Failure = failure;
            Success = default(TSuccess);
        }

        internal Result(TSuccess success)
        {
            IsFailure = false;
            Failure = default(TFailure);
            Success = success;
        }

        public static Result<TFailure, TSuccess> Of(TSuccess obj) => obj;
        public static Result<TFailure, TSuccess> Of(TFailure obj) => obj;

        public void Dispose()
        {
            Success = default;
            Failure = default;
        }

        public static implicit operator Result<TFailure, TSuccess>(TFailure failure)
            => new Result<TFailure, TSuccess>(failure);

        public static implicit operator Result<TFailure, TSuccess>(TSuccess success)
            => new Result<TFailure, TSuccess>(success);
    }
}
