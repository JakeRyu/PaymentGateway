using System;

namespace Common.DateService
{
    public interface IDateTime
    {
        DateTime Now { get; }
    }
}