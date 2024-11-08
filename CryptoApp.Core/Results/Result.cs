﻿namespace CryptoApp.Core.Results;

public class Result<T> where T : class
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public T? Data { get; set; }
    
    private Result(bool isSuccess, string message, T data)
    {
        IsSuccess = isSuccess;
        Message = message;
        Data = data;
    }
    
    public static Result<T> Success(T data)
    {
        return new Result<T>(true, string.Empty, data);
    }
    
    public static Result<T> Failure(string message)
    {
        return new Result<T>(false, message, default);
    }
}