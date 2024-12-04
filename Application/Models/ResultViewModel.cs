﻿namespace LogTransformer.Application.Models
{
    public class ResultViewModel
    {
        public ResultViewModel(bool isSuccess = true, string message = "")
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public bool IsSuccess { get; private set; }
        public string Message { get; private set; }

        public static ResultViewModel Success()
        {
            return new ResultViewModel(true, string.Empty);
        }

        public static ResultViewModel Error(string message)
        {
            return new ResultViewModel(false, message);
        }
    }

    public class ResultViewModel<T> : ResultViewModel
    {
        public ResultViewModel(T data, bool isSuccess = true, string message = "")
            : base(isSuccess, message)
        {
            Data = data;
        }

        public T Data { get; private set; }

        public static ResultViewModel<T> Success(T data)
        {
            return new ResultViewModel<T>(data, true, string.Empty);
        }

        public static ResultViewModel<T> Error(string message)
        {
            return new ResultViewModel<T>(default, false, message);
        }
    }
}
