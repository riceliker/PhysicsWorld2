using Godot;
using System;
using System.Threading;
/*
	This class will manage the data about loading.
	NOTICE: DO NOT try to WRITE it's data on two and more threads.
*/
public static class LoadingProcess
{
	private static readonly ReaderWriterLockSlim _rwLock = new();
	private static string process_information = "";
	private static int process_percent = 0;
	public static void setInformationToProcess(String information)
	{
		try
		{
			_rwLock.EnterWriteLock();
			process_information = information;
		}
		finally
		{
			_rwLock.ExitWriteLock();
		}
	}
	public static void setProcess(int percent)
	{
		try
		{
			_rwLock.EnterWriteLock();
			process_percent = percent;
		}
		finally
		{
			_rwLock.ExitWriteLock();
		}
	}
	public static (string information, int progress) GetLoadingInfo()
    {
        try
        {
            // 进入读锁（可以多个线程同时进）
            _rwLock.EnterReadLock();
            return (process_information, process_percent);
        }
        finally
        {
            _rwLock.ExitReadLock();
        }
    }
	public static void Reset()
    {
        try
        {
            _rwLock.EnterWriteLock();
            process_information = "";
            process_percent = 0;
        }
        finally
        {
            _rwLock.ExitWriteLock();
        }
    }
}
